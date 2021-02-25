import logging
import paho.mqtt.client as mqtt
import time
import yaml


class MqttClient(object):
    """
    Class wrapper around paho mqtt client
    """
    def __init__(self, client_id, config_file='configs/default.yml'):
        with open(config_file, 'r') as f:
            self.config = yaml.load(f, Loader=yaml.FullLoader)
        self.client = mqtt.Client(client_id=client_id)
        self.client.username_pw_set(username=self.config['username'], password=self.config['password'])
        self.client.on_connect = self.on_connect
        self.client.on_subscribe = self.on_subscribe
        self.client.on_message = self.on_message
        self.connected_flag = False
        self.connection_attempts = 0

    def __del__(self):
        self.client.loop_stop()

    def connect(self):
        try:
            self.client.loop_start()
            while not self.connected_flag:  # wait in loop
                if self.connection_attempts > 2:
                    raise RuntimeError("Too many failed connection attempts")
                self.connection_attempts += 1
                self.client.connect(host=self.config['hostname'], port=self.config['port'])
                logging.info("Trying to connect")
                time.sleep(1)
        except RuntimeError as ex:
            logging.exception(ex)
            return False
        return True

    def subscribe_to_topics(self):
        self.client.subscribe('golden_fish/commands/#')
        self.client.subscribe('golden_fish/test/#')

    def on_subscribe(self, client, userdata, mid, granted_qos, properties=None):
        print("Subscribed!")

    def on_connect(self, client, userdata, flags, rc):
        if rc == 0:
            self.connected_flag = True  # set flag
            print("connected OK Returned code=", rc)
            self.subscribe_to_topics()
        else:
            print("Bad connection Returned code=", rc)

    def on_message(self, client, userdata, message):
        print(f"Received message {message.payload} on topic {message.topic}")
