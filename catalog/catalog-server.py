import datetime

import logging
from flask import Flask, jsonify
from flask.json import JSONEncoder
from os import environ
from dapr.clients import DaprClient
from dapr.clients.grpc._state import StateItem
from dapr.clients.grpc._request import TransactionalStateOperation, TransactionOperationType


class CustomJSONEncoder(JSONEncoder):
    def default(self, obj):
        try:
            if isinstance(obj, datetime.date):
                return obj.isoformat()
            iterable = iter(obj)
        except TypeError:
            pass
        else:
            return list(iterable)
        return JSONEncoder.default(self, obj)


app = Flask(__name__)
app.json_encoder = CustomJSONEncoder


def example_data():
    logging.warning("Example data catalog")
    if environ.get('DAPR_HTTP_PORT') is not None:
        with DaprClient() as client:
            secret = client.get_secret(store_name='secretstore', key='eventcatalogdb')
            logging.warning('Secret result:')
            logging.warning(secret.secret)
    now = datetime.datetime.now()
    days7 = now + datetime.timedelta(days=7)
    day3 = now + datetime.timedelta(days=3)
    days5 = now + datetime.timedelta(days=5)
    data = [
        {'EventId': 'CFB88E29-4744-48C0-94FA-B25B92DEA317', 'Name': 'ABBA concert', 'Price': 100, 'Artist': 'ABBA',
         'Date': days7, 'Description': 'ABB concert', 'ImageUrl': ''},
        {'EventId': 'CFB88E29-4744-48C0-94FA-B25B92DEA318', 'Name': 'Reload', 'Price': 300, 'Artist': 'Metallica',
         'Date': days5, 'Description': 'Battery', 'ImageUrl': ''},
        {'EventId': 'CFB88E29-4744-48C0-94FA-B25B92DEA319', 'Name': 'Thunder', 'Price': 150, 'Artist': 'AC/DC',
         'Date': day3, 'Description': 'Thunder Concert', 'ImageUrl': ''}
    ]
    return data


@app.route("/event")
def events():
    return jsonify(example_data())


@app.route("/event/<event_id>")
def event(event_id):
    data = example_data()
    data_filter = list(filter(lambda x: (x['EventId'] == event_id.upper()), data))
    return jsonify(data_filter[0])


app.run(port=5016)
