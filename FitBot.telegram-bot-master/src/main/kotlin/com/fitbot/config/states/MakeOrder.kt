package com.fitbot.config.states

import com.fitbot.data.Dish
import com.fitbot.data.Product
import com.fitbot.data.User
import dev.inmo.tgbotapi.types.ChatId

data class MakeOrder(override val context: ChatId) : BotState
data class ReadySaladOrder(override val context: ChatId, val userContext: User) : BotState
data class ReadySaladApprove(override val context: ChatId, val userContext: User, val dish: Dish) : BotState
data class ConstructorSaladOrder(override val context: ChatId, val userContext: User) : BotState
data class ConstructorSaladApprove(override val context: ChatId, val userContext: User, val prds: List<Product>) : BotState
