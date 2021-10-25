using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undo : MonoBehaviour
{

    public class set
    {
        public GameObject gameObject;

        public Vector3 position;
        public Quaternion rotation;

        public bool undone;


        public void Restore()
        {
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;
            gameObject.SetActive(undone);
        }

        public set(GameObject a)
        {
            gameObject = a;
            position = a.transform.position;
            rotation = a.transform.rotation;
            undone = a.activeSelf;
        }
    }


    public List<set> UndoList;

    public void AddUndo (GameObject a)
    {
        set b = new set(a);
        UndoList.Add(b);
    }
    
    public void UndoFunc()
    {
        if(UndoList.Count > 0)
        {
            UndoList[UndoList.Count - 1].Restore();
            UndoList.RemoveAt(UndoList.Count - 1);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        UndoList = new List<set>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
