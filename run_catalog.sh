#!/bin/sh
VENV_NAME=catalog_server_venv
cd catalog
#python3 -m venv $VENV_NAME
#source $VENV_NAME/bin/activate
#pip install -r requirements.txt
dapr run --app-id catalog --app-port 5016 --dapr-http-port 3501 --components-path ../dapr/components/ $VENV_NAME/bin/python catalog-server.py