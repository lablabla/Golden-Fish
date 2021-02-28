from apscheduler.schedulers.background import BlockingScheduler
from apscheduler.jobstores.sqlalchemy import SQLAlchemyJobStore
import logging
import threading

from MqttClient import MqttClient
from LCDManager import LCDManager
from ValveManager import ValveManager, Valve


class Server(object):
    def __init__(self, client_id='GoldenFishPython'):
        self.display = LCDManager()
        self.valves_manager = ValveManager()
        jobstores = {
            'default': SQLAlchemyJobStore(url='sqlite:///jobs.sqlite')
        }
        logging.getLogger('apscheduler').setLevel(logging.DEBUG)
        self.scheduler = BlockingScheduler(jobstores=jobstores)
        self.client = MqttClient(client_id=client_id, scheduler=self.scheduler, display=self.display)

    def run(self):
        threading.Thread(target=self.client.connect).start()
        self.scheduler.start()  # This will block indefinitely
