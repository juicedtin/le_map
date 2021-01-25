using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ShowMenu : MonoBehaviour, IPointerClickHandler
{
    public Vector3 screenPosition;
    public bool showMenu = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right Mouse Button Clicked");
            showMenu = true;
            Debug.Log("target is " + screenPosition.x + " pixels from the left");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Main Camera").GetComponent<ScreenPos>().screenPos = screenPosition;
    }
}
