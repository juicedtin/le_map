using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmReadout : MonoBehaviour
{
    public Button targetButton;
    public GameObject targetq1;
    public GameObject targetq2;
    public GameObject targetq3;
    public GameObject targetq4;
    public GameObject textsource1;
    public GameObject textsource2;
    public GameObject textsource3;
    public GameObject textsource4;
    public GameObject drscheck;
    public string drs;
    public string concat; 
    public int quadrant; 
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
        if (drscheck.GetComponent<Toggle>().isOn)
        {
            drs = "Dermal Rim Sign Present";
        } else
        {
            drs = "Dermal Rim Sign Not Present";
        }
        concat = "Edema Severity: " + textsource1.GetComponent<Text>().text + " | Additional Notes: " + textsource2.GetComponent<Text>().text + "\n" + "Fat Severity: " + textsource3.GetComponent<Text>().text + " | Additional Notes: " + textsource4.GetComponent<Text>().text + "\n" + drs;
        Debug.Log("triggered, readout ported");
        Debug.Log(concat);
        switch (quadrant)
        {
            case 1:
                targetq1.GetComponent<Text>().text = concat;
                break;
            case 2:
                targetq2.GetComponent<Text>().text = concat;
                break;
            case 3:
                targetq3.GetComponent<Text>().text = concat;
                break;
            case 4:
                targetq4.GetComponent<Text>().text = concat;
                break;
        }
    }
}
