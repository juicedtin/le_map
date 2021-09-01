using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Globalization;
using System.Text;


//Define ScanData class that includes edema, fat, and drs values along with an ObjectID used to find
//reference GameObject in the scene for porting. 
//No child methods, only getters/setters
public class ScanData
{
    float edemaValue;
    float fatValue;
    bool drs;
    string objectID;

    public ScanData(float eV, float fV, bool drsBool, string objID)
    {
        edemaValue = eV;
        fatValue = fV;
        drs = drsBool;
        objectID = objID;
    }

    public ScanData(float eV, float fV, bool drsBool)
    {
        edemaValue = eV;
        fatValue = fV;
        drs = drsBool;
        objectID = "";
    }

    public float getEdema()
    {
        return edemaValue;
    }

    public float getFat()
    {
        return fatValue;
    }
    
    public bool getDRS()
    {
        return drs;
    }

    public string getID()
    {
        return objectID;
    }

    public void setEdema(float eVal)
    {
        edemaValue = eVal;
    }

    public void setFat(float fVal)
    {
        fatValue = fVal;
    }
    
    public void setDRS(bool newDRS)
    {
        drs = newDRS;
    }

    public void setID(string newID)
    {
        objectID = newID;
    }
}


public class VisualizationPort : MonoBehaviour
{
    string targetTag;
    string textInput;

    GameObject targetObject;
    GameObject targetBtn;
    GameObject rcInput;
    GameObject eMinInput;
    GameObject eMaxInput;
    GameObject fMinInput;
    GameObject fMaxInput;

    float eMin;
    float eMax;
    float fMax;
    float fMin;
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
        //Parse the inputted min/max into global float values
        eMin = float.Parse(eMinInput.GetComponent<InputField>().text, CultureInfo.InvariantCulture.NumberFormat);
        eMax = float.Parse(eMaxInput.GetComponent<InputField>().text, CultureInfo.InvariantCulture.NumberFormat);
        fMin = float.Parse(fMinInput.GetComponent<InputField>().text, CultureInfo.InvariantCulture.NumberFormat);
        fMax = float.Parse(fMaxInput.GetComponent<InputField>().text, CultureInfo.InvariantCulture.NumberFormat);

        //Port text into string for delimiterization
        textInput = rcInput.GetComponent<InputField>().text;
        string linedInput[] = textInput.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);


        
        GameObject taggedObject = GameObject.Find(targetTag);
        for (string s in linedInput)
        {
            
        }
    }

    //Port inputs onto visualizationa and change color accordingly for one single GameObject/datapoint.
    //Takes values eVal for edema, fVal for fat, and drs for drs y/n, and a target GameObject
    //Returns void
    void RedCapPort(ScanData data, GameObject targetObject)
    {
        //Find and assign data to each GameObject for visualization
        GameObject eSlider = targetObject.transform.Find("EdemaSlider").gameObject;
        GameObject fSlider = targetObject.transform.Find("FatSlider").gameObject;
        GameObject drsBox =  targetObject.transform.Find("DRS").gameObject;
        eSlider.GetComponent<Slider>().value = data.getEdema();
        fSlider.GetComponent<Slider>().value = data.getFat();
        drsBox.GetComponent<Toggle>().isOn = data.getDRS();

        //Set slider color for easy viewing by interpolating the inputted value between max and min inputs.
        ColorBlock eLerp = eSlider.GetComponent<Slider>().colors;
        eLerp.normalColor = Color.Lerp(Color.green, Color.red, (eVal - eMin) / (eMax - eMin));
        eSlider.GetComponent<Slider>().colors = eLerp;
        ColorBlock fLerp = fSlider.GetComponent<Slider>().colors;
        fLerp.normalColor = Color.Lerp(Color.green, Color.red, (eVal - eMin) / (eMax - eMin));
        fSlider.GetComponent<Slider>().colors = fLerp;
        
        //Special case for DRS (due to checkbox/bool)
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

    //
    ScanData parseRcInput(string input, char delim)
    {
        float edemaVal = 0;
        float fatVal = 0;
        bool drsCheck = false;
        string[] splitInput = input.Split(delim);
        string[] idChars = splitInput[0].Split(' ');
        StringBuilder targetID = new StringBuilder("A-");
        if (idChars[idChars.Length - 1].ToLower().Equals("edema"))
        {
            edemaVal = float.Parse(splitInput[1].Replace(" ", string.Empty), CultureInfo.InvariantCulture.NumberFormat);
        } else if (idChars[idChars.Length - 1].ToLower().Equals("fat"))
        {
            fatVal = float.Parse(splitInput[1].Replace(" ", string.Empty), CultureInfo.InvariantCulture.NumberFormat);
        } else if (idChars[idChars.Length - 1].ToLower().Equals("drs"))
        {
            drsCheck = bool.Parse(splitInput[1].Replace(" ", string.Empty));
        } else
        {
            Debug.Log("Unknown case in idChars identification/piping");
        }
        t
        ScanData output = new ScanData(edemaVal, fatVal, drsCheck, targetID.ToString());
        return output;
    }
}
