package com.fitbot.config.states

import com.fitbot.data.User
import dev.inmo.tgbotapi.types.ChatId

data class ShouldRegister(override val context: ChatId, val userContext: dev.inmo.tgbotapi.types.chat.User) : BotState
data class Registration(override val context: ChatId) : BotState
