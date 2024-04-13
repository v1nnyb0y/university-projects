package com.fitbot.data

data class Dish(
    val name: String,
    val products: List<Product>,
    val proteins: Int,
    val fats: Int,
    val carbohydrates: Int,
    val cals: Int
)
