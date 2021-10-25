// Arthiran Sivarajah - 100660300 : SaveLevel.h

#pragma once
#ifndef __SAVE_LEVEL__
#define __SAVE_LEVEL__

#include "PluginSettings.h"
#include <fstream>
#include <iostream>
#include <string>
#include <vector>
using namespace std;


class PLUGIN_API SaveLevel
{
public:
	ofstream WriteFile;

	void SaveToFile(float objectNumber, float locationx, float locationy, float locationz, float rotationx,
		float rotationy, float rotationz, float scalex, float scaley, float scalez);
	float LoadFromFile(int j, const char* fileName);
	void StartWriting(const char* fileName);
	void EndWriting();
	int GetLines(const char* fileName);

	vector <float> line;

};

#endif /* Defined (__SAVE_LEVEL__) */