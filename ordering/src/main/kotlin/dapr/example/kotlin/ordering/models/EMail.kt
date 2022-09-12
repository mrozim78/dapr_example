package dapr.example.kotlin.ordering.models

data class EMail (val emailFrom:String,
             val emailTo:String,
             val subject:String,
             val body:String
        )
