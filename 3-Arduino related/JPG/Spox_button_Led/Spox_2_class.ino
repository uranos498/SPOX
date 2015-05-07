
//////////////////////////////////////////////////////////////////////////////
// Class definition
//////////////////////////////////////////////////////////////////////////////
// Class ButtonHandler
class ButtonHandler {
  public:
    // Constructor
    ButtonHandler(int pin, int longpress_len = DEFAULT_LONGPRESS_LEN);
    // Initialization done after construction, to permit static instances
    void init();
    // Handler, to be called in the loop()
    int handle();
  protected:
    const int pin;           // pin to which button is connected
    boolean was_pressed;     // previous state
    int pressed_counter;     // press running duration
    const int longpress_len; // longpress duration
};

ButtonHandler::ButtonHandler(int p, int lp) : pin(p), longpress_len(lp) {}
void ButtonHandler::init()
{
  pinMode(pin, INPUT);
  digitalWrite(pin, HIGH); // pull-up
  was_pressed = false;
  pressed_counter = 0;
}
int ButtonHandler::handle()
{
  int event;
  int now_pressed = !digitalRead(pin);

  if (!now_pressed && was_pressed) {
    // handle release event
    if (pressed_counter < longpress_len)
      event = EV_SHORTPRESS;
    else
      event = EV_LONGPRESS;
  }
  else
    event = EV_NONE;

  // update press running duration
  if (now_pressed)
    ++pressed_counter;
  else
    pressed_counter = 0;

  // remember state, and we're done
  was_pressed = now_pressed;
  return event;
}
//////////////////////////////////////////////////////////////////////////////
// Class LedHandler
//////////////////////////////////////////////////////////////////////////////
class LedHandler {
  public:
    // Constructor
    LedHandler(int pin_Led, int pin_Spox, int EEprom_Alarm_Level, int Alarm_Level = DEFAULT_AlARM_LEVEL );
    // Initialization done after construction, to permit static instances
    void init();
    // Handler, to be called in the loop()
    void handle(boolean BlinkPhase, boolean MustBlink, long meanv);
    void onoff(boolean state);
    void read_Alarm_level_from_EEprom();
    void setAlarm(int alarm);
    char ison();
    boolean Led_Is_On;       // Current state ias accessible from outside
    boolean Spox_Alarm;          // Current state
    int  Alarm_Level ;           // Analog level to trigg the alarm
  protected:
    const int pin_Led;           // pin to which Led is connected
    const int pin_Spox;          // pin to which device is connected
    const int EEadr;             // Possition EEPROM
    int wrk;
};
LedHandler::LedHandler(int pl, int ps,  int EEa, int al) : pin_Led(pl), pin_Spox(ps), EEadr(EEa), Alarm_Level(al) {}
void LedHandler::init()
{
  pinMode(pin_Led, OUTPUT);      //The Led is connected to OUTPUT Pin
  pinMode(pin_Spox, OUTPUT);     //The Spox is connected to OUTPUT Pin
  Led_Is_On = false;             // Initialized to off
  Spox_Alarm = false;            // Initialized to off

}
void LedHandler::read_Alarm_level_from_EEprom() {
  wrk = word(EEPROM.read(EEadr + 0), EEPROM.read(EEadr + 1));
  if (wrk>1 && wrk<1022) {
      Alarm_Level = wrk;
    }
}
      void LedHandler::setAlarm(int alarm)
{
  Alarm_Level = alarm;
  EEPROM.write( EEadr + 0, highByte(Alarm_Level) );
  EEPROM.write( EEadr + 1, lowByte(Alarm_Level) );
}
void LedHandler::onoff(boolean state)
{
  Led_Is_On = state;
}
char LedHandler::ison()
{
  if (Led_Is_On) {
    if (Spox_Alarm) {
      return '1';
    }
    else {
      return '1';
    }
  }
  else {
    return '0';
  }
}
void LedHandler::handle(boolean BlinkPhase, boolean MustBlink, long meanv)
{ // we now set the right voltage on the pins
  if (!Led_Is_On) {
    digitalWrite(pin_Led, LOW);
    digitalWrite(pin_Spox, LOW);
    return;
  }
  digitalWrite(pin_Spox, HIGH);
  digitalWrite(pin_Led, (MustBlink && BlinkPhase) || (!MustBlink && HIGH));
#if debug && false
  Serial.print(String(int(meanv)));
  Serial.print(" ** ");
  Serial.println(String(int(Alarm_Level)));
#endif
  if (int(Alarm_Level) > int(meanv)) {   // Intensity is Below... perhaps problem
    Spox_Alarm = true;
  }
  else   {
    Spox_Alarm = false;
  }
}
//////////////////////////////////////////////////////////////////////////////
// Class Blinker  generator of a public 10ms on 90ms off signal vawe
//////////////////////////////////////////////////////////////////////////////
//
class Blinker {
  public:
    // Constructor
    Blinker(unsigned long p1, unsigned long p2);
    // Initialization done after construction, to permit static instances
    void init();
    // Handler, to be called in the loop()
    void handle();
    boolean ison();              // Current state ias accessible from outside
  protected:
    boolean is_on;
    unsigned long next_time;               // millis for next action
    unsigned long curr_time;               // current millis
    unsigned long phase_1 = 100;           // duration of Phase ison millis
    unsigned long phase_2 = 900;           // duration of Phase isoff millis
};
Blinker::Blinker(unsigned long p1, unsigned long p2) : phase_1(p1), phase_2(p2) {}
void Blinker::init()
{
  is_on = true;            // Initialized to on
  next_time = millis() + phase_1;

}
boolean Blinker::ison()
{
  return is_on;
}
void Blinker::handle()
{
  curr_time = millis();
  if (curr_time < next_time) {
    return;
  }
  if (is_on) {
    next_time = curr_time + phase_2;
  }
  else {
    next_time = curr_time + phase_1;
  }
  is_on = !is_on;
}
//
//////////////////////////////////////////////////////////////////////////////
// END Class definition
//////////////////////////////////////////////////////////////////////////////

