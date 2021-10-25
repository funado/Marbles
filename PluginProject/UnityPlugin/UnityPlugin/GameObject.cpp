// Arthiran Sivarajah - 100660300 : GameObject.cpp

#include "GameObject.h"
#include <stdlib.h>

GameObject::GameObject()
{
	SetID();
	SetPosition();
}

int GameObject::GetID() const
{
	return m_id;
}

void GameObject::SetID(const int id)
{
	m_id = id;
}

Vector3D GameObject::GetPosition() const
{
	return m_position;
}

void GameObject::SetPosition(const float x, const float y, const float z)
{
	Vector3D newPos = GenerateRandomPosition();
	m_position.x = newPos.x;
	m_position.y = y;
	m_position.z = newPos.z;
}

Vector3D GameObject::GenerateRandomPosition()
{
	m_randPosition.x = (rand() % (int)((RandomRange.y - RandomRange.x)) + RandomRange.x);
	m_randPosition.y = (rand() % (int)((RandomRange.y - RandomRange.x)) + RandomRange.x);
	m_randPosition.z = (rand() % (int)((RandomRange.y - RandomRange.x)) + RandomRange.x);

	return m_randPosition;
}
