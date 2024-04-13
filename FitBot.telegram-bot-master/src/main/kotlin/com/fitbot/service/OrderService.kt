package com.fitbot.service

import com.fitbot.data.Dish
import com.fitbot.data.EnumOrderType
import com.fitbot.data.Order
import com.fitbot.data.Product
import com.fitbot.data.User
import com.fitbot.storage.OrderStorage
import com.soywiz.klock.DateTime

class OrderService {
    private var storage: OrderStorage = OrderStorage()

    fun makeOrder(type: EnumOrderType, dish: Dish, user: User): Order {
        val order = Order(
            type,
            DateTime.now(),
            null,
            dish,
            user
        )
        storage.makeOrder(order)
        return order
    }

    fun makeOrder(type: EnumOrderType, prds: List<Product>, user: User): Order {
        val order = Order (
            type,
            DateTime.now(),
            prds,
            null,
            user
        )
        storage.makeOrder(order)
        return order
    }
}
