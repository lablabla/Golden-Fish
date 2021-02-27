import liquidcrystal_i2c
from enum import Enum


class Justify(Enum):
    LEFT = 0,
    CENTER = 1,
    RIGHT = 2


class LCDManager(object):

    def __init__(self):
        self.cols = 20
        self.rows = 4

        self.lcd = liquidcrystal_i2c.LiquidCrystal_I2C(0x27, 1, numlines=self.rows)

        self.write_line(0, 'Golden Fish V0.1', Justify.CENTER)
        self.write_line(2, 'Initializing', Justify.CENTER)

    def write_line(self, line: int, message: str, justify: Justify = Justify.CENTER):
        if justify == Justify.LEFT:
            output_message = message.ljust(self.cols)
        elif justify == Justify.CENTER:
            output_message = message.center(self.cols)
        else:  # justify == Justify.RIGHT
            output_message = message.rjust(self.cols)

        self.lcd.printline(line, output_message)

    def clear_line(self, line: int):
        self.lcd.printline(line, ' ' * self.cols)

    def set_backlight(self, is_on):
        if is_on:
            self.lcd.backlight()
        else:
            self.lcd.noBacklight()

