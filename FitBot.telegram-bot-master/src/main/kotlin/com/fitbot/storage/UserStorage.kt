package com.fitbot.storage

import com.fitbot.data.Dish
import com.fitbot.data.Product
import com.fitbot.data.User
import com.fitbot.utils.logger
import org.slf4j.Logger
import java.util.Date

class UserStorage {
    private val log: Logger by logger()
    private var users = HashMap<Long, User>()

    fun get(chatId: Long): User? = users[chatId]

    fun save(chatId: Long, user: User): User {
        users[chatId] = user
        return user
    }

    fun setBirth(chatId: Long, birth: Date) {
        users[chatId]!!.birth = birth
    }

    fun setAllergy(chatId: Long, prd: Product) {
        users[chatId]!!.allergy.add(prd)
    }

    fun removeAllergy(chatId: Long, prd: Product) {
        users[chatId]!!.allergy.remove(prd)
    }

    fun setFavsDish(chatId: Long, dish: Dish) {
        users[chatId]!!.favsDishes.add(dish)
    }

    fun setFavsPrd(chatId: Long, prd: Product) {
        users[chatId]!!.favsProducts.add(prd)
    }

    fun removeFavsDish(chatId: Long, dish: Dish) {
        users[chatId]!!.favsDishes.remove(dish)
    }

    fun removeFavsPrd(chatId: Long, prd: Product) {
        users[chatId]!!.favsProducts.remove(prd)
    }
}
