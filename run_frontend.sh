#!/bin/sh
cd frontend/Dapr.Example.Dotnet.Frontend/Dapr.Example.Dotnet.Frontend
dapr run --app-id frontend --app-port 5164 --dapr-http-port 3500 --components-path ../../../dapr/components/ dotnet run


