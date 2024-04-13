package com.fitbot.data

import java.util.Date

data class User(
    val chatId: Long,

    val firstName: String,
    val lastName: String,

    var birth: Date? = null,
    var allergy: MutableList<Product> = mutableListOf(),
    var favsProducts: MutableList<Product> = mutableListOf(),
    var favsDishes: MutableList<Dish> = mutableListOf()
)
