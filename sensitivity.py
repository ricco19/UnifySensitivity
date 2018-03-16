#!/usr/bin/python
import sys

# Source/Quake Engine Conversions
# Sensitivity * Yaw (0.022) * DPI = Degrees/Inch
def source_get_inper(sense, dpi = 400, yaw = 0.022):
    return 360 / (sense * dpi * yaw)
def source_get_sense(inper, dpi = 400, yaw = 0.022):
    return 360 / (inper * dpi * yaw)
# Overwatch Conversions
# Sensitivity * Yaw (0.0066) * DPI = Degrees/Inch
OW_YAW = 0.0066
def ow_get_inper(sense, dpi = 400):
    return 360 / (sense * dpi * OW_YAW)
def ow_get_sense(inper, dpi = 400):
    return 360 / (inper * dpi * OW_YAW)


y = 100 * ((log10(c) + (269897.0/100000.0)) / 2)
# PUBG Conversions
# Best estimation we have is Source Sense / 101.0101 = PUBG Converted
# The in game 0-100 value is a logarithmic representation of the converted
