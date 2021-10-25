// Arthiran Sivarajah - 100660300 : GameObject.h

#pragma once
#ifndef __GAME_OBJECT__
#define __GAME_OBJECT__

#include "PluginSettings.h"
#include "Vector2D.h"
#include "Vector3D.h"

class PLUGIN_API GameObject
{
public:
	// Constructor
	GameObject();

	//Getters and Setters
	int GetID() const;
	void SetID(int id = 0);

	Vector3D GetPosition() const;
	void SetPosition(float x = 0.0f, float y = 0.0f, float z = 0.0f);

	Vector3D GenerateRandomPosition();

private:
	int m_id;
	Vector3D m_position;
	Vector3D m_randPosition;

	Vector2D RandomRange = { 5, 25 };
};

#endif /* Defined (__GAME_OBJECT__) */

