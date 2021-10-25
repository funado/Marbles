// Content modified from Game Engine Design Tutorials 
// Author: Parisa Sargolzaei
// Modified By: Arthiran Sivarajah - 100660300, Aaron Chan - 100657311

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public GameObject SaveBox;
    public GameObject LoadBox;
    public TMP_Text SaveInput;
    public TMP_Text LoadInput;

    public TMP_Text CurrentObjectText;

    private const string PluginName = "UnityPlugin";
    private string folderLocation = Application.streamingAssetsPath + "\\";
    private const string textExtension = ".txt";

    private List<GameObject> spawnables = new List<GameObject>();
    private int MaxElements = 10;

    [DllImport(PluginName)]
    private static extern void SaveToFile(float objectNumber, float locationx, float locationy, float locationz, float rotationx,
float rotationy, float rotationz, float scalex, float scaley, float scalez);
    [DllImport(PluginName)]
    private static extern float LoadFromFile(int j, string fileName);
    [DllImport(PluginName)]
    private static extern void StartWriting(string fileName);
    [DllImport(PluginName)]
    private static extern void EndWriting();
    [DllImport(PluginName)]
    private static extern int GetLines(string fileName);


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
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            SaveObjects();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            LoadObjects();
        }
    }

    public void SaveObjects()
    {
        if (SaveInput.text != null)
        {
            string cleanedInput = SaveInput.text.Substring(0, SaveInput.text.Length - 1);
            string fileToSave = folderLocation + cleanedInput + textExtension;
            EditPrefabTransform[] spawnablestemp = FindObjectsOfType<EditPrefabTransform>();
            GameObject[] spawnableGameObjects = new GameObject[spawnablestemp.Length];
            if (spawnablestemp.Length > 0)
            {
                for (int i = 0; i < spawnablestemp.Length; i++)
                {
                    spawnableGameObjects[i] = spawnablestemp[i].gameObject;
                }
            }
            spawnables.AddRange(spawnableGameObjects);
            StartWriting(fileToSave);
            for (int i = 0; i < TrackPrefabs.Count; i++)
            {
                for (int j = 0; j < spawnables.Count; j++)
                {
                    if (TrackPrefabs[i].name.ToLower() == spawnables[j].name.Substring(0, spawnables[j].gameObject.name.Length - 7).ToLower())
                    {
                        SaveToFile(i, spawnables[j].transform.position.x, spawnables[j].transform.position.y, spawnables[j].transform.position.z,
                            spawnables[j].transform.eulerAngles.x, spawnables[j].transform.eulerAngles.y, spawnables[j].transform.eulerAngles.z,
                            spawnables[j].transform.localScale.x, spawnables[j].transform.localScale.y, spawnables[j].transform.localScale.z);
                    }
                }
            }
            EndWriting();
        }
    }

    public void LoadObjects()
    {
        if (LoadInput.text != null)
        {
            string cleanedInput = LoadInput.text.Substring(0, LoadInput.text.Length - 1);
            string loadFile = folderLocation + cleanedInput + textExtension;
            bool loading = true;
            int infoSet = 0;

            if (GetLines(loadFile) != 0)
            {
                while (loading)
                {
                    for (int i = 0; i <= (GetLines(loadFile) / MaxElements); i++)
                    {
                        GameObject tempSpawnableObject;
                        tempSpawnableObject = Instantiate(TrackPrefabs[Mathf.RoundToInt(LoadFromFile(0 + infoSet, loadFile))].gameObject, new Vector3(LoadFromFile(1 + infoSet, loadFile), LoadFromFile(2 + infoSet, loadFile), LoadFromFile(3 + infoSet, loadFile)), Quaternion.Euler(LoadFromFile(4 + infoSet, loadFile), LoadFromFile(5 + infoSet, loadFile), LoadFromFile(6 + infoSet, loadFile)));
                        tempSpawnableObject.transform.localScale = new Vector3(LoadFromFile(7 + infoSet, loadFile), LoadFromFile(8 + infoSet, loadFile), LoadFromFile(9 + infoSet, loadFile));
                        tempSpawnableObject.AddComponent<EditPrefabTransform>();
                        infoSet = infoSet + MaxElements;
                    }
                    loading = false;
                }
            }
        }
    }

    public void OpenSaveBox()
    {
        SaveBox.SetActive(!SaveBox.activeSelf && !LoadBox.activeSelf ? true : false);
    }

    public void OpenLoadBox()
    {
        LoadBox.SetActive(!LoadBox.activeSelf && !SaveBox.activeSelf ? true : false);
    }

    public void ClearScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
