package dapr.example.kotlin.ordering.controllers

import dapr.example.kotlin.ordering.models.rest.OrderForCreation
import dapr.example.kotlin.ordering.services.OrderingService
import io.dapr.Topic
import org.slf4j.LoggerFactory
import org.springframework.web.bind.annotation.PostMapping
import org.springframework.web.bind.annotation.RequestBody
import org.springframework.web.bind.annotation.RestController

@RestController
class OrderingController(val orderingService:OrderingService)
{

    private val logger = LoggerFactory.getLogger(OrderingController::class.simpleName)
    @Topic(name="orders" , pubsubName="pubsub")
    @PostMapping(path = ["/ordering"])
    fun ordering(@RequestBody cloudEvent:io.dapr.client.domain.CloudEvent<OrderForCreation>)
    {
        logger.info("Get order")
        logger.info("Source:"+cloudEvent.source)
        logger.info(cloudEvent.data.toString())
        orderingService.ordering(cloudEvent.data)
    }
}