using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float zoomSpeed;
    public Camera cam;

    private Vector3 camOrigin;
    private Vector3 camMoveDifference;

    private bool isDraging;

    private void Update()
    {
        float y = Input.mouseScrollDelta.y;

        if(y < 0)
        {
            Debug.Log("H");
            cam.orthographicSize += zoomSpeed;
        }
        if(y > 0)
        {
            cam.orthographicSize -= zoomSpeed;
        }
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 1f, 100);
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            camMoveDifference = (cam.ScreenToWorldPoint(Input.mousePosition) - cam.transform.position);
            if (!isDraging)
            {
                isDraging = true;
                camOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            isDraging = false;
        }

        if (isDraging)
        {
            cam.transform.position = camOrigin - camMoveDifference;
        }
    }


}
