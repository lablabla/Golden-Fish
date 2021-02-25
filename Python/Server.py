from MqttClient import MqttClient


class Server(object):
    def __init__(self, client_id='GoldenFishPython'):
        self.client = MqttClient(client_id=client_id)
        self.is_running = self.client.connect()

    def run(self):
        while self.is_running:
            pass
