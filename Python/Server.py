from MqttClient import MqttClient
from LCDManager import LCDManager
from ValveManager import ValveManager, Valve


class Server(object):
    def __init__(self, client_id='GoldenFishPython'):
        self.display = LCDManager()
        self.valves_manager = ValveManager()
        self.client = MqttClient(client_id=client_id, display=self.display)
        self.is_running = self.client.connect()
        if self.is_running:
            self.display.clear_line(2)
            self.display.write_line(1, "Connected to server")
            self.display.write_line(3, "Running..")
            self.display.set_backlight(False)

    def run(self):
        while self.is_running:
            pass
