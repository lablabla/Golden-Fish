from gpiozero import DigitalOutputDevice
from models import Valve
import time
from typing import List
import utils


class ValveManager(object):

    def __init__(self, valves: List[Valve] = None):
        self.logger = utils.get_logger(__name__)
        self.logger.info(f"Initializing valve manager")
        self.control_dict = dict()
        self.valves = valves
        if valves is not None:
            for v in valves:
                self.add_valve(v)

    def add_valve(self, v: Valve):
        self.control_dict[v.id] = DigitalOutputDevice(v.gpio, active_high=False)
        self.control_dict[v.id].off()

