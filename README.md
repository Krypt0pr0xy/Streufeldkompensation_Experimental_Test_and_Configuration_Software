# Streufeldkompensation Test and Configuration Software

This program belongs to the Streufeldkompensation project. It is intended for the developers and testers. This software is used for the calibration of the micro controller. It can be used exclusively with the DAC20Bit and the DMM6500 6 1/2 Digital Multimerter. 

This program measures the output voltage from the respective DAC channels. Afterwards the difference in voltage is displayed in a chart. Through this you can find out the resolution and accuracy.  But before this can be measured the channel must be linearized. This software includes the function to calculate this. These parameters can then be inserted into the C program of the MSP430.
