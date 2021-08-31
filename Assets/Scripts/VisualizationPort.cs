using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class VisualizationPort : MonoBehaviour
{
    string targetTag;
    GameObject targetObject;
    GameObject targetBtn;
    float eMin;
    float eMax;
    float fMax;
    float fMin;
    //Delimiter?
    string delim;
    /*Case-based colors, current implementation is done with color lerp
    Color none = new Color(0.3114324, 0.7735849, 0.0105415, 1);
    Color mild = new Color(0.8962264, 0.7194716. 0, 1);
    Color moderate = new Color(0.8980392, 0.4031845, 0, 1);
    Color severe = new Color(0.8490566, 0, 0.3208869, 1);*/
    // Start is called before the first frame update
    void Start()
    {
        Button parseButton = targetBtn.GetComponent<Button>();
        parseButton.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TaskOnClick()
    {

    }

    void RedCapPort(float eVal, float fVal, bool drs, GameObject targetObject)
    {
        //Find and assign data to each GameObject for visualization
        GameObject eSlider = targetObject.transform.Find("EdemaSlider").gameObject;
        GameObject fSlider = targetObject.transform.Find("FatSlider").gameObject;
        GameObject drsBox =  targetObject.transform.Find("DRS").gameObject;
        eSlider.GetComponent<Slider>().value = eVal;
        fSlider.GetComponent<Slider>().value = fVal;
        drsBox.GetComponent<Toggle>().isOn = drs;

        //Set slider color for easy viewing
        ColorBlock eLerp = eSlider.GetComponent<Slider>().colors;
        eLerp.normalColor = Color.Lerp(Color.green, Color.red, (eVal - eMin) / (eMax - eMin));
        eSlider.GetComponent<Slider>().colors = eLerp;
        ColorBlock fLerp = fSlider.GetComponent<Slider>().colors;
        fLerp.normalColor = Color.Lerp(Color.green, Color.red, (eVal - eMin) / (eMax - eMin));
        fSlider.GetComponent<Slider>().colors = fLerp;
        ColorBlock drsLerp = new ColorBlock();
        if (drs)
        {
            drsLerp.normalColor = Color.red;
        } else
        {
            drsLerp.normalColor = Color.white;
        }
        drsBox.GetComponent<Toggle>().colors = drsLerp;
    }
}
