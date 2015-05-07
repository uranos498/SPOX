// Loop: repetitive activity
//

void loop()
{
  // test for bserial message
  if (stringComplete) {              // We got an async message thru USB
    process_usb_message();
    InitTx();
    // we process it
  }
  else {
    process_local_button();          // Test and process button pressure
  }
  //
  //
  // update timer state: support function
  MyBlinker.handle();            // to generate the flashing wave
  DoMean.handle();               // to manage the mean of analog values
  //
  if (DoMean.ison()) {
    ManageAnalogMean();          // Capture les échantillons et calcule la moyenne
    DisplayError();
  }
  else
  {
    meancount = 0;               // Raz si pas de capture en cours
    meansum = 0;
  }
  //
  // Process led as required to have blinking output if any
  mustblink = LedArray[0].Led_Is_On && LedArray[1].Led_Is_On;   // if both ways are on, we blink
  for (int ix = 0; ix < MaxWay; ix++) {                         // on each way
    LedArray[ix].handle(MyBlinker.ison(), mustblink, meanvalue);          // we blink
  }
}

