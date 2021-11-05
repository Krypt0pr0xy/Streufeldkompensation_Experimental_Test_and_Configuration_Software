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

***
### How to setup the Test Software:
1. Connect to the Mesure Device (1) and select the Device ID. You need to Install NI Max to connect to the DMM6500 
2. Connect to the DAC by selecting the COM Port (3)
3. Open the Serial Port
4. Select the Channel you want to mesure (6)
5. Press start to initialize mesure process
6. When the mesurment is finished you can select the data by `Save Values` this are the Offset and Slope values. By Pressing the `write data to File` Button you can save the raw mesured data. If you want to save th Chart press `Save Picture of Chart`.
***
## Calibration
1. To calibrate the DAC box, save the data from the Save Values for each channel and for each output voltage setting (+/- 1V and +/-10V).
2. Install Code Composer Studio. Instructions can be found at the following [link][5].
3. Import Master Project. [Link][6].
4. Search in the project for the following file: [Streufeldkompensation_function.h][7].
5. The slope and offset values must be inserted on the following lines:
***
```C
//      +/- 10V
#define CH1_AlphaA_10 1.0001088384669
#define CH1_AlphaB_10 -0.000074437497073571

#define CH2_AlphaA_10 1.00006678236934
#define CH2_AlphaB_10 -0.000102876570730844

#define CH3_AlphaA_10 0.999930388745645
#define CH3_AlphaB_10 0.000192725953658485

#define CH4_AlphaA_10 0.999968902926829
#define CH4_AlphaB_10 0.0000795440404880362

#define CH5_AlphaA_10 1
#define CH5_AlphaB_10 0

#define CH6_AlphaA_10 1
#define CH6_AlphaB_10 0

#define CH7_AlphaA_10 1
#define CH7_AlphaB_10 0

#define CH8_AlphaA_10 1
#define CH8_AlphaB_10 0

//      +/-1V
#define CH1_AlphaA_1 0.999967053101045
#define CH1_AlphaB_1 -0.00000481405541466135

#define CH2_AlphaA_1 0.999953392473868
#define CH2_AlphaB_1 -0.0000102498604878234

#define CH3_AlphaA_1 0.999861222682927
#define CH3_AlphaB_1 0.000016563681951194

#define CH4_AlphaA_1 0.999933929581882
#define CH4_AlphaB_1 0.00000790920397566669

#define CH5_AlphaA_1 1
#define CH5_AlphaB_1 0

#define CH6_AlphaA_1 1
#define CH6_AlphaB_1 0

#define CH7_AlphaA_1 1
#define CH7_AlphaB_1 0

#define CH8_AlphaA_1 1
#define CH8_AlphaB_1 0
```
The AlphaA Values are the slope values and AlphaB are offset 
***
6. When all values are inserted you have to compile the code, this is done via the hammer symbol.
7. After that you have to connect the MSP430 Launchpad. It is important that the Ground / RST and TEST are connected. In addition, the jumper settings should be set as shown in the picture. 
***
### MSP430 Launchpad
![Image][8]
***
### Spy by wire connection
![Image][9]
***
8. After everything is connected you can download the program code to the controller by pressing the Flash Button.
9. As a tip, take a look at the console to check if the writing was successful.
***
If you dont want to use the correction you can simply comment out this line of code `#define CORRECTION` or set the AlphaA value to 1 and AlphaB to 0.
These lines are in the same file as mentioned before.  [Streufeldkompensation_function.h][7]
***

[1]:https://github.com/Krypt0pr0xy/Streufeldkompensation
[2]:https://github.com/Krypt0pr0xy/Streufeldkompensation_Official_Software
[3]:https://github.com/Krypt0pr0xy/Streufeldkompensation_Experimental_Test_and_Configuration_Software/blob/master/bin/Debug/TEST_Software_V3.exe
[4]:https://github.com/Krypt0pr0xy/Streufeldkompensation_Experimental_Test_and_Configuration_Software/blob/master/Streufeldkompensation_Test_and_Configuration_Software_GUI.JPG
[5]:https://github.com/Krypt0pr0xy/Streufeldkompensation/blob/master/CodeComposerStudio_install.md
[6]:https://github.com/Krypt0pr0xy/Streufeldkompensation/blob/master/add_project_to_CCS.md
[7]:https://github.com/Krypt0pr0xy/Streufeldkompensation/blob/master/Streufeldkompensation_Master_V1/Streufeldkompensation_function.h
[8]:https://github.com/Krypt0pr0xy/Streufeldkompensation/blob/master/MSP430_Launchpad.jpg
[9]:https://github.com/Krypt0pr0xy/Streufeldkompensation/blob/master/spy_by_wire_connection.jpg

