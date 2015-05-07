void serialEvent() {
  while (Serial.available()) {
    // get the new byte:
    char inChar = (char)Serial.read();
    // if the incoming character is a newline, set a flag
    // so the main loop can do something about it:
    if ((inChar == '\n') || (inChar == '#') || (inChar == '\r')) {
      stringComplete = true;
    }
    else {
      // add it to the inputString:
      inputString += inChar;
    }
  }
}
//
// Basic (to Arduino)
//  n0+rclf : Contact n to Off ; return n0+rclf / error                x
//  n1+rclf : Contact n to On ; return n1+rclf / error                 x
// Extensions:
//  n?+rclf  : Query state for contact n. ; return nx+rclf / error
//  0A+rclf  : Query state of analog Pin (0-1023)
//  nAdddd+rclf : set alarm level (dddd) for way n
//  0X+rclf  : Query state of Alarm Led (0-1023)
//  00+rclf  : All contacts off ; return 00+rclf / error               x
//Error/identification
//  SPOX+rclf  is returned by Arduino ie device name is given on invalid query
//

boolean DecodeString() {
  // nettoyage chaine: lf ou rc ou # en début
  for (int iy = 0; iy < NbToClean; iy++) {
    if (inputString.charAt(0) == toclean[iy]) {
      inputString.remove(0, 1);
    }
  }
  if (inputString.length() < 2 ) {
#if debug
    mdbg();
#endif
    return false;
  }
#if debug
  Serial.print("recu ");
  Serial.println(inputString);
#endif
  //Récupère 1er Char = no voie (1,2)
  novoie = inputString.charAt(0);
  //Récupère 2ème Char =
  action = inputString.charAt(1);
  // Get Alarm_Lvl
  Alarm_Lvl = inputString;
  Alarm_Lvl.remove(0, 2);
  return true;
}
String bolstr(boolean b) {
  if (b) {
    //      12345678
    return " True  ";
  }
  return " False ";

}

