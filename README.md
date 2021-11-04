# Streufeldkompensation Test and Configuration Software
***
***

**This program belongs to the [Streufeldkompensation][1] / [Streufeldkompensation_Official_Software][2]  project.** It is intended for the developers and testers. This software is used for the calibration of the micro controller. It can be used exclusively with the **DAC20Bit** and the **DMM6500 6 1/2 Digital Multimerter**. 

This program measures the output voltage from the respective DAC channels. Afterwards the difference in voltage is displayed in a chart. Through this you can find out the resolution and accuracy.  But before that, the measured channel must be linearized. This software includes the function to calculate this. These parameters can then be inserted into the C program of the MSP430.
***

# How to Calibrate th DAC
The very first thing you need to do is download the application. The Software is found under this [link][3].

[1]:https://github.com/Krypt0pr0xy/Streufeldkompensation
[2]:https://github.com/Krypt0pr0xy/Streufeldkompensation_Official_Software
[3]:https://github.com/Krypt0pr0xy/Streufeldkompensation_Experimental_Test_and_Configuration_Software/blob/master/bin/Debug/TEST_Software_V3.exe
