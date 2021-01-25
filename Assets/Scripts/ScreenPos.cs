using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ScreenPos : MonoBehaviour
{
    public Transform target;
    public Vector3 screenPos;
    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        screenPos = cam.WorldToScreenPoint(target.position);
        Debug.Log("target is " + screenPos.x + " pixels from the left");
    }
    
}
