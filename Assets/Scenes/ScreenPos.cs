using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ContextMenu : MonoBehaviour, IPointerClickHandler
{
    public Transform target;
    public static Vector3 screenPos;
    public static bool showMenu;
    Camera cam;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right) {
            Debug.Log("Right Mouse Button Clicked");
            screenPos = cam.WorldToScreenPoint(target.position);
            Debug.Log("target is " + screenPos.x + " pixels from the left");
            showMenu = true;
        }
    }

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        
    }
    
}
