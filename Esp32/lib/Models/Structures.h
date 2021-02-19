#ifndef __STRUCTURES_H__
#define __STRUCTURES_H__

#include <Arduino.h>


#pragma pack(1)
struct Valve
{
    uint16_t id;
    uint16_t gpio;
    char description[1024];
};

struct Event
{
    char startCron[128];
    char endCron[128];
    uint16_t valveId;
};

#endif /* __STRUCTURES_H__ */
