package com.fitbot.service

import com.fitbot.data.Product
import com.fitbot.data.ProductType
import com.fitbot.utils.logger
import org.slf4j.Logger

class RunAfterStartup {
    companion object {
        private val log: Logger by logger()

        private fun createProducts(productService: ProductService) {
            log.info("### Create Products: Start")

            // For Caesar salad
            productService.create("Курица", ProductType.PRODUCT)
            productService.create("Лист салата", ProductType.PRODUCT)
            productService.create("Сухарики", ProductType.PRODUCT)
            productService.create("Помидоры Черри", ProductType.PRODUCT)
            productService.create("Сыр", ProductType.PRODUCT)
            productService.create("Соус цезарь", ProductType.SOUCE)

            // For Greece
            productService.create("Капуста", ProductType.PRODUCT)
            productService.create("Болгарский перец", ProductType.PRODUCT)
            productService.create("Оливки", ProductType.PRODUCT)
            productService.create("Маслины", ProductType.PRODUCT)
            productService.create("Помидоры", ProductType.PRODUCT)
            productService.create("Огурцы", ProductType.PRODUCT)
            productService.create("Оливковое масло", ProductType.SOUCE)

            log.info("### Create Products: Finish")
        }

        private fun createDishes(dishService: DishService) {
            log.info("### Create Dishes: Start")

            dishService.create(
                "Цезарь",
                listOf(
                    Product("Курица", ProductType.PRODUCT),
                    Product("Лист салата", ProductType.PRODUCT),
                    Product("Сухарики", ProductType.PRODUCT),
                    Product("Помидоры Черри", ProductType.PRODUCT),
                    Product("Сыр", ProductType.PRODUCT),
                    Product("Соус цезарь", ProductType.SOUCE)
                ),
                23,
                12,
                36,
                124
            )

            dishService.create(
                "Греческий",
                listOf(
                    Product("Капуста", ProductType.PRODUCT),
                    Product("Болгарский перец", ProductType.PRODUCT),
                    Product("Оливки", ProductType.PRODUCT),
                    Product("Маслины", ProductType.PRODUCT),
                    Product("Помидоры", ProductType.PRODUCT),
                    Product("Огурцы", ProductType.PRODUCT),
                    Product("Оливковое масло", ProductType.SOUCE)
                ),
                44,
                56,
                87,
                56
            )

            log.info("### Create Dishes: Finish")
        }

        fun startUp(dishService: DishService, productService: ProductService) {
            log.info("### StartUp: Start")

            createProducts(productService)
            createDishes(dishService)

            log.info("### StartUp: Finish")
        }
    }
}
