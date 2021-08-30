using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizationPort : MonoBehaviour
{
    string targetTag;
    GameObject targetObject;
    /*Case-based colors, current implementation is done with color lerp
    Color none = new Color(0.3114324, 0.7735849, 0.0105415, 1);
    Color mild = new Color(0.8962264, 0.7194716. 0, 1);
    Color moderate = new Color(0.8980392, 0.4031845, 0, 1);
    Color severe = new Color(0.8490566, 0, 0.3208869, 1);*/
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RedCapPort(int eVal, int fVal, bool drs, GameObject targetObject)
    {
        GameObject eSlider = targetObject.transform.Find("EdemaSlider");
        GameObject fSlider = targetObject.transform.Find("FatSlider");
        GameObject drsBox = targetObject.transform.Find("DRS");
        eSlider.value = eVal;
        fSlider.value = fVal;
        drsBox.isOn = drs;
    }
}
