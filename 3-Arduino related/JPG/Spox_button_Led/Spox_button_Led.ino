#include <EEPROM.h>


// This unit of code is for the "SPOX" device produce by Sheliack Instruments

// Name Version Improvements
// JPG     1.0  Initial

// Maintenance approach:
// This is an Oriented Object (OO) conception.
// Objects: Button, Led, Blinker
//   Object Button: able to manage minimal length for pressure (Anti rebonds)
//   Object Led : Keep the state of the contacts
//   Object Blinker : Generate appropriate wave for cycle events (blinking, mean of analog value,...)

// Warning: external blinking state is generated as a "Logical And" between internal state of led and blinking Wave.


//Spox – Définition des entrées & sorties, hardware et logicielles									
//									
//	Button calib	Button flat	Current	Output Calib	LED Calib	Output Flat	LED Flat	LED error	
//Physical position	Left	Right	-	XLR conn.	Left	XLR conn.	Right	Center	
//PCB ref	PB2	PB1	-	J3-1	LED 1	J3-3	LED 3	LED2	
//I/O Arduino	D6	D5	A0	D8	D2	D9	D4	D3	
//Color (physical)	-	-	-	-	Yellow	N/A	Green	Red	
//Mode									
//Off	0	0	-	0	0	0	0	0	
//Calib	1	0	> 60mA	1	1	0	0	0	
//			< 60mA	1	1	0	0	Blink	
//Flat	0	1	> 60mA	0	0	1	1	0	
//			< 60mA	0	0	1	1	Blink	
//Dark	1	1	-	1	Blink	1	Blink	0	









//________________________________________________________________________________//
//
//Compile as "Arduino Pro Mini 5v, ATmega328"  for first serie of devices
//________________________________________________________________________________//
