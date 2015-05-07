//
//////////////////////////////////////////////////////////////////////////////
char toclean[] = {'#', '\r', '\n'}; // Caracters to wipe out
char novoie;                     // No Voie
char action ;                    // Action
char Alst;
String Alarm_Lvl = "" ;          // Alarm Level (set)
String inputString = "";         // a String (Object) to hold incoming data
boolean stringComplete = false;  // whether the string is complete
boolean state = false;
boolean mustblink = false;       // whether the Led must blink
boolean AlarmIsOn = false;		 // whether the Alarm led is on
boolean on1;
boolean on2;
long  meancount = 0;             // nbr of value
long  meansum = 0;               // sum of value
long  meanvalue = 0;             // mean value

//
// Instantiate Events objects as an arry
int EventArray[MaxWay];
// Instantiate button objects as an array
ButtonHandler ButtonArray[MaxWay] = {ButtonHandler(BUTTON1_PIN), ButtonHandler(BUTTON2_PIN, DEFAULT_LONGPRESS_LEN * 2) };
// Instantiate Led objects as an array
LedHandler LedArray[MaxWay] = {LedHandler(LED1_PIN, OUT1_PIN, EEadrb + 0, DEFAULT_AlARM_LEVEL ), LedHandler(LED2_PIN, OUT2_PIN, EEadrb + 02, DEFAULT_AlARM_LEVEL) };
// Instantiate Blinker objects
Blinker MyBlinker(cp1, cp2);         // to manage blinking state
Blinker DoMean(10, 4990);            // to get the mean of analog value (every 5 sec)
//
//
void InitTx() {
  inputString = "";
  stringComplete = false;
}


//
//
void setup()
{
  // on reset, blink Alarm
  pinMode(LEDERROR, OUTPUT);
  digitalWrite(LEDERROR, HIGH);
  delay(500);
  digitalWrite(LEDERROR, LOW);
  // init serial communication via USB
  Serial.begin(9600);
  // init buttons and Leds pins; I suppose it's best to do here
  for (int ix = 0; ix < MaxWay; ix++) {
    ButtonArray[ix].init();
    LedArray[ix].init();
    LedArray[ix].read_Alarm_level_from_EEprom();
  }
  MyBlinker.init();
  DoMean.init();

  InitTx();
  //pinMode(LEDERROR, OUTPUT);
  //digitalWrite(LEDERROR, HIGH);
  Serial.println("** Spox Initialized");
#if debug && false
  mdbg();
#endif
}
// Include Here service functions used in Loop process
//
///////////////////////////////////////////////////////////////////////

void process_usb_message()
{
  // handle transmission
  if (!DecodeString()) {                     //Decode as novoie and action
    return;                                  // Nothing : Blank line
  }
  switch (action) {
    case '1':                                // action =  on / off
    case '0':
      state = false;
      if (action == '1') {
        state = true;
      }
      switch (novoie) {
        case '0':
          LedArray[0].onoff(state);			//novoie = 0 Both way are concerned
          LedArray[1].onoff(state);			//novoie = 0 Both way are concerned
          Serial.print(novoie);
          Serial.println(action);
          break;
        default:
#if debug && false
          mdbg();
          Serial.print("traite ");
          Serial.print(int(novoie) - 48);
          Serial.print(" ");
          Serial.print(bolstr(state));
          Serial.println(action);
#endif
          LedArray[int(novoie) - 49].onoff(state); //novoie # 0 Just the way  concerned
#if debug && true
          mdbg();
#endif
      }
      Serial.print(novoie);
      Serial.println(action);						// Reply with CRLF
      break;
    case '?':										// action = query
      Serial.print(novoie);
      Serial.println(LedArray[int(novoie) - 49].ison());
      break;
    case 'A':										// action = Analog
      switch (novoie) {
        case '0':
          Serial.print("An");
          Serial.println(String(meanvalue));         //novoie = 0 Query for analog value
          break;
        default:									 //novoie # 0 Set Alarm_Lvl
          LedArray[int(novoie) - 49].setAlarm(Alarm_Lvl.toInt());       //novoie # 0 Just the way  concerned
          Serial.println("As");
      }
      break;
    case 'X':										// action = Analog
      switch (novoie) {
        case '0':
          Alst = '0';
          if (AlarmIsOn) {
            Alst = '1';
          }
          Serial.print("X");
          Serial.println(Alst);					//novoie = 0 Query for analog value
          break;
        default:									//novoie # 0 Set Alarm_Lvl
          LedArray[int(novoie) - 49].setAlarm(Alarm_Lvl.toInt());       //novoie # 0 Just the way  concerned
      }
      break;
    case '!':
      // force le reset de l'eeprom
      LedArray[0].setAlarm(0);
      LedArray[1].setAlarm(0);
      break;
    default:
      Serial.print("SPOX Alarme 2 ");
      Serial.print(novoie);
      Serial.println(action);
  }
}
//
///////////////////////////////////////////////////////////////////////
void process_local_button() {
  // handle local button
  for (int ix = 0; ix < MaxWay; ix++) {
    EventArray[ix] = ButtonArray[ix].handle();
    switch (EventArray[ix]) {
      case EV_NONE:                  // ignore
        break;
      case EV_SHORTPRESS:            // ignore
        break;
      case EV_LONGPRESS:             // Process the case
        LedArray[ix].onoff(!LedArray[ix].Led_Is_On);
        break;
      default:
        Serial.println("SPOX Alarme 1");
        break;
    } // Switch
    EventArray[ix] = EV_NONE;        // Reset processed event
  } // for
}
//
///////////////////////////////////////////////////////////////////////
void ManageAnalogMean() {
  ++meancount;
  meansum = meansum + analogRead(ANA1_PIN);
  meanvalue = meansum / meancount;
}
//
////////////////////////////////////////////////////////////////////////
void DisplayError() {
  on1 = LedArray[0].Led_Is_On && !(LedArray[1].Led_Is_On);
  on2 = !(LedArray[0].Led_Is_On) && (LedArray[1].Led_Is_On);
  digitalWrite(LEDERROR, LOW);
  AlarmIsOn = false;
  if ((on1 && LedArray[0].Spox_Alarm) || (on2 && LedArray[1].Spox_Alarm)) {
    AlarmIsOn = true;
    digitalWrite(LEDERROR, HIGH);
  }

}
