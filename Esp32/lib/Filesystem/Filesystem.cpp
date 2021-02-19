#include "Filesystem.h"
#include "Structures.h"
#include <SPIFFS.h>

const char* valvesFile = "/valves.bin";
const char* eventsFile = "/events.bin";

bool Filesystem::init()
{
    if(!SPIFFS.begin(true)){
        Serial.println("An Error has occurred while mounting SPIFFS");
        return false;
    }
    return true;
}


bool Filesystem::addValve(const Valve& valve) const
{
    //removeValvesFile();
    std::vector<Valve> valves;
    readValvesFile(valves);
    bool exists = false;
    for (int i = 0; i < valves.size(); i++)
    {
        if (valves[i].id == valve.id)
        {
            valves[i].gpio = valve.gpio;
            strcpy(valves[i].description, valve.description);
            exists = true;
            Serial.printf("Found matching valve with id %d, replacing.\n", valve.id);
            break;
        }
    }
    if (!exists)
    {
        Serial.printf("Adding new valve with id %d\n", valve.id);
        valves.push_back(valve);
    }
    Serial.printf("Writing %d valves to file\n", valves.size());
    return writeValvesFile(valves);
}

bool Filesystem::readValvesFile(std::vector<Valve>& valves) const
{    
    return readStructVectorFromFile<Valve>(valvesFile, valves);
}

bool Filesystem::readEventsFile(std::vector<Event>& events) const
{
    return readStructVectorFromFile<Event>(eventsFile, events);
}

bool Filesystem::writeEventsFile(const std::vector<Event>& events) const
{
    return writeStructVectorToFile<Event>(eventsFile, events);
}

void Filesystem::reset() const
{
    SPIFFS.remove(valvesFile);
    SPIFFS.remove(eventsFile);
}

bool Filesystem::writeValvesFile(const std::vector<Valve>& valves) const
{
    return writeStructVectorToFile<Valve>(valvesFile, valves);
}

template<typename T>
bool Filesystem::readStructVectorFromFile(const char* path, std::vector<T>& items) const
{
    File file = SPIFFS.open(path, "rb");
    if(!file){
        Serial.printf("Failed to open file %s for reading", path);
        return false;
    }
    size_t numOfItems = 0;
    size_t read = file.readBytes((char*)&numOfItems, sizeof(size_t));
    if (read != sizeof(size_t))
    {
        Serial.println("Failed reading number of items");
        file.close();
        return false;
    }
    Serial.printf("Reading %d items from file\n", numOfItems);
    for (int i = 0; i < numOfItems; i++)
    {
        T item;
        read = file.readBytes((char*)&item, sizeof(T));
        if (read != sizeof(T))
        {
            Serial.printf("Failed reading item number %d\n", i);
            file.close();
            return false;
        }
        items.push_back(item);
    }
    file.close();
    return true;
}

template<typename T>
bool Filesystem::writeStructVectorToFile(const char* path, const std::vector<T>& items) const
{
    File file = SPIFFS.open(path, "wb");
    if(!file){
        Serial.println("Failed to open file for reading");
        return false;
    }
    size_t numOfItems = items.size();
    size_t written = file.write((const uint8_t*)&numOfItems, sizeof(size_t));
    if (written != sizeof(size_t))
    {
        Serial.println("Failed writing number of items");
        file.close();
        return false;
    }
    for (const auto& item : items)
    {
        written = file.write((const uint8_t*)&item, sizeof(T));
        if (written != sizeof(T))
        {
            Serial.println("Failed writing item");
            file.close();
            return false;
        }
    }
    file.close();
    return true;
}