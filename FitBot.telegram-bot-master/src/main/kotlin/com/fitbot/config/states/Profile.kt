package com.fitbot.config.states

import com.fitbot.data.User
import dev.inmo.tgbotapi.types.ChatId

data class EditProfile(override val context: ChatId) : BotState

data class EditBirthday(override val context: ChatId, val isRegistration: Boolean, val userContext: User) : BotState
data class EditFavs(override val context: ChatId, val userContext: User) : BotState
data class EditFavsPrd(override val context: ChatId, val userContext: User) : BotState
data class EditFavsDish(override val context: ChatId, val userContext: User) : BotState
data class EditAllergy(override val context: ChatId, val userContext: User) : BotState
data class EditAllergyLogic(override val context: ChatId, val userContext: User) : BotState
