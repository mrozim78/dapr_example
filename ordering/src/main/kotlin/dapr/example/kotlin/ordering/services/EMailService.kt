package dapr.example.kotlin.ordering.services

import dapr.example.kotlin.ordering.models.EMail
import io.dapr.client.DaprClient
import io.dapr.client.DaprClientBuilder
import org.slf4j.LoggerFactory
import org.springframework.stereotype.Service
import reactor.core.publisher.Mono


@Service
class EMailService {
    private val logger = LoggerFactory.getLogger(EMailService::class.simpleName)

    fun sendEMail(email: EMail) {
        try{
            val client:DaprClient  = (DaprClientBuilder().build())
            logger.info("Try to send email")
            logger.info("EmailFrom:${email.emailFrom}")
            logger.info("EmailTo:${email.emailTo}")
            logger.info("Subject:${email.subject}");
            logger.info("Body:${email.body}")
            val metaDataEmail = mapOf("emailFrom" to email.emailFrom , "emailTo" to email.emailTo , "subject" to email.subject)

           client.invokeBinding("sendmail","create",email.body, metaDataEmail, Void.TYPE).block()
        } catch (e:Exception)
        {
            logger.error("Send email error",e)
        }
    }
}