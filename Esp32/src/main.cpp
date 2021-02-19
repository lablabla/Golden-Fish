#include <WiFi.h>
#include "time.h"

#include "Filesystem.h"
#include "Structures.h"

const char* ssid       = "ssid";
const char* password   = "password";

const char* ntpServer = "pool.ntp.org";
const long  gmtOffset_sec = 2*3600;
const int   daylightOffset_sec = 3600;

Filesystem fs;

void printLocalTime()
{
  struct tm timeinfo;
  if(!getLocalTime(&timeinfo)){
    Serial.println("Failed to obtain time");
    return;
  }
  Serial.println(&timeinfo, "%A, %B %d %Y %H:%M:%S");
}

void setup()
{
  Serial.begin(115200);
  

  bool init = fs.init();
  if (init)
  {
    Serial.println("Filesystem intialized successfully");
  }
  else
  {
    Serial.println("Failed initializing filesystem. Exiting");
    return;
  }

  Valve v;
  v.id = 3;
  v.gpio = 35;
  if (fs.addValve(v))
  {
    Serial.println("Added Valve!");
  }
  else
  {
    Serial.println("Failed");
  }

  //connect to WiFi
  Serial.printf("Connecting to %s ", ssid);
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
      delay(500);
      Serial.print(".");
  }
  Serial.println(" CONNECTED");
  
  //init and get the time
  configTime(gmtOffset_sec, daylightOffset_sec, ntpServer);
  printLocalTime();

  //disconnect WiFi as it's no longer needed
  WiFi.disconnect(true);
  WiFi.mode(WIFI_OFF);
}

void loop()
{
  delay(1000);
  //printLocalTime();
}