package com.fitbot.data

enum class ProductType {
    SOUCE,
    PRODUCT
}

data class Product(
    val name: String,
    val type: ProductType
)
