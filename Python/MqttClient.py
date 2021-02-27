from LCDManager import LCDManager
import logging
import os
import paho.mqtt.client as mqtt
import sys
import time
import utils
import yaml


class MqttClient(object):
    """
    Class wrapper around paho mqtt client
    """
    def __init__(self, client_id: str, display: LCDManager=None, config_file: str ='configs/default.yml'):

        self.logger = utils.get_logger(__name__)
        self.logger.info('Initializing client')

        with open(config_file, 'r') as f:
            self.config = yaml.load(f, Loader=yaml.FullLoader)
        self.client = mqtt.Client(client_id=client_id)
        if {'username', 'password'}.issubset(self.config.keys()):
            self.client.username_pw_set(username=self.config['username'], password=self.config['password'])
        self.client.on_connect = self.on_connect
        self.client.on_subscribe = self.on_subscribe
        self.client.on_message = self.on_message

        self.client.connected_flag = False
        self.connection_attempts = 0
        self.display = display
        self.topics = [
            'golden_fish/commands/#',
            'golden_fish/test/#',
        ]

        self.on_valve_callback = None
        self.on_event_callback = None
        self.get_satus_callback = None

    def __del__(self):
        self.client.loop_stop()

    def connect(self):
        try:
            self.client.loop_start()
            self.client.connect(host=self.config['hostname'], port=self.config['port'])
            while not self.client.connected_flag:  # wait in loop
                if self.connection_attempts > 2:
                    raise RuntimeError("Too many failed connection attempts")
                self.connection_attempts += 1
                self.logger.debug("Trying to connect")
                time.sleep(1)
        except RuntimeError as ex:
            self.logger.exception(ex)
            return False
        return True

    def subscribe_to_topics(self):
        self.client.subscribe([(t, 2) for t in self.topics])

    def on_subscribe(self, client, userdata, mid, granted_qos, properties=None):
        self.logger.debug(f"Subscribed to {str(mid)} with QoS: {granted_qos}")

    def on_connect(self, client, userdata, flags, rc):
        if rc == 0:
            self.logger.debug(f"connected OK Returned code={rc}")
            client.connected_flag = True
            self.subscribe_to_topics()
        else:
            self.logger.debug(f"Bad connection Returned code={rc}")

    def on_message(self, client, userdata, message):
        self.logger.debug(f"Received message {message.payload} on topic {message.topic}")
        self.parse_message(message)

    def parse_message(self, message: str):
        topic, message = str(message.topic), str(message.payload)
        if topic.startswith('golden_fish/commands/'):
            levels = list(filter(None, topic[len('golden_fish/commands/'):].split('/')))
            if levels[0] == 'valve':
                if self.on_valve_callback is not None:
                    self.on_valve_callback(levels[1], message)
            elif levels[0] == 'event':
                if self.on_event_callback is not None:
                    self.on_event_callback(levels[1], message)
            elif levels[0] == 'status':
                if self.get_satus_callback is not None:
                    status = self.get_satus_callback(levels[1], message)
                    # TODO: publish the status

