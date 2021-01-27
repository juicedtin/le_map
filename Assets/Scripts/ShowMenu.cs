using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ShowMenu : MonoBehaviour, IPointerClickHandler
{
    public Vector3 pointerPos;
    public Camera cam;
    public GameObject menuPrefab;
    public bool menuShown = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right Mouse Button Clicked");
            pointerPos = Input.mousePosition;
            menuShown = true;
            Debug.Log("target is at (" + pointerPos.x + ", " + pointerPos.y + ")");
            Instantiate(menuPrefab, pointerPos, Quaternion.identity);
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
