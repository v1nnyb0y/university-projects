package com.fitbot.storage

import com.fitbot.data.Product
import com.fitbot.utils.logger
import org.slf4j.Logger

class ProductStorage {
    private val log: Logger by logger()
    private var products = mutableListOf<Product>()

    fun save(prd: Product): Product {
        products.add(prd)
        return prd
    }

    fun find(name: String): Product? = products.find { it -> it.name == name }

    fun get(): List<Product> = products
}
