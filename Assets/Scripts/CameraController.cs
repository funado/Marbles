using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform Player;
    [SerializeField]
    private float smoothSpeed = 0.05f;
    [SerializeField]
    private float RotateSpeed = 1f;

    private Vector3 Offset;

    private void Awake()
    {
        Offset = transform.position;
    }

    private void FixedUpdate()
    {
        LerpCamera(Player.transform.position, smoothSpeed);
    }

    private void LerpCamera(Vector3 newPos, float newSmooth)
    {
        Vector3 targetPos = newPos + Offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, targetPos, newSmooth);
        transform.position = smoothedPos;
    }
}
