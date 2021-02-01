using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ShowMenu : MonoBehaviour, IPointerClickHandler
{
    public Vector3 pointerPos;
    public Camera cam;
    public Transform menuParent;
    public GameObject menu;
    public int quadrant;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right Mouse Button Clicked");
            Vector2 mouseWorldPosition = Input.mousePosition;
            pointerPos = cam.ScreenToWorldPoint(new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, cam.nearClipPlane+10));
            Debug.Log("target is at (" + pointerPos.x + ", " + pointerPos.y + ")");
            float halfHeight = cam.GetComponent<Camera>().orthographicSize;
            float halfWidth = cam.GetComponent<Camera>().aspect * halfHeight;
            if (pointerPos.x <= 0 && pointerPos.y >= 0)
            {
                quadrant = 1;
            } else if (pointerPos.x >= 0 && pointerPos.y >= 0)
            {
                quadrant = 2;
            } else if (pointerPos.x <= 0 && pointerPos.y <= 0)
            {
                quadrant = 3;
            } else if (pointerPos.x >= 0 && pointerPos.y <= 0)
            {
                quadrant = 4;
            }
            Debug.Log("quadrant: " + quadrant);
            menu.transform.position = pointerPos;
            Debug.Log("object instantiated");
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
