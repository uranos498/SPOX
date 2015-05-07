ABout the use of the Spox box
-----------------------------

To be completed by Sheliack















About the modules installed
---------------------------

Spox_Button_Led (Arduino code)

Please!!! open Spox_Button_Led.ino  as main Module... Others will be chained
This is the Arduino Board software.

Spox_Gui
This is the Gui used to test the "COM" Dll. Ascom is not used/required

Spox_Gui <--> Spox_Dll <--> Arduino Board
This way of use is "proprietary" and not recommended.

Spox_Dll 
This is the COM module used to dialog with arduino via a serial com port.
This way of use is "proprietary" and not recommended for personal developments.
Spox_DLL.Dll is registred as COM Module by the installer or by your MeMean ;)


Spox_Test 
This is the GUI used to test the Ascom (Switch type) link with the arduino.

Spox_Test <--> ASCOM <--> Ascom.Switch.Spox.dll <--> Arduino Board
This way of use is "Ascom Standard Compliant" and should be prefered/recommended.
You can switch on/off (Set,Get) the Arduino output by controlling 
Ascom Switch [0,1] named '1', '2'.
You have access to Ascom "Action" (Use dummy parameter)
: READANALOG returns an integer [0-1023] as the analog value
: READALARM returns a Boolean about the Alarm light (True=on)

Spox 
The Ascom Driver : produces Ascom.Switch.Spox.dll.
Ascom.Switch.Spox.dll is registred (As Ascom and As COM) by the installer