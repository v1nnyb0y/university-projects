package com.fitbot.service

import com.fitbot.data.Dish
import com.fitbot.data.Product
import com.fitbot.storage.DishStorage

class DishService {
    private var storage: DishStorage = DishStorage()

    fun create(name: String, products: List<Product>, proteins: Int, fats: Int, carbohydrates: Int, cals: Int) = storage.save(
        Dish(
            name,
            products,
            proteins,
            fats,
            carbohydrates,
            cals
        )
    )

    fun find(name: String): Dish? = storage.find(name)

    fun get(): List<Dish> = storage.get()
}
