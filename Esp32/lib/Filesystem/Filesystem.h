#ifndef __FILESYSTEM_H__
#define __FILESYSTEM_H__

#include <Arduino.h>

struct Valve;
struct Event;

class Filesystem
{
public:
    bool init();
    bool addValve(const Valve& valve) const;

private:
    void reset() const;
    bool readValvesFile(std::vector<Valve>& valves) const;
    bool writeValvesFile(const std::vector<Valve>& valves) const;
    
    bool readEventsFile(std::vector<Event>& events) const;
    bool writeEventsFile(const std::vector<Event>& events) const;

    template<typename T>
    bool readStructVectorFromFile(const char* path, std::vector<T>& items) const;
    template<typename T>
    bool writeStructVectorToFile(const char* path, const std::vector<T>& items) const;
};

#endif /* __FILESYSTEM_H__ */