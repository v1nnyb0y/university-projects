package com.fitbot.data

import com.soywiz.klock.DateTime

enum class EnumOrderType {
    Constructor,
    Ready
}

data class Order(
    val type: EnumOrderType,
    val datetime: DateTime,
    val prds: List<Product>?,
    val dish: Dish?,
    val user: User
)
