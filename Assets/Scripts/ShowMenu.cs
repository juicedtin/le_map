using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ShowMenu : MonoBehaviour, IPointerClickHandler
{
    public Vector3 pointerPos;
    public Camera cam;
    public Transform menuParent;
    public GameObject menuPrefab;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right Mouse Button Clicked");
            Vector2 mouseWorldPosition = Input.mousePosition;
            pointerPos = cam.ScreenToWorldPoint(new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, cam.nearClipPlane));
            Debug.Log("target is at (" + pointerPos.x + ", " + pointerPos.y + ")");
            Instantiate(menuPrefab, pointerPos, Quaternion.identity, menuParent);
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
