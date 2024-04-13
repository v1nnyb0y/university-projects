package com.fitbot

import com.fitbot.config.FSMTelegramBot
import com.fitbot.service.DishService
import com.fitbot.service.OrderService
import com.fitbot.service.ProductService
import com.fitbot.service.RunAfterStartup
import com.fitbot.service.UserService
import io.ktor.util.InternalAPI

suspend fun main() {
    val userService = UserService()
    val productService = ProductService()
    val dishService = DishService()
    val orderService = OrderService()
    RunAfterStartup.startUp(dishService, productService)
    val fsm = FSMTelegramBot(userService, productService, dishService, orderService)

    fsm.start()
}
