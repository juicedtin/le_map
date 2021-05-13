using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmReadout : MonoBehaviour
{
    public Button targetButton;
    public GameObject targetUA;
    public GameObject targetLA;
    public GameObject targetH;
    public GameObject edemaSlider;
    public GameObject fatSlider;
    public GameObject drscheck;
    public string drs;
    public string concat; 
    public string code;
    public int loc;
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
        if (loc != 3)
        {
            if (drscheck.GetComponent<Toggle>().isOn)
            {
                drs = "Dermal Rim Sign Present";
            }
            else
            {
                drs = "Dermal Rim Sign Not Present";
            }
        } else
        {
            drs = "Not applicable - L/R Hand";
        }
        concat = code + ": Edema Severity: " + edemaSlider.GetComponent<Slider>().value + " Fat Severity: " + fatSlider.GetComponent<Slider>().value + " " + drs + System.Environment.NewLine;
        Debug.Log("Triggered, readout ported");
        Debug.Log(concat);
        switch (loc)
        {
            case 1:
                targetUA.GetComponent<Text>().text = overwriteReadout(targetUA.GetComponent<Text>().text, code);
                targetUA.GetComponent<Text>().text += concat;
                break;
            case 2:
                targetLA.GetComponent<Text>().text = overwriteReadout(targetLA.GetComponent<Text>().text, code);
                targetLA.GetComponent<Text>().text += concat;
                break;
            case 3:
                targetH.GetComponent<Text>().text = overwriteReadout(targetH.GetComponent<Text>().text, code);
                targetH.GetComponent<Text>().text += concat;
                break;
        }
    }

    string overwriteReadout(string readout, string code)
    {
        int indexStart = readout.IndexOf(code);
        int deleteIndex = readout.IndexOf("\n") - indexStart;
        if (indexStart == -1)
        {
            return readout;
        }
        Debug.Log(deleteIndex + " " + indexStart);
        readout = readout.Remove(indexStart, deleteIndex+1);
        Debug.Log(readout);
        return readout;
    }

    string ovewriteReadoutLoop(string readout, string code)
    {
        int indexStart = readout.IndexOf(code);
        int deleteIndex = readout.IndexOf("\n") - indexStart;
        while (indexStart > -1)
        {
            Debug.Log(deleteIndex + " " + indexStart);
            readout = readout.Remove(indexStart, deleteIndex + 1);
            Debug.Log(readout);
            return readout;
            indexStart = readout.IndexOf(code);
            deleteIndex = readout.IndexOf("\n") - indexStart;
        }
    }
}
