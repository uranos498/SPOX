//
////////////////////////////////////////////////////////
// local debugger
///////////////////////////////////////////////////////
#if debug
void mdbg()
{
  Serial.print("** ");
  Serial.print("Rg");
  Serial.print ("Led_on ");
  Serial.print ("Alr_on ");
  Serial.print ("Alr_Lvl");
  Serial.println(" ** ");
  for (int ix = 0; ix < MaxWay; ix++) {                         // on each way
    Serial.print("** ");
    Serial.print(ix);
    Serial.print (bolstr(LedArray[ix].Led_Is_On));
    Serial.print (bolstr(LedArray[ix].Spox_Alarm));
    Serial.print (String(LedArray[ix].Alarm_Level ));
    Serial.println(" ** ");
  }
  Serial.println("************************ ** ");
}
#endif

