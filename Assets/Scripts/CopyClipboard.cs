using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyClipboard : MonoBehaviour
{
    public Button targetButton;
    public GameObject textTarget;
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
        Debug.Log("triggered, text copied");
        string targetText = textTarget.GetComponent<Text>().text;
        GUIUtility.systemCopyBuffer = targetText;
    }
}
