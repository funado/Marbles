using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorCameraController : MonoBehaviour
{
    [SerializeField]
    private float DragFactor;
    [SerializeField]
    private float ScrollFactor;

    private Vector3 CamPos;
    private Vector3 MouseDrag;
    private float MouseScroll;
    private float DragLimit = 5f;

    private void LateUpdate()
    {
        // Camera Controls
        MouseDrag = Input.mousePosition - CamPos;
        // Controls camera movement by dragging right mouse button
        if (Input.GetMouseButton(1))
        {
            CamPos = Input.mousePosition;
            if (Mathf.Abs(MouseDrag.x) > DragLimit || Mathf.Abs(MouseDrag.y) > DragLimit)
            {
                return;
            }
            transform.position += new Vector3((MouseDrag / DragFactor).x, 0, (MouseDrag / DragFactor).y);
        }
        // Controls camera zoom by using mouse scroll wheel
        if ((Input.GetAxisRaw("Mouse ScrollWheel") != 0))
        {
            MouseScroll = Input.GetAxisRaw("Mouse ScrollWheel") * ScrollFactor;
            transform.position -= new Vector3(0, MouseScroll, 0);
        }
    }
}