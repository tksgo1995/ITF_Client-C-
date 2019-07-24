#pragma once
#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include <WinSock2.h>
#include<tchar.h>
#include <iostream>
#pragma comment (lib , "ws2_32.lib")
using namespace std;

#define MASK_SIZE 4
#define ERROR_SIZE 128
#define GET_BIT(w, k) (((w) >> (k)) & 0x01)

void InitProcess(int* mask);
int ConnectSocket(const char* ip, int port);
void UpdatePolicy(int mask);
void RegSet(HKEY hk, const TCHAR* path, int value, const TCHAR* setValue);