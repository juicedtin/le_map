using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyClipboard : MonoBehaviour
{
    public Button targetButton;
    public GameObject textTarget;
    public GameObject target1All;
    public GameObject target2All;
    public GameObject target3All;
    public bool copyAll;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = targetButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TaskOnClick()
    {
        if (!copyAll)
        {
            Debug.Log("Triggered, text copied");
            string targetText = textTarget.GetComponent<Text>().text;
            GUIUtility.systemCopyBuffer = targetText;
        } else
        {
            Debug.Log("Triggered, all text concated and copied");
            string targetText = "Upper Arm: " + target1All.GetComponent<Text>().text +
                "Lower Arm: " + target2All.GetComponent<Text>().text + "Hand: " + target3All.GetComponent<Text>().text;
            GUIUtility.systemCopyBuffer = targetText;
        }

    }
}
