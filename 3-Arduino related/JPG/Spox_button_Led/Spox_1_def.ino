

//-------------------------------------------------------------------------------------
#define debug                   false


#define BUTTON1_PIN               5  // Button 1
#define BUTTON2_PIN               6  // Button 2

#define LED1_PIN                  2  // Led 1
#define LED2_PIN                  4  // Led 2
#define LED3_PIN                  3  // Led 3
#define LEDERROR                  3  // Led 3

#define OUT1_PIN                  8  // Sortie 1
#define OUT2_PIN                  9  // Sortie 2

#define ANA1_PIN                  0  // Capteur 1

#define DEFAULT_LONGPRESS_LEN    25  // Min nr of loops for a long press
#define DEFAULT_AlARM_LEVEL      50 // Max Analog Level on Analog Input before alarm
#define DELAY                    20  // Delay per loop in ms
#define MaxWay                   2   // Nbr of ways handled (Actually 2 : Calibration & Black)
#define NbToClean                3   // Nbr of chars in the toclean table
#define cp1                      100 // millis : length of phase 1
#define cp2                      900 // millis : length of phase 2

#define EEadrb                     0  // position EEPROM
//////////////////////////////////////////////////////////////////////////////

enum { EV_NONE = 0, EV_SHORTPRESS, EV_LONGPRESS, TX_REQ};

