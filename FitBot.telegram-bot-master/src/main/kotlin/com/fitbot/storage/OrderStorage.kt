package com.fitbot.storage

import com.fitbot.data.Order

class OrderStorage {
    private var orders = mutableListOf<Order>()

    fun makeOrder(order: Order) {
        orders.add(order)
    }
}