package dapr.example.kotlin.ordering

import org.springframework.boot.autoconfigure.SpringBootApplication
import org.springframework.boot.runApplication

@SpringBootApplication
class OrderingApplication

fun main(args: Array<String>) {
	runApplication<OrderingApplication>(*args)
}
