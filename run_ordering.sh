#!/bin/sh
JAVA_RUN=/home/mrozi/.jdks/openjdk-18.0.2.1/bin/java
JAR_NAME=ordering-0.0.1-SNAPSHOT.jar
cd ordering/build/libs/
dapr run --app-id ordering --app-port 8080 --dapr-http-port 3502 --components-path  ../../../dapr/components/ -- $JAVA_RUN -jar $JAR_NAME