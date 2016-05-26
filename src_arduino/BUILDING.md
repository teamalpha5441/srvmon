1) Include [U8glib](https://github.com/olikraus/u8glib) library in Arduino IDE
2) Disconnect the capacitor between RESET and GND
3) Build and Upload to Arduino
4) Connect the capacitor again

If disconnecting the capacitor is not possible, see
http://playground.arduino.cc/Main/DisablingAutoResetOnSerialConnection
for a solution on programming the Arduino without disconnecting the capacitor.
