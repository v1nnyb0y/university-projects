package com.fitbot.service

import com.fitbot.data.Product
import com.fitbot.data.ProductType
import com.fitbot.storage.ProductStorage

class ProductService {
    private var storage: ProductStorage = ProductStorage()

    fun create(name: String, type: ProductType): Product = storage.save(Product(name, type))

    fun find(name: String): Product? = storage.find(name)

    fun get(): List<Product> = storage.get()
}
