using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ShowMenu : MonoBehaviour, IPointerClickHandler
{
    public Vector3 pointerPos;
    public bool showMenu = false;
    public Camera cam;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right Mouse Button Clicked");
            showMenu = true;
            pointerPos = Input.mousePosition;
            Debug.Log("target is at (" + pointerPos.x + ", " + pointerPos.y + ")");
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
