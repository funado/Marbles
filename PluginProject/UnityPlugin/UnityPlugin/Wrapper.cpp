// Arthiran Sivarajah - 100660300 : Wrapper.cpp

#include "Wrapper.h"
#include "GameObject.h"

GameObject gameObject;

PLUGIN_API int GetID()
{
	return gameObject.GetID();
}

PLUGIN_API void SetID(int id)
{
	gameObject.SetID(id);
}

PLUGIN_API Vector3D GetPosition()
{
	return gameObject.GetPosition();
}

PLUGIN_API void SetPosition(float x, float y, float z)
{
	gameObject.SetPosition(x, y, z);
}

PLUGIN_API Vector3D GenerateRandomPosition()
{
	return gameObject.GenerateRandomPosition();
}
