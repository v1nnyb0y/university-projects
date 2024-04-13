@file:OptIn(PreviewFeature::class)
@file:Suppress("OPT_IN_IS_NOT_ENABLED")

package com.fitbot.config

import com.fitbot.config.consts.ACCEPT
import com.fitbot.config.consts.ALREADY_EXISTS
import com.fitbot.config.consts.AUTH_WRONG
import com.fitbot.config.consts.AWAY_CMD
import com.fitbot.config.consts.BACK
import com.fitbot.config.consts.CALS
import com.fitbot.config.consts.CHOOSE_CMD_FROM_LIST
import com.fitbot.config.consts.CHOOSE_PRODUCT
import com.fitbot.config.consts.CHOOSE_SOUCE
import com.fitbot.config.consts.CONSTR_SALAD
import com.fitbot.config.consts.DELETE
import com.fitbot.config.consts.DESCR_SALAD
import com.fitbot.config.consts.FATS
import com.fitbot.config.consts.FAVS_DISH
import com.fitbot.config.consts.FAVS_PRDS
import com.fitbot.config.consts.HAVE_ALLERGY
import com.fitbot.config.consts.HELLO_MSG
import com.fitbot.config.consts.HYBRO
import com.fitbot.config.consts.INPUT_AGE
import com.fitbot.config.consts.MAKEORDER_CMD
import com.fitbot.config.consts.MAKE_ORDER
import com.fitbot.config.consts.MYPROFILE_CMD
import com.fitbot.config.consts.NEXT
import com.fitbot.config.consts.NO
import com.fitbot.config.consts.PREV
import com.fitbot.config.consts.PROFILE_AGE
import com.fitbot.config.consts.PROFILE_AGE_NOT
import com.fitbot.config.consts.PROFILE_ALLERGY
import com.fitbot.config.consts.PROFILE_ALLERGY_NOT
import com.fitbot.config.consts.PROFILE_BIRTHDAY
import com.fitbot.config.consts.PROFILE_FAVS_DISH
import com.fitbot.config.consts.PROFILE_FAVS_NOT
import com.fitbot.config.consts.PROFILE_FAVS_PRD
import com.fitbot.config.consts.PROFILE_INPUT
import com.fitbot.config.consts.PRT
import com.fitbot.config.consts.READY_SALAD
import com.fitbot.config.consts.RECHOOSE
import com.fitbot.config.consts.REGISTRATION_CMD
import com.fitbot.config.consts.SALAD_NAME
import com.fitbot.config.consts.SET_ALLERGY
import com.fitbot.config.consts.SET_BIRTH
import com.fitbot.config.consts.SET_FAVS
import com.fitbot.config.consts.START_CMD
import com.fitbot.config.consts.THANKS
import com.fitbot.config.consts.WELCOME
import com.fitbot.config.consts.WHAT_ALLERGY
import com.fitbot.config.consts.WHAT_FAVS_CHOOSE
import com.fitbot.config.consts.WHAT_FAVS_DISHES
import com.fitbot.config.consts.WHAT_FAVS_PRDS
import com.fitbot.config.consts.WHAT_ORDER
import com.fitbot.config.consts.WHAT_READY_SALAD
import com.fitbot.config.consts.WRONG_AGE
import com.fitbot.config.consts.YES
import com.fitbot.config.consts.YOUR_CHOOSE
import com.fitbot.config.consts.YOUR_PRD
import com.fitbot.config.consts.YOUR_SOUCE
import com.fitbot.config.states.BotState
import com.fitbot.config.states.ConstructorSaladApprove
import com.fitbot.config.states.ConstructorSaladOrder
import com.fitbot.config.states.EditAllergy
import com.fitbot.config.states.EditAllergyLogic
import com.fitbot.config.states.EditBirthday
import com.fitbot.config.states.EditFavs
import com.fitbot.config.states.EditFavsDish
import com.fitbot.config.states.EditFavsPrd
import com.fitbot.config.states.EditProfile
import com.fitbot.config.states.MakeOrder
import com.fitbot.config.states.ReadySaladApprove
import com.fitbot.config.states.ReadySaladOrder
import com.fitbot.config.states.Registration
import com.fitbot.config.states.ShouldRegister
import com.fitbot.data.Dish
import com.fitbot.data.EnumOrderType
import com.fitbot.data.Product
import com.fitbot.data.ProductType
import com.fitbot.data.User
import com.fitbot.helper.Calculations
import com.fitbot.helper.QRTools
import com.fitbot.service.DishService
import com.fitbot.service.OrderService
import com.fitbot.service.ProductService
import com.fitbot.service.UserService
import com.fitbot.utils.logger
import dev.inmo.tgbotapi.extensions.api.bot.getMe
import dev.inmo.tgbotapi.extensions.api.send.media.sendPhoto
import dev.inmo.tgbotapi.extensions.api.send.sendTextMessage
import dev.inmo.tgbotapi.extensions.behaviour_builder.telegramBotWithBehaviourAndFSMAndStartLongPolling
import dev.inmo.tgbotapi.extensions.behaviour_builder.triggers_handling.command
import dev.inmo.tgbotapi.extensions.behaviour_builder.triggers_handling.onText
import dev.inmo.tgbotapi.extensions.utils.asFromUser
import dev.inmo.tgbotapi.extensions.utils.extensions.raw.text
import dev.inmo.tgbotapi.extensions.utils.types.buttons.ReplyKeyboardBuilder
import dev.inmo.tgbotapi.extensions.utils.types.buttons.replyKeyboard
import dev.inmo.tgbotapi.extensions.utils.types.buttons.row
import dev.inmo.tgbotapi.extensions.utils.types.buttons.simpleButton
import dev.inmo.tgbotapi.requests.abstracts.InputFile
import dev.inmo.tgbotapi.utils.PreviewFeature
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import org.slf4j.Logger
import java.io.File
import java.text.ParseException
import java.util.Date

class FSMTelegramBot(
    private val userService: UserService,
    private val productService: ProductService,
    private val dishService: DishService,
    private val orderService: OrderService,
    private val qrTools: QRTools = QRTools,
    private val botToken: String = "5492089608:AAGDE3Piufe-dzAJaNWX-nxq6bExp-uUp30"
) {
    private val log: Logger by logger()

    private enum class EnumPagingType {
        Product,
        Dish
    }

    private fun ReplyKeyboardBuilder.orderType() {
        row {
            simpleButton(READY_SALAD)
            simpleButton(CONSTR_SALAD)
        }
        row {
            simpleButton(BACK)
        }
    }

    private fun ReplyKeyboardBuilder.startRegPageButtons() {
        row {
            simpleButton(REGISTRATION_CMD)
        }
        row {
            simpleButton(AWAY_CMD)
        }
    }

    private fun ReplyKeyboardBuilder.changeMyProfile() {
        row {
            simpleButton(SET_ALLERGY)
            simpleButton(SET_FAVS)
        }
        row {
            simpleButton(SET_BIRTH)
        }
        row {
            simpleButton(BACK)
        }
    }

    private fun ReplyKeyboardBuilder.pagingOrderProduct(startIndex: Int, listPrd: List<Product>, selected: List<Product>, allergy: List<Product>, type: ProductType) {
        val list = listPrd.filter {
            !selected.contains(it) && !allergy.contains(it) && it.type === type
        }
        if (list.isNotEmpty()) {
            if (list.count() > 3) {
                var endIndex = startIndex + 2
                if (endIndex > list.count()) {
                    endIndex = list.count() - 1
                }
                row {
                    list.subList(startIndex, endIndex + 1).forEach {
                        simpleButton(it.name)
                    }
                }
                row {
                    if (startIndex != 0) {
                        simpleButton(PREV)
                    }
                    simpleButton(BACK)
                    if (endIndex != list.count() - 1) {
                        simpleButton(NEXT)
                    }
                }
            } else {
                row {
                    list.forEach {
                        simpleButton(it.name)
                    }
                }
                row {
                    simpleButton(BACK)
                }
            }
        }
    }

    private fun ReplyKeyboardBuilder.pagingOrderDish(startIndex: Int, listDish: List<Dish>, selected: List<Product>) {
        val list = listDish.filter {
            var isOk = true
            it.products.forEach { it ->
                if (selected.contains(it)) {
                    isOk = false
                }
            }
            isOk
        }
        if (list.isNotEmpty()) {
            if (list.count() > 3) {
                var endIndex = startIndex + 2
                if (endIndex > list.count()) {
                    endIndex = list.count() - 1
                }
                row {
                    list.subList(startIndex, endIndex + 1).forEach {
                        simpleButton(it.name)
                    }
                }
                row {
                    if (startIndex != 0) {
                        simpleButton(PREV)
                    }
                    simpleButton(BACK)
                    if (endIndex != list.count() - 1) {
                        simpleButton(NEXT)
                    }
                }
            } else {
                row {
                    list.forEach { it ->
                        simpleButton(it.name)
                    }
                }
                row {
                    simpleButton(BACK)
                }
            }
        }
    }

    private fun ReplyKeyboardBuilder.paging(
        startIndex: Int,
        what: EnumPagingType,
        listPrd: List<Product> = emptyList(),
        selectedPrd: List<Product> = emptyList(),
        listDish: List<Dish> = emptyList(),
        selectedDish: List<Dish> = emptyList()
    ) {
        val list = when (what) {
            EnumPagingType.Dish -> listDish
            EnumPagingType.Product -> listPrd
            else -> emptyList()
        }
        val selectedItemsList = when (what) {
            EnumPagingType.Product -> selectedPrd
            EnumPagingType.Dish -> selectedDish
            else -> emptyList()
        }
        if (list.isNotEmpty()) {
            if (list.count() > 3) {
                var endIndex = startIndex + 2
                if (endIndex > list.count()) {
                    endIndex = list.count() - 1
                }
                row {
                    list.subList(startIndex, endIndex + 1).forEach {
                        if (selectedItemsList.contains(it)) {
                            simpleButton(
                                DELETE + when (what) {
                                    EnumPagingType.Product -> (it as Product).name
                                    EnumPagingType.Dish -> (it as Dish).name
                                }
                            )
                        } else {
                            simpleButton(
                                ACCEPT + when (what) {
                                    EnumPagingType.Product -> (it as Product).name
                                    EnumPagingType.Dish -> (it as Dish).name
                                }
                            )
                        }
                    }
                }
                row {
                    if (startIndex != 0) {
                        simpleButton(PREV)
                    }
                    simpleButton(BACK)
                    if (endIndex != list.count() - 1) {
                        simpleButton(NEXT)
                    }
                }
            } else {
                row {
                    list.forEach {
                        if (selectedItemsList.contains(it)) {
                            simpleButton(
                                DELETE + when (what) {
                                    EnumPagingType.Product -> (it as Product).name
                                    EnumPagingType.Dish -> (it as Dish).name
                                }
                            )
                        } else {
                            simpleButton(
                                ACCEPT + when (what) {
                                    EnumPagingType.Product -> (it as Product).name
                                    EnumPagingType.Dish -> (it as Dish).name
                                }
                            )
                        }
                    }
                }
                row {
                    simpleButton(BACK)
                }
            }
        }
    }

    private fun ReplyKeyboardBuilder.yesNo(extraNo: String = "", extraYes: String = "") {
        row {
            simpleButton(YES + extraYes)
            simpleButton(NO + extraNo)
        }
    }

    private fun ReplyKeyboardBuilder.dishOrPrdFavs() {
        row {
            simpleButton(FAVS_DISH)
            simpleButton(FAVS_PRDS)
        }
        row {
            simpleButton(BACK)
        }
    }

    private fun formatMyProfileMsg(context: User): String {
        var msg = PROFILE_INPUT + "${context.firstName} ${context.lastName}"
        msg += if (context.birth != null) {
            PROFILE_AGE + Calculations.calculateAge(context.birth!!)
            PROFILE_BIRTHDAY + Calculations.dateToString(context.birth!!)
        } else {
            PROFILE_AGE_NOT
        }
        msg += if (context.allergy.isNotEmpty()) {
            var tmp = ""
            context.allergy.forEach {
                tmp += it.name + ", "
            }
            PROFILE_ALLERGY + tmp.substring(0, tmp.length - 2)
        } else {
            PROFILE_ALLERGY_NOT
        }
        msg += if (context.favsDishes.isNotEmpty() || context.favsProducts.isNotEmpty()) {
            var res = ""
            if (context.favsDishes.isNotEmpty()) {
                var tmp = ""
                context.favsDishes.forEach {
                    tmp += it.name + ", "
                }
                res += PROFILE_FAVS_DISH + tmp.substring(0, tmp.length - 2)
            }
            if (context.favsProducts.isNotEmpty()) {
                var tmp = ""
                context.favsProducts.forEach {
                    tmp += it.name + ", "
                }
                res += PROFILE_FAVS_PRD + tmp.substring(0, tmp.length - 2)
            }
            res
        } else {
            PROFILE_FAVS_NOT
        }

        return msg
    }

    private fun dishDescription(dish: Dish): String {
        var msg = SALAD_NAME + dish.name
        msg += CALS + dish.cals
        msg += FATS + dish.fats
        msg += PRT + dish.proteins
        msg += HYBRO + dish.carbohydrates
        msg += DESCR_SALAD
        dish.products.forEach {
            msg += it.name + ", "
        }
        return msg.substring(0, msg.length - 2)
    }

    private fun productDescription(selected: List<Product>): String {
        var msg = YOUR_CHOOSE
        msg += YOUR_PRD
        selected.filter { it.type === ProductType.PRODUCT }.forEach {
            msg += it.name + ", "
        }
        msg = msg.substring(0, msg.length - 2)
        msg += YOUR_SOUCE
        selected.filter { it.type === ProductType.SOUCE }.forEach {
            msg += it.name
        }
        return msg
    }

    suspend fun start() {
        telegramBotWithBehaviourAndFSMAndStartLongPolling<BotState>(
            botToken,
            CoroutineScope(Dispatchers.IO),
            onStateHandlingErrorHandler = { state, e ->
                when (state) {
                    is ShouldRegister -> {
                        println("Thrown error on ShouldRegister")
                    }
                    is Registration -> {
                        println("Thrown error on Registration")
                    }
                    is EditProfile -> {
                        println("Thrown error on EditProfile")
                    }
                    is EditBirthday -> {
                        println("Thrown error on EditBirthday")
                    }
                    is EditFavs -> {
                        println("Thrown error on EditFavs")
                    }
                    is EditAllergy -> {
                        println("Thrown error on EditAllergy")
                    }
                }
                e.printStackTrace()
                state
            }
        ) {
            println(getMe())

            strictlyOn<ShouldRegister> {
                if (userService.isExistedByChatId(it.context.chatId)) {
                    sendTextMessage(it.context, ALREADY_EXISTS)
                } else {
                    userService.create(it.context.chatId, it.userContext)
                    sendTextMessage(
                        it.context, HELLO_MSG,
                        replyMarkup = replyKeyboard(resizeKeyboard = true, oneTimeKeyboard = true) {
                            startRegPageButtons()
                        }
                    )
                    var openListener = true
                    var isError: Boolean

                    onText { ot ->
                        val text = ot.text
                        if (openListener && ot.chat.id.chatId == it.context.chatId && text != null) {
                            when (text) {
                                REGISTRATION_CMD -> {
                                    startChain(Registration(it.context))
                                    openListener = false
                                    isError = false
                                }
                                AWAY_CMD -> {
                                    sendTextMessage(it.context, WELCOME)
                                    openListener = false
                                    isError = false
                                }
                                else -> {
                                    isError = true
                                }
                            }
                            if (isError) {
                                sendTextMessage(
                                    it.context, CHOOSE_CMD_FROM_LIST,
                                    replyMarkup = replyKeyboard(resizeKeyboard = true, oneTimeKeyboard = true) {
                                        startRegPageButtons()
                                    }
                                )
                            }
                        }
                    }
                }

                null
            }

            strictlyOn<Registration> {
                log.info("Registration Command from ${it.context}")
                val userContext = userService.get(it.context.chatId)
                if (userContext != null) {
                    startChain(EditBirthday(it.context, true, userContext))
                } else {
                    sendTextMessage(it.context, AUTH_WRONG)
                }

                null
            }

            strictlyOn<EditProfile> {
                val userContext = userService.get(it.context.chatId)
                if (userContext != null) {
                    sendTextMessage(
                        it.context, formatMyProfileMsg(userContext),
                        replyMarkup = replyKeyboard(resizeKeyboard = true, oneTimeKeyboard = true) {
                            changeMyProfile()
                        }
                    )
                    var isError: Boolean
                    var openListener = true

                    onText { ot ->
                        val text = ot.text
                        if (openListener && ot.chat.id.chatId == it.context.chatId && text != null) {
                            when (text) {
                                SET_ALLERGY -> {
                                    startChain(EditAllergy(it.context, userContext))
                                    openListener = false
                                    isError = false
                                }
                                SET_FAVS -> {
                                    startChain(EditFavs(it.context, userContext))
                                    openListener = false
                                    isError = false
                                }
                                SET_BIRTH -> {
                                    startChain(EditBirthday(it.context, false, userContext))
                                    openListener = false
                                    isError = false
                                }
                                BACK -> {
                                    sendTextMessage(it.context, WELCOME)
                                    openListener = false
                                    isError = false
                                }
                                else -> {
                                    isError = true
                                }
                            }
                            if (isError) {
                                sendTextMessage(
                                    it.context, CHOOSE_CMD_FROM_LIST,
                                    replyMarkup = replyKeyboard(resizeKeyboard = true, oneTimeKeyboard = true) {
                                        changeMyProfile()
                                    }
                                )
                            }
                        }
                    }
                } else {
                    sendTextMessage(it.context, AUTH_WRONG)
                }
                null
            }

            strictlyOn<EditBirthday> {
                log.info("Edit Birthday from ${it.context}")
                sendTextMessage(it.context, INPUT_AGE)
                var openListener = true
                var isError: Boolean
                var birth: Date

                onText { ot ->
                    val text = ot.text
                    if (openListener && ot.chat.id.chatId == it.context.chatId && text != null) {
                        try {
                            birth = Calculations.DT_FORMATTER.parse(text)
                            val age = Calculations.calculateAge(birth)
                            if (age in (10..80)) {
                                userService.setBirthday(it.context.chatId, birth)
                                if (it.isRegistration) {
                                    sendTextMessage(it.context, THANKS)
                                    sendTextMessage(it.context, WELCOME)
                                } else {
                                    startChain(EditProfile(it.context))
                                }
                                openListener = false
                                isError = false
                            } else {
                                isError = true
                            }
                        } catch (e: ParseException) {
                            isError = true
                        }
                        if (isError) {
                            sendTextMessage(it.context, WRONG_AGE)
                        }
                    }
                }
                null
            }

            strictlyOn<EditFavs> {
                log.info("Edit Favorite Food from ${it.context}")
                val userContext = userService.get(it.context.chatId)
                if (userContext != null) {
                    sendTextMessage(
                        it.context, WHAT_FAVS_CHOOSE,
                        replyMarkup = replyKeyboard(resizeKeyboard = true, oneTimeKeyboard = true) {
                            dishOrPrdFavs()
                        }
                    )
                    var openListener = true
                    var isError: Boolean

                    onText { ot ->
                        val text = ot.text
                        if (openListener && ot.chat.id.chatId == it.context.chatId && text != null) {
                            when (text) {
                                FAVS_PRDS -> {
                                    startChain(EditFavsPrd(it.context, userContext))
                                    isError = false
                                    openListener = false
                                }
                                FAVS_DISH -> {
                                    startChain(EditFavsDish(it.context, userContext))
                                    isError = false
                                    openListener = false
                                }
                                BACK -> {
                                    startChain(EditProfile(it.context))
                                    isError = false
                                    openListener = false
                                }
                                else -> {
                                    isError = true
                                }
                            }
                            if (isError) {
                                sendTextMessage(
                                    it.context, CHOOSE_CMD_FROM_LIST,
                                    replyMarkup = replyKeyboard(resizeKeyboard = true, oneTimeKeyboard = true) {
                                        dishOrPrdFavs()
                                    }
                                )
                            }
                        }
                    }
                } else {
                    sendTextMessage(it.context, AUTH_WRONG)
                }
                null
            }

            strictlyOn<EditFavsPrd> {
                val prds = productService.get()
                var index = 0
                var isError: Boolean
                sendTextMessage(
                    it.context, WHAT_FAVS_PRDS,
                    replyMarkup = replyKeyboard(resizeKeyboard = true) {
                        paging(index, what = EnumPagingType.Product, listPrd = prds, selectedPrd = it.userContext.favsProducts)
                    }
                )
                var isBack = false
                var openListener = true

                onText { ot ->
                    val text = ot.text
                    if (openListener && ot.chat.id.chatId == it.context.chatId && text != null) {
                        when {
                            text == PREV -> {
                                isError = false
                                index -= 3
                            }
                            text == NEXT -> {
                                isError = false
                                index += 3
                            }
                            text == BACK -> {
                                startChain(EditProfile(it.context))
                                openListener = false
                                isError = false
                                isBack = true
                            }
                            DELETE in text -> {
                                if (text.length > 2) {
                                    val prdName = text.trim().substring(2)
                                    val prd = productService.find(prdName)
                                    it.userContext.favsProducts.remove(prd)
                                } else {
                                    sendTextMessage(it.context, "Что-то пошло не так. Попробуйте убрать любимый продукт еще раз")
                                }
                                isError = false
                            }
                            ACCEPT in text -> {
                                if (text.length > 2) {
                                    val prdName = text.trim().substring(2)
                                    val prd = productService.find(prdName)!!
                                    it.userContext.favsProducts.add(prd)
                                } else {
                                    sendTextMessage(it.context, "Что-то пошло не так. Попробуйте добавить любимый продукт еще раз")
                                }
                                isError = false
                            }
                            else -> {
                                isError = true
                            }
                        }
                        if (isError) {
                            sendTextMessage(
                                it.context, CHOOSE_CMD_FROM_LIST,
                                replyMarkup = replyKeyboard(resizeKeyboard = true) {
                                    paging(index, what = EnumPagingType.Product, listPrd = prds, selectedPrd = it.userContext.favsProducts)
                                }
                            )
                        } else if (!isBack) {
                            sendTextMessage(
                                it.context, WHAT_FAVS_PRDS,
                                replyMarkup = replyKeyboard(resizeKeyboard = true) {
                                    paging(index, what = EnumPagingType.Product, listPrd = prds, selectedPrd = it.userContext.favsProducts)
                                }
                            )
                        }
                    }
                }
                null
            }

            strictlyOn<EditFavsDish> {
                val dishes = dishService.get()
                var index = 0
                var isError: Boolean
                sendTextMessage(
                    it.context, WHAT_FAVS_DISHES,
                    replyMarkup = replyKeyboard(resizeKeyboard = true) {
                        paging(index, what = EnumPagingType.Dish, listDish = dishes, selectedDish = it.userContext.favsDishes)
                    }
                )
                var isBack = false
                var openListener = true

                onText { ot ->
                    val text = ot.text
                    if (openListener && ot.chat.id.chatId == it.context.chatId && text != null) {
                        when {
                            text == PREV -> {
                                isError = false
                                index -= 3
                            }
                            text == NEXT -> {
                                isError = false
                                index += 3
                            }
                            text == BACK -> {
                                startChain(EditProfile(it.context))
                                openListener = false
                                isError = false
                                isBack = true
                            }
                            DELETE in text -> {
                                if (text.length > 2) {
                                    val dishName = text.trim().substring(2)
                                    val dish = dishService.find(dishName)!!
                                    it.userContext.favsDishes.remove(dish)
                                } else {
                                    sendTextMessage(it.context, "Что-то пошло не так. Попробуйте убрать любимое блюдо еще раз")
                                }
                                isError = false
                            }
                            ACCEPT in text -> {
                                if (text.length > 2) {
                                    val dishName = text.trim().substring(2)
                                    val dish = dishService.find(dishName)!!
                                    it.userContext.favsDishes.add(dish)
                                } else {
                                    sendTextMessage(it.context, "Что-то пошло не так. Попробуйте добавить любимое блюдо еще раз")
                                }
                                isError = false
                            }
                            else -> {
                                isError = true
                            }
                        }
                        if (isError) {
                            sendTextMessage(
                                it.context, CHOOSE_CMD_FROM_LIST,
                                replyMarkup = replyKeyboard(resizeKeyboard = true) {
                                    paging(index, what = EnumPagingType.Dish, listDish = dishes, selectedDish = it.userContext.favsDishes)
                                }
                            )
                        } else if (!isBack) {
                            sendTextMessage(
                                it.context, WHAT_FAVS_DISHES,
                                replyMarkup = replyKeyboard(resizeKeyboard = true) {
                                    paging(index, what = EnumPagingType.Dish, listDish = dishes, selectedDish = it.userContext.favsDishes)
                                }
                            )
                        }
                    }
                }
                null
            }

            strictlyOn<EditAllergy> {
                log.info("Edit Allergy from ${it.context}")
                sendTextMessage(
                    it.context, HAVE_ALLERGY,
                    replyMarkup = replyKeyboard(resizeKeyboard = true, oneTimeKeyboard = true) {
                        yesNo()
                    }
                )
                var isError: Boolean
                var openListener = true

                onText { ot ->
                    val text = ot.text
                    if (openListener && ot.chat.id.chatId == it.context.chatId && text != null) {
                        when (text) {
                            YES -> {
                                startChain(EditAllergyLogic(it.context, it.userContext))
                                isError = false
                                openListener = false
                            }
                            NO -> {
                                startChain(EditProfile(it.context))
                                isError = false
                                openListener = false
                            }
                            else -> {
                                isError = true
                            }
                        }
                        if (isError) {
                            sendTextMessage(
                                it.context, CHOOSE_CMD_FROM_LIST,
                                replyMarkup = replyKeyboard(resizeKeyboard = true, oneTimeKeyboard = true) {
                                    yesNo()
                                }
                            )
                        }
                    }
                }
                null
            }

            strictlyOn<EditAllergyLogic> {
                val prds = productService.get()
                var isError: Boolean
                var index = 0
                sendTextMessage(
                    it.context, WHAT_ALLERGY,
                    replyMarkup = replyKeyboard(resizeKeyboard = true) {
                        paging(index, what = EnumPagingType.Product, listPrd = prds, selectedPrd = it.userContext.allergy)
                    }
                )
                var isBack = false
                var openListener = true

                onText { ot ->
                    val text = ot.text
                    if (openListener && ot.chat.id.chatId == it.context.chatId && text != null) {
                        when {
                            text == PREV -> {
                                isError = false
                                index -= 3
                            }
                            text == NEXT -> {
                                isError = false
                                index += 3
                            }
                            text == BACK -> {
                                startChain(EditProfile(it.context))
                                isError = false
                                openListener = false
                                isBack = true
                            }
                            DELETE in text -> {
                                if (text.length > 2) {
                                    val prdName = text.trim().substring(2)
                                    val prd = productService.find(prdName)
                                    it.userContext.allergy.remove(prd)
                                } else {
                                    sendTextMessage(it.context, "Что-то пошло не так. Попробуйте убрать аллерген еще раз")
                                }
                                isError = false
                            }
                            ACCEPT in text -> {
                                if (text.length > 2) {
                                    val prdName = text.trim().substring(2)
                                    val prd = productService.find(prdName)!!
                                    it.userContext.allergy.add(prd)
                                } else {
                                    sendTextMessage(it.context, "Что-то пошло не так. Попробуйте добавить аллерген еще раз")
                                }
                                isError = false
                            }
                            else -> {
                                isError = true
                            }
                        }
                        if (isError) {
                            sendTextMessage(
                                it.context, CHOOSE_CMD_FROM_LIST,
                                replyMarkup = replyKeyboard(resizeKeyboard = true) {
                                    paging(index, what = EnumPagingType.Product, listPrd = prds, selectedPrd = it.userContext.allergy)
                                }
                            )
                        } else if (!isBack) {
                            sendTextMessage(
                                it.context, WHAT_ALLERGY,
                                replyMarkup = replyKeyboard(resizeKeyboard = true) {
                                    paging(index, what = EnumPagingType.Product, listPrd = prds, selectedPrd = it.userContext.allergy)
                                }
                            )
                        }
                    }
                }
                null
            }

            strictlyOn<ReadySaladOrder> {
                log.info("Ready Salad Order from ${it.context}")
                var isError = false
                var index = 0
                val dishes = dishService.get()
                sendTextMessage(
                    it.context, WHAT_READY_SALAD,
                    replyMarkup = replyKeyboard(resizeKeyboard = true, oneTimeKeyboard = true) {
                        pagingOrderDish(
                            index,
                            listDish = dishes,
                            it.userContext.allergy
                        )
                    }
                )
                var openListener = true

                onText { ot ->
                    val text = ot.text
                    if (openListener && ot.chat.id.chatId == it.context.chatId && text != null) {
                        when (text) {
                            PREV -> {
                                isError = false
                                index -= 3
                            }
                            NEXT -> {
                                isError = false
                                index += 3
                            }
                            BACK -> {
                                isError = false
                                openListener = false
                                sendTextMessage(it.context, WELCOME)
                            }
                            else -> {
                                val dish = dishService.find(text)
                                if (dish != null) {
                                    startChain(ReadySaladApprove(it.context, it.userContext, dish))
                                    openListener = false
                                    isError = false
                                } else {
                                    isError = true
                                }
                            }
                        }
                        if (isError) {
                            sendTextMessage(
                                it.context, CHOOSE_CMD_FROM_LIST,
                                replyMarkup = replyKeyboard(resizeKeyboard = true, oneTimeKeyboard = true) {
                                    pagingOrderDish(
                                        index,
                                        listDish = dishes,
                                        it.userContext.allergy
                                    )
                                }
                            )
                        }
                    }
                }
                null
            }

            strictlyOn<ReadySaladApprove> {
                var isError = false
                sendTextMessage(it.context, dishDescription(it.dish))
                sendTextMessage(
                    it.context, MAKE_ORDER,
                    replyMarkup = replyKeyboard(resizeKeyboard = true, oneTimeKeyboard = true) {
                        yesNo()
                    }
                )
                var openListener = true

                onText { ot ->
                    val text = ot.text
                    if (openListener && ot.chat.id.chatId == it.context.chatId && text != null) {
                        when (text) {
                            YES -> {
                                log.info("Bought from ${it.context}")
                                val order = orderService.makeOrder(EnumOrderType.Constructor, it.dish, it.userContext)
                                val path = qrTools.encodeText(order.user.chatId.toString() + order.dish!!.name)
                                val newOrderFile = File(path)
                                sendPhoto(it.context, InputFile.fromFile(newOrderFile))
                                if (!newOrderFile.delete()) {
                                    log.error(String.format("File '%s' removing error", path))
                                }
                                openListener = false
                                isError = false
                                sendTextMessage(it.context, WELCOME)
                            }
                            NO -> {
                                startChain(ReadySaladOrder(it.context, it.userContext))
                                openListener = false
                                isError = false
                            }
                            else -> {
                                isError = true
                            }
                        }
                        if (isError) {
                            sendTextMessage(
                                it.context, CHOOSE_CMD_FROM_LIST,
                                replyMarkup = replyKeyboard(resizeKeyboard = true, oneTimeKeyboard = true) {
                                    yesNo()
                                }
                            )
                        }
                    }
                }
                null
            }

            strictlyOn<ConstructorSaladOrder> {
                log.info("Constructor Salad Order from ${it.context}")
                var isError = false
                var index = 0
                val prds = productService.get()
                val context = userService.get(it.context.chatId)
                var shouldPrint: Boolean
                var selectedProducts: MutableList<Product> = mutableListOf()
                sendTextMessage(
                    it.context,
                    when {
                        selectedProducts.count() == 4 -> CHOOSE_SOUCE
                        else -> CHOOSE_PRODUCT + (selectedProducts.count() + 1)
                    },
                    replyMarkup = replyKeyboard(resizeKeyboard = true, oneTimeKeyboard = true) {
                        pagingOrderProduct(
                            index, prds, selectedProducts, it.userContext.allergy,
                            when {
                                selectedProducts.count() == 4 -> ProductType.SOUCE
                                else -> ProductType.PRODUCT
                            }
                        )
                    }
                )
                var openListener = true

                onText { ot ->
                    val text = ot.text
                    if (openListener && ot.chat.id.chatId == it.context.chatId && text != null) {
                        shouldPrint = false
                        when (text) {
                            PREV -> {
                                isError = false
                                index -= 3
                            }
                            NEXT -> {
                                isError = false
                                index += 3
                            }
                            BACK -> {
                                openListener = false
                                isError = false
                                sendTextMessage(it.context, WELCOME)
                            }
                            else -> {
                                val prd = productService.find(text)
                                if (prd != null) {
                                    if (selectedProducts.count() == 4 && prd.type === ProductType.PRODUCT) {
                                        isError = true
                                    } else if (selectedProducts.count() < 3 && prd.type === ProductType.SOUCE) {
                                        isError = true
                                    } else if (selectedProducts.count() == 4) {
                                        selectedProducts.add(prd)
                                        startChain(ConstructorSaladApprove(it.context, it.userContext, selectedProducts))
                                        openListener = false
                                        isError = false
                                    } else {
                                        selectedProducts.add(prd)
                                        shouldPrint = true
                                    }
                                } else {
                                    isError = true
                                }
                            }
                        }
                        if (isError) {
                            sendTextMessage(
                                it.context, CHOOSE_CMD_FROM_LIST,
                                replyMarkup = replyKeyboard(resizeKeyboard = true, oneTimeKeyboard = true) {
                                    pagingOrderProduct(
                                        index, prds, selectedProducts, it.userContext.allergy,
                                        when {
                                            selectedProducts.count() == 4 -> ProductType.SOUCE
                                            else -> ProductType.PRODUCT
                                        }
                                    )
                                }
                            )
                        } else if (shouldPrint) {
                            sendTextMessage(
                                it.context,
                                when {
                                    selectedProducts.count() == 4 -> CHOOSE_SOUCE
                                    else -> CHOOSE_PRODUCT + (selectedProducts.count() + 1)
                                },
                                replyMarkup = replyKeyboard(resizeKeyboard = true, oneTimeKeyboard = true) {
                                    pagingOrderProduct(
                                        index, prds, selectedProducts, it.userContext.allergy,
                                        when {
                                            selectedProducts.count() == 4 -> ProductType.SOUCE
                                            else -> ProductType.PRODUCT
                                        }
                                    )
                                }
                            )
                        }
                    }
                }
                null
            }

            strictlyOn<ConstructorSaladApprove> {
                var isError = false
                sendTextMessage(it.context, productDescription(it.prds))
                sendTextMessage(
                    it.context, MAKE_ORDER,
                    replyMarkup = replyKeyboard(resizeKeyboard = true, oneTimeKeyboard = true) {
                        yesNo(extraNo = RECHOOSE)
                    }
                )
                var openListener = true

                onText { ot ->
                    val text = ot.text
                    if (openListener && ot.chat.id.chatId == it.context.chatId && text != null) {
                        when (text) {
                            YES -> {
                                log.info("Bought from ${it.context}")
                                val order = orderService.makeOrder(EnumOrderType.Ready, it.prds, it.userContext)
                                var qrContent = order.user.chatId.toString()
                                order.prds!!.forEach { prd ->
                                    qrContent += prd.name
                                }
                                val path = qrTools.encodeText(qrContent)
                                val newOrderFile = File(path)
                                sendPhoto(it.context, InputFile.fromFile(newOrderFile))
                                if (!newOrderFile.delete()) {
                                    log.error(String.format("File '%s' removing error", path))
                                }
                                openListener = false
                                isError = false
                                sendTextMessage(it.context, WELCOME)
                            }
                            NO + RECHOOSE -> {
                                startChain(ConstructorSaladOrder(it.context, it.userContext))
                                openListener = false
                                isError = false
                            }
                            else -> {
                                isError = true
                            }
                        }
                        if (isError) {
                            sendTextMessage(
                                it.context, CHOOSE_CMD_FROM_LIST,
                                replyMarkup = replyKeyboard(resizeKeyboard = true, oneTimeKeyboard = true) {
                                    yesNo(extraNo = RECHOOSE)
                                }
                            )
                        }
                    }
                }
                null
            }

            strictlyOn<MakeOrder> {
                val context = userService.get(it.context.chatId)
                if (context != null) {
                    var isError: Boolean
                    sendTextMessage(
                        it.context, WHAT_ORDER,
                        replyMarkup = replyKeyboard(resizeKeyboard = true, oneTimeKeyboard = true) {
                            orderType()
                        }
                    )
                    var openListener = true

                    onText { ot ->
                        val text = ot.text
                        if (openListener && ot.chat.id.chatId == it.context.chatId && text != null) {
                            when (text) {
                                READY_SALAD -> {
                                    startChain(ReadySaladOrder(it.context, context))
                                    openListener = false
                                    isError = false
                                }
                                CONSTR_SALAD -> {
                                    startChain(ConstructorSaladOrder(it.context, context))
                                    openListener = false
                                    isError = false
                                }
                                BACK -> {
                                    sendTextMessage(it.context, WELCOME)
                                    openListener = false
                                    isError = false
                                }
                                else -> {
                                    isError = true
                                }
                            }
                            if (isError) {
                                sendTextMessage(
                                    it.context, CHOOSE_CMD_FROM_LIST,
                                    replyMarkup = replyKeyboard(resizeKeyboard = true, oneTimeKeyboard = true) {
                                        orderType()
                                    }
                                )
                            }
                        }
                    }
                } else {
                    sendTextMessage(it.context, AUTH_WRONG)
                }
                null
            }

            command(START_CMD, requireOnlyCommandInMessage = true) {
                log.info("Start Command from ${it.chat.id}")
                startChain(ShouldRegister(it.chat.id, it.asFromUser()!!.user))
            }

            command(MYPROFILE_CMD, requireOnlyCommandInMessage = true) {
                log.info("My Profile Command from ${it.chat.id}")
                startChain(EditProfile(it.chat.id))
            }

            command(MAKEORDER_CMD, requireOnlyCommandInMessage = true) {
                log.info("Make Order Command from ${it.chat.id}")
                startChain(MakeOrder(it.chat.id))
            }
        }.second.join()
    }
}
