// Arthiran Sivarajah - 100660300 : Wrapper.cpp

#include "Wrapper.h"
#include "SaveLevel.h"

SaveLevel saveLevel;

void SaveToFile(float objectNumber, float locationx, float locationy, float locationz, float rotationx,
	float rotationy, float rotationz, float scalex, float scaley, float scalez)
{
	saveLevel.SaveToFile(objectNumber, locationx, locationy, locationz, rotationx,
		rotationy, rotationz, scalex, scaley, scalez);
}

float LoadFromFile(int j, const char* fileName)
{
	return saveLevel.LoadFromFile(j, fileName);
}

void StartWriting(const char* fileName)
{
	saveLevel.StartWriting(fileName);
}

void EndWriting()
{
	saveLevel.EndWriting();
}

int GetLines(const char* fileName)
{
	return saveLevel.GetLines(fileName);
}
