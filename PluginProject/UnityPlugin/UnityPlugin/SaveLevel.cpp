// Arthiran Sivarajah - 100660300 : SaveLevel.cpp

#include "SaveLevel.h"

void SaveLevel::SaveToFile(float objectNumber, float locationx, float locationy, float locationz, float rotationx,
	float rotationy, float rotationz, float scalex, float scaley, float scalez)
{

	WriteFile << objectNumber << "\n";
	WriteFile << locationx << "\n";
	WriteFile << locationy << "\n";
	WriteFile << locationz << "\n";
	WriteFile << rotationx << "\n";
	WriteFile << rotationy << "\n";
	WriteFile << rotationz << "\n";
	WriteFile << scalex << "\n";
	WriteFile << scaley << "\n";
	WriteFile << scalez << "\n";
}

float SaveLevel::LoadFromFile(int j, const char* fileName)
{
	line.clear();
	ifstream myReadFile;
	myReadFile.open(fileName);
	float value;
	while (myReadFile >> value)
	{
		line.push_back(value);

	}
	myReadFile.close();
	return line[j];
}

void SaveLevel::StartWriting(const char* fileName)
{
	WriteFile.open(fileName);

}
void SaveLevel::EndWriting()
{
	WriteFile.close();
}

int SaveLevel::GetLines(const char* fileName)
{
	line.clear();
	ifstream myReadFile;
	myReadFile.open(fileName);
	int value = 0;
	string tempString;
	while (getline(myReadFile, tempString))
	{
		value++;
	}
	myReadFile.close();
	return value;
}