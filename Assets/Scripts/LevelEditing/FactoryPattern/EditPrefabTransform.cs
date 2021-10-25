// Modified By: Arthiran Sivarajah - 100660300, Aaron Chan - 100657311

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditPrefabTransform : MonoBehaviour
{
    LevelEditorManager EditorManager;
    private void Awake()
    {
        EditorManager = FindObjectOfType<LevelEditorManager>();
    }

    private void OnMouseDown()
    {
        if (EditorManager.EditMode)
        {
            EditorManager.SelectedObject = gameObject;
            // Position
            EditorManager.SelectedPosition[0].text = transform.position.x.ToString();
            EditorManager.SelectedPosition[1].text = transform.position.y.ToString();
            EditorManager.SelectedPosition[2].text = transform.position.z.ToString();

            // Rotation
            EditorManager.SelectedRotation[0].text = transform.eulerAngles.x.ToString();
            EditorManager.SelectedRotation[1].text = transform.eulerAngles.y.ToString();
            EditorManager.SelectedRotation[2].text = transform.eulerAngles.z.ToString();

            // Scale
            EditorManager.SelectedScale[0].text = transform.localScale.x.ToString();
            EditorManager.SelectedScale[1].text = transform.localScale.y.ToString();
            EditorManager.SelectedScale[2].text = transform.localScale.z.ToString();
        }
    }
}
