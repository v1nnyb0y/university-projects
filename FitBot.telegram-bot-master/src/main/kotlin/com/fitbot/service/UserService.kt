package com.fitbot.service

import com.fitbot.data.Dish
import com.fitbot.data.Product
import com.fitbot.data.User
import com.fitbot.storage.UserStorage
import com.fitbot.utils.logger
import org.slf4j.Logger
import java.util.Date

class UserService {
    private val log: Logger by logger()
    private var storage: UserStorage = UserStorage()

    fun isExistedByChatId(chatId: Long): Boolean = storage.get(chatId) != null

    fun create(chatId: Long, user: dev.inmo.tgbotapi.types.chat.User): User = storage.save(
        chatId,
        User(
            chatId = chatId,
            firstName = user.firstName,
            lastName = user.lastName
        )
    )

    fun setBirthday(chatId: Long, birth: Date) {
        storage.setBirth(chatId, birth)
    }

    fun setAllergy(chatId: Long, product: Product) {
        storage.setAllergy(chatId, product)
    }

    fun removeAllergy(chatId: Long, product: Product) {
        storage.removeAllergy(chatId, product)
    }

    fun setFavs(chatId: Long, product: Product) {
        storage.setFavsPrd(chatId, product)
    }

    fun setFavs(chatId: Long, dish: Dish) {
        storage.setFavsDish(chatId, dish)
    }

    fun removeFavs(chatId: Long, product: Product) {
        storage.removeFavsPrd(chatId, product)
    }

    fun removeFavs(chatId: Long, dish: Dish) {
        storage.removeFavsDish(chatId, dish)
    }

    fun get(chatId: Long): User? = storage.get(chatId)
}
