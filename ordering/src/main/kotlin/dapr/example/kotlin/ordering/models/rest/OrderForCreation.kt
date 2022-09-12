package dapr.example.kotlin.ordering.models.rest

import java.time.LocalDate

data class OrderForCreation(
    val orderId:String,
    val date:String,
    val customerDetails: CustomerDetails,
    val lines:List<OrderLine>
    )



