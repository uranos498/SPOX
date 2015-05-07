/*
SPOX arduino driver.
Base on serial Reader
 */

// pins for LEDs:
const int calibLed = 2;
const int tungLed = 4;
const int errorLed = 3;

const int PBCalib = 6;
const int PBTung = 5;

//Pins for Out
const int xlr1Out = 8;
const int xlr2Out = 9;

//debugging
bool debug=false;// Be carefull Can do some extra things with serial communication.

//communication. see API.
int switchNumber;
int switchState;

//true if a command is passed.
boolean commandOK=false;

//command for led blinking
boolean calibLedBlink=false;
boolean tungLedBlink=false;
boolean toggleLed=false;

//PB commutator
boolean pb1=false;
boolean pb2=false;


//switch for reset, if PB1 and PB2 are presed together.
boolean reset=true;
boolean stringComplete=false;
boolean serialReceived=false;

int timeOfCommand;
int commandSend=0;


String backMessage="";
String inputString;

void setup() {
  // initialize serial:
  Serial.begin(9600);
  timeOfCommand=millis();
  // define the pins outputs:
  pinMode(calibLed, OUTPUT);
  pinMode(tungLed, OUTPUT);
  pinMode(errorLed, OUTPUT);
  pinMode(xlr1Out, OUTPUT);
  pinMode(xlr2Out, OUTPUT);
  
  // define the pins inputs:
  pinMode(PBCalib, INPUT);    
  pinMode(PBTung, INPUT);    
  
  //write defaults values.
  digitalWrite(xlr1Out, LOW);
  digitalWrite(xlr2Out, LOW);
  digitalWrite(tungLed, LOW);
  digitalWrite(calibLed, LOW);
  digitalWrite(errorLed, LOW);
  
  // INITIALIZE TIMER INTERRUPTS. Needed for blinking. As is, no delay in loop "loop".
  cli(); // disable global interrupts

  TCCR1A = 0; // set entire TCCR1A register to 0
  TCCR1B = 0; // same for TCCR1B

  //change here to BLINK FASTER
  OCR1A = 15624; // set compare match register to desired timer count. 16 MHz with 1024 prescaler = 15624 counts/s
  TCCR1B |= (1 << WGM12); // turn on CTC mode. clear timer on compare match

  TCCR1B |= (1 << CS10); // Set CS10 and CS12 bits for 1024 prescaler
  TCCR1B |= (1 << CS12);

  TIMSK1 |= (1 << OCIE1A); // enable timer compare interrupt

  sei(); // enable global interrupts
  
}

boolean test(String string){
    if (string.length()!=3){
        return false;
    } 
   // if ((string[0]<48)||(string[0]>50)||(string[1]<48)||(string[1]<49)){return false;}
    
    return true;
}

void loop() {
    
     if (stringComplete) {
          if (debug){
            Serial.print("String Complete "); 
            Serial.print(inputString); 
            Serial.print(" "); 
            Serial.println(inputString.length());
          }
          
          if (test(inputString)){
              switchNumber = inputString[0]; 
              switchState = inputString[1];
              serialReceived=true; 
          } else {

          Serial.print("USBRC4ALPY, invalid Query. String : ");
          Serial.println(inputString);  
          }
          
          
          commandOK=true;
    // clear the string:
          inputString = "";
          stringComplete = false;
     }
     
     if ((millis()-timeOfCommand)>300){

           if (digitalRead(PBCalib)==HIGH){
               if (debug){
               Serial.println("Debug : PB1 pressed");}
               pb1=!pb1;
               commandOK=true;
               serialReceived=false;
               timeOfCommand=millis();
           } else if (digitalRead(PBTung)==HIGH){
               if (debug){
                 Serial.println("Debug : PB2 pressed");}
               pb2=!pb2;
               commandOK=true;
               serialReceived=false;
               timeOfCommand=millis();
                             
               }
  
     
    delay(20);//change here to change responsivness.
     
   
    if (commandOK) {
        if (serialReceived){
      switch (switchNumber) {
          case 48:     
          if (switchState==48){ //48== '0'
            
            if (debug){Serial.print("dark");
                          Serial.print("toogle calibLed");
                  Serial.print(calibLedBlink);
                  Serial.print("tungLed");
                  Serial.println(tungLedBlink);  
            }
            
                if (debug){
                  Serial.println("Reset all");
                  Serial.print("toogle calibLed");
                  Serial.print(calibLedBlink);
                  Serial.print("tungLed");
                  Serial.println(tungLedBlink);
                }    
               commandSend=0; 
            
          } else {Serial.println("USBRC4ALPY, invalid Query");}
          break;
          
          case 49:     //Calib Lamp
          if (switchState==48){
              commandSend=0;
          }
          else if (switchState==49) {
               if (debug){Serial.print("Received Calib Lamp Serial Command");
                          
            }
              commandSend=2;
          }
          
          else if (switchState==63) { // 63 -> ?
              Serial.print(1);
              Serial.println(digitalRead(xlr1Out));
          }
          
         break;
          
          case 50:   //flat Lamp
           if (switchState==48){
              commandSend=0;
          }
          else if (switchState==49) {
               if (debug){Serial.print("Received Flat Lamp Serial Command");
                          
            }
              commandSend=3;
          }
          else if (switchState==63) { // 63 -> ?
              Serial.print(2);
              Serial.println(digitalRead(xlr2Out));
              //digitalWrite(xlr1Out,HIGH);
              //digitalWrite(xlr2Out,HIGH);
              
          }
          
          break;
          
          case 51: //dark command
          if (switchState==48){
              commandSend=0;
          }
          else if (switchState==49) {
               if (debug){Serial.print("Received Dark Serial Command");
                          
            }
              commandSend=1;
          }
          else if (switchState==63) { // 63 -> ?
              Serial.print(3);
              Serial.println(digitalRead(xlr2Out));
              //digitalWrite(xlr1Out,HIGH);
              //digitalWrite(xlr2Out,HIGH);
              
          }
         
          default:
               Serial.println("USBRC4ALPY, invalid Query");
               break;
          
      } 
        } 
  
    //PB fonctions
    if (debug){
        Serial.print("DEBUG : ");  
        Serial.print(" pb1 : ");  
        Serial.print(pb1);
        Serial.print(" pb2 : ");  
        Serial.println(pb2);
    }
    if (!serialReceived){
          if (pb1&&pb2){
              commandSend=1;
              if (debug){
              Serial.println("pb1 && pb2 Pressed");
              }
          } else if (pb1 && !pb2 ){
              if (tungLedBlink){
                commandSend=0;
              } else {
              commandSend=2;
              }
              if (debug){
              Serial.println("pb1");
              }
          } else if (pb2 &&!pb1){
              if (tungLedBlink){
                commandSend=0;
              } else {
              commandSend=3;
              }
              if (debug){
              Serial.println("pb2");
              }
          } else if (!pb1 || !pb2){
              commandSend=0;
              if (debug){
              Serial.println("reset");
              }
          }
    }
    
    //command treatment
    switch (commandSend) {
     case 1: //dark
              if (debug){Serial.println("dark");}
              digitalWrite(xlr1Out,HIGH);
              digitalWrite(xlr2Out,HIGH);
              tungLedBlink=true;
              calibLedBlink=true;
              pb1=false;
              pb2=false;
              reset=true;
              break;
    
     case 2: //calib
              digitalWrite(xlr2Out,LOW);
              digitalWrite(tungLed,LOW);
              digitalWrite(xlr1Out,HIGH);
              digitalWrite(calibLed,HIGH);
              tungLedBlink=false;
              calibLedBlink=false;
              reset=true;
              if (debug){Serial.println("calib");}
              break;  
     
     case 3: //Flat
              digitalWrite(xlr2Out,HIGH);
              digitalWrite(tungLed,HIGH);
              digitalWrite(xlr1Out,LOW);
              digitalWrite(calibLed,LOW);
              tungLedBlink=false;
              calibLedBlink=false; 
              if (debug){Serial.println("flat");    }
              reset=true;
              break;
    
     case 0:  //All Off
              digitalWrite(xlr2Out,LOW);
              digitalWrite(tungLed,LOW);
              digitalWrite(xlr1Out,LOW);
              digitalWrite(calibLed,LOW);
              tungLedBlink=false;
              calibLedBlink=false;
              pb1=false;
              pb2=false;
              reset=false;
              if (debug){Serial.println("STOP");}
              break;  
    }     
  
  
  commandOK=false;
  delay(200);
  
}//end of commandOK state
}//end of millis
}//end of loop

void serialEvent() {
  while (Serial.available()) {
    // get the new byte:
    char inChar = (char)Serial.read(); 
    // add it to the inputString:
    inputString += inChar;
    // if the incoming character is a newline, set a flag
    // so the main loop can do something about it:
    if (inChar == '\n') {
      stringComplete = true;
    } 
  }
}


// TIMER VECTOR, gets called once a second (depends on prescaler and match register)
ISR(TIMER1_COMPA_vect)
{ toggleLed=!toggleLed;
  if (toggleLed){
    
  if (calibLedBlink){digitalWrite(calibLed,LOW);}
  
  
  if (tungLedBlink){digitalWrite(tungLed,LOW);}
  }
  else {
    if (calibLedBlink){digitalWrite(calibLed,HIGH);}
  
  
    if (tungLedBlink){digitalWrite(tungLed,HIGH);}
  
  }
  
  
}






