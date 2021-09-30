using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform Player;
    [SerializeField]
    private Vector3 Offset = new Vector3(0, 2.75f, -10);
    [SerializeField]
    private float smoothSpeed = 0.05f;

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
