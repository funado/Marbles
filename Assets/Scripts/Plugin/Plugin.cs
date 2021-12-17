using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.IO;

public class Plugin : MonoBehaviour
{

    [DllImport("TestDLL")]
    static extern int GetID();

    [DllImport("TestDLL")]
    static extern void SetID(float id);

    [DllImport("TestDLL")]
    static extern Vector3 GetPosition();

    [DllImport("TestDLL")]
    static extern void SetPosition(float x, float y, float z);

    [DllImport("TestDLL")]
    static extern void SavePosition();

    [DllImport("TestDLL")]
    static extern Color RandomColor();

    [DllImport("TestDLL")]
    static extern void SaveColor(float r, float g, float b);

    private Vector3 pos;
    private Color cr;

    private MeshRenderer mr;

    //Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();

        ParticleSystem.MainModule ma = ps.main;

        Color myColor = RandomColor();
        myColor.a = 1;

        ma.startColor = myColor;

    }

    public void SaveMCPos()
    {
        pos = transform.position;

        SetPosition(pos.x, pos.y, pos.z);

        SavePosition();

        SaveColor(cr.r, cr.g, cr.b);
    }

    public void RandColor()
    {
        Color rc = RandomColor();
        mr.material.color = new Color(rc.r, rc.g, rc.b);
    }
}
