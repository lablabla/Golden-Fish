import os
import logging
import logging.handlers as handlers
import sys


def get_logger(name: str, level=logging.DEBUG, include_console=True, console_level=logging.DEBUG):
    if not os.path.exists('logs'):
        os.makedirs('logs')
    logger = logging.getLogger(name)
    logger.setLevel(level)
    formatter = logging.Formatter('%(asctime)s | %(name)-10s | %(levelname)-8s | %(lineno)04d | %(message)s')
    handler = handlers.TimedRotatingFileHandler('logs/golden_fish.log', when='D', interval=1, backupCount=5)
    handler.setFormatter(formatter)

    handler.setLevel(level)
    handler.setFormatter(formatter)
    logger.addHandler(handler)
    if include_console:
        console = logging.StreamHandler(sys.stdout)
        console.setFormatter(formatter)
        console.setLevel(console_level)
    logger.addHandler(console)

    return logger

