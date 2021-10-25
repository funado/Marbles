// Arthiran Sivarajah - 100660300 : Wrapper.h

#pragma once
#ifndef __WRAPPER__
#define __WRAPPER__

#include "PluginSettings.h"
#include "Vector2D.h"
#include "Vector3D.h"

#ifdef __cplusplus
extern "C"
{
#endif

	PLUGIN_API void SaveToFile(float objectNumber, float locationx, float locationy, float locationz, float rotationx,
		float rotationy, float rotationz, float scalex, float scaley, float scalez);

	PLUGIN_API float LoadFromFile(int j, const char* fileName);

	PLUGIN_API void StartWriting(const char* fileName);

	PLUGIN_API void EndWriting();

	PLUGIN_API int GetLines(const char* fileName);

#ifdef __cplusplus
}
#endif

#endif /* defined (__WRAPPER__)*/
