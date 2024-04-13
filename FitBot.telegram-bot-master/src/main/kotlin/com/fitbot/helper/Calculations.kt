@file:OptIn(InternalAPI::class)
@file:Suppress("OPT_IN_IS_NOT_ENABLED")

package com.fitbot.helper

import io.ktor.server.util.toLocalDateTime
import io.ktor.util.InternalAPI
import java.text.SimpleDateFormat
import java.time.LocalDateTime
import java.util.Date

class Calculations {
    companion object {
        val DT_FORMATTER = SimpleDateFormat("dd-MM-yyyy")

        fun calculateAge(birthday: Date): Int = LocalDateTime.now().year - birthday.toLocalDateTime().year

        fun dateToString(birthday: Date): String = DT_FORMATTER.format(birthday)!!
    }
}
