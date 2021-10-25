// Content modified from Game Engine Design Tutorials
// Author: Parisa Sargolzaei

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class LevelEditorManager : MonoBehaviour
{
    Camera maincam;
    RaycastHit hitInfo;
    public Transform cubePrefab;

    public List<Transform> TrackPrefabs = new List<Transform>();

    private int ObjectIncrement = 0;
    private int MaxObjects = 0;

    [HideInInspector]
    public bool EditMode = false;

    [HideInInspector]
    public GameObject SelectedObject;

    public List<TMP_InputField> SelectedPosition = new List<TMP_InputField>();
    public List<TMP_InputField> SelectedRotation = new List<TMP_InputField>();
    public List<TMP_InputField> SelectedScale = new List<TMP_InputField>();

    public GameObject TransformBox;
    public GameObject TrackObjectBox;

    public TMP_Text CurrentObjectText;


    // Start is called before the first frame update
    void Awake()
    {
        maincam = Camera.main;
        MaxObjects = TrackPrefabs.Count - 1;
        CurrentObjectText.text = "Current Object: " + TrackPrefabs[ObjectIncrement].name;
    }

    // Update is called once per frame
    void Update()
    {
        if (!EditMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject(-1))
                {
                    return;
                }

                Ray ray = maincam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
                {
                    Color c = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
                    //CubePlacer.PlaceCube(hitInfo.point, c, cubePrefab);

                    ICommand command = new PlaceObjectCommand(hitInfo.point, c, TrackPrefabs[ObjectIncrement]);
                    CommandInvoker.AddCommand(command);
                }
            }
            
            /*if (Input.GetKeyDown(KeyCode.J))
            {
                ObjectIncrement = ObjectIncrement < MaxObjects ? ObjectIncrement + 1 : ObjectIncrement = 0;
            }*/
        }
    }

    public void ChangeObjectIndex(int index)
    {
        ObjectIncrement = index;
        CurrentObjectText.text = "Current Object: " + TrackPrefabs[ObjectIncrement].name;
    }

    public void ChangeEditMode()
    {
        EditMode = !EditMode ? EditMode = true : EditMode = false;
        TransformBox.SetActive(EditMode);
        TrackObjectBox.SetActive(!EditMode);

        if (!EditMode)
        {
            SelectedObject = null;
        }
    }

    public void ChangeValue(string val)
    {
        if (SelectedObject != null)
        {
            if (val == "PosX")
            {
                SelectedObject.transform.position = new Vector3(float.Parse(SelectedPosition[0].text), SelectedObject.transform.position.y, SelectedObject.transform.position.z);
            }
            else if (val == "PosY")
            {
                SelectedObject.transform.position = new Vector3(SelectedObject.transform.position.x, float.Parse(SelectedPosition[1].text), SelectedObject.transform.position.z);
            }
            else if (val == "PosZ")
            {
                SelectedObject.transform.position = new Vector3(SelectedObject.transform.position.x, SelectedObject.transform.position.y, float.Parse(SelectedPosition[2].text));
            }
            else if (val == "RotX")
            {
                SelectedObject.transform.eulerAngles = new Vector3(float.Parse(SelectedRotation[0].text), SelectedObject.transform.eulerAngles.y, SelectedObject.transform.eulerAngles.z);
            }
            else if (val == "RotY")
            {
                SelectedObject.transform.eulerAngles = new Vector3(SelectedObject.transform.eulerAngles.x, float.Parse(SelectedRotation[1].text), SelectedObject.transform.eulerAngles.z);
            }
            else if (val == "RotZ")
            {
                SelectedObject.transform.eulerAngles = new Vector3(SelectedObject.transform.eulerAngles.x, SelectedObject.transform.eulerAngles.y, float.Parse(SelectedRotation[2].text));
            }
            else if (val == "ScaleX")
            {
                SelectedObject.transform.localScale = new Vector3(float.Parse(SelectedScale[0].text), SelectedObject.transform.localScale.y, SelectedObject.transform.localScale.z);
            }
            else if (val == "ScaleY")
            {
                SelectedObject.transform.localScale = new Vector3(SelectedObject.transform.localScale.x, float.Parse(SelectedScale[1].text), SelectedObject.transform.localScale.z);
            }
            else if (val == "ScaleZ")
            {
                SelectedObject.transform.localScale = new Vector3(SelectedObject.transform.localScale.x, SelectedObject.transform.localScale.y, float.Parse(SelectedScale[2].text));
            }
        }
    }
}
