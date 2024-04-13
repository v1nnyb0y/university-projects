package com.fitbot.storage

import com.fitbot.data.Dish
import com.fitbot.utils.logger
import org.slf4j.Logger

class DishStorage {
    private val log: Logger by logger()
    private var dishes = mutableListOf<Dish>()

    fun save(dish: Dish): Dish {
        dishes.add(dish)
        return dish
    }

    fun find(name: String): Dish? = dishes.find { it -> it.name == name }

    fun get(): List<Dish> = dishes
}
