# Streufeldkompensation Test and Configuration Software
***
***

**This program belongs to the [Streufeldkompensation][1] / [Streufeldkompensation_Official_Software][2]  project.** It is intended for the developers and testers. This software is used for the calibration of the micro controller. It can be used exclusively with the **DAC20Bit** and the **DMM6500 6 1/2 Digital Multimerter**. 

This program measures the output voltage from the respective DAC channels. Afterwards the difference in voltage is displayed in a chart. Through this you can find out the resolution and accuracy.  But before that, the measured channel must be linearized. This software includes the function to calculate this. These parameters can then be inserted into the C program of the MSP430.
***

# How to Calibrate th DAC
The very first thing you need to do is download the application. The Software is found under this [link][3].

## Software explanation
![Image][4]
***
Number | Description 
--- | ---
 1\. | This Buttons connect/disconnect the Messuredevice, currently only works the Keithley DMM6500
 2\. | IF you press this button you should see the mesured voltage
 3\. | Serial port Selection, if you are unsure which port to use, you can open the Device Manager under Windows. 
 4\. | Serial Open and Close Button
 5\. | The Help button sends the Help command via UART when connected to the device, the DAC sends back a message.
 6\. | This setting sets the channel. 1-8
 7\. | These buttons let you set the voltage range.
 8\. | This function can set the Output resistor
 9\. | This setting sets the voltage at the respective channel
 10\. | The Send command is used to transmit the data to de DAC.
 11\. | This Buttons Starts and Stops the mesure process
 12\. | The nummeric input will set how many points will be mesured
 13\. | with this values you can set the start and stop Voltage
 14\. | On the Data block you can get to the calculated / stored Values.


[1]:https://github.com/Krypt0pr0xy/Streufeldkompensation
[2]:https://github.com/Krypt0pr0xy/Streufeldkompensation_Official_Software
[3]:https://github.com/Krypt0pr0xy/Streufeldkompensation_Experimental_Test_and_Configuration_Software/blob/master/bin/Debug/TEST_Software_V3.exe
[4]:https://github.com/Krypt0pr0xy/Streufeldkompensation_Experimental_Test_and_Configuration_Software/blob/master/Streufeldkompensation_Test_and_Configuration_Software_GUI.JPG
