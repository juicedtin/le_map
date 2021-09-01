using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Globalization;
using System.Text;
using System;
using System.Linq;


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
    string textInput;

    public GameObject targetBtn;
    public GameObject rcInput;
    public GameObject eMinInput;
    public GameObject eMaxInput;
    public GameObject fMinInput;
    public GameObject fMaxInput;

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
        string[] linedInput = textInput.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

        //Loop through entire input and categorize all similar tags for ScanData parsing
        List<List<String>> groupedScanData = new List<List<String>>();
        foreach (string line in linedInput)
        {
            string threeID = string.Join(" ", line.Split().Take(3));
            bool uniqueThreeID = true;
            foreach (List<String> searchQ in groupedScanData)
            {
                string searchQthreeID = string.Join(" ", searchQ[0].Split().Take(3));
                if (threeID.Equals(searchQthreeID))
                {
                    searchQ.Add(line);
                    uniqueThreeID = false;
                }
            }
            if (uniqueThreeID)
            {
                List<String> newThreeIDLine = new List<String>();
                newThreeIDLine.Add(line);
                groupedScanData.Add(newThreeIDLine);
            }
        }

        //Run through every list, convert to array, and feed into parseRcInput and RedCapPort
        foreach (List<String> uniqueLines in groupedScanData)
        {
            ScanData tempData = parseRcInput(uniqueLines.ToArray(), ':');
            GameObject taggedObject = GameObject.Find(tempData.getID());
            RedCapPort(tempData, eMax, eMin, fMax, fMin, taggedObject);
        }
    }

    //Port inputs onto visualizationa and change color accordingly for one single GameObject/datapoint.
    //Takes values eVal for edema, fVal for fat, and drs for drs y/n, and a target GameObject
    //Returns void
    void RedCapPort(ScanData data, float eMax, float eMin, float fMax, float fMin, GameObject targetObject)
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
        eLerp.normalColor = Color.Lerp(Color.green, Color.red, (data.getEdema() - eMin) / (eMax - eMin));
        eSlider.GetComponent<Slider>().colors = eLerp;
        ColorBlock fLerp = fSlider.GetComponent<Slider>().colors;
        fLerp.normalColor = Color.Lerp(Color.green, Color.red, (data.getFat() - fMin) / (fMax - fMin));
        fSlider.GetComponent<Slider>().colors = fLerp;
        
        //Special case for DRS (due to checkbox/bool)
        ColorBlock drsLerp = drsBox.GetComponent<Toggle>().colors;
        if (data.getDRS())
        {
            drsLerp.normalColor = Color.red;
        } else
        {
            drsLerp.normalColor = Color.white;
        }
        drsBox.GetComponent<Toggle>().colors = drsLerp;
    }

    //Methods that parses RedCap input, outputting a ScanData instance from a single
    //line input string of RedCap and delimiter separating identifier from data.
    ScanData parseRcInput(string[] sameIDInput, char delim)
    {
        //Establish values to be piped into final output ScanData
        float edemaVal = 0;
        float fatVal = 0;
        bool drsCheck = false;

        foreach (string input in sameIDInput)
        {
            //Split the input string into the identifier segment, and the data segment.
            string[] splitInput = input.Split(delim);
            //Further split data segment into single-word identifications, which will be used to build ID string
            string[] idChars = splitInput[0].Split();

            //Parse the last word of the identification half of the string, and pipe the subsequent values into the ScanData output
            if (idChars[idChars.Length - 1].ToLower().Equals("edema"))
            {
                edemaVal = float.Parse(splitInput[1].Replace(" ", string.Empty), CultureInfo.InvariantCulture.NumberFormat);
            }
            else if (idChars[idChars.Length - 1].ToLower().Equals("fat"))
            {
                fatVal = float.Parse(splitInput[1].Replace(" ", string.Empty), CultureInfo.InvariantCulture.NumberFormat);
            }
            else if (idChars[idChars.Length - 1].ToLower().Equals("drs"))
            {
                drsCheck = bool.Parse(splitInput[1].Replace(" ", string.Empty));
            }
            else
            {
                Debug.Log("Unknown case in idChars identification/piping");
            }
        }
        //For performance, assign targetID once after loop as sameIDINput will all have near-identicial identifier segments
        StringBuilder targetID = new StringBuilder("A-");
        string[] IDTempSplit = sameIDInput[0].Split(delim);
        string[] IDTempWordSplit = IDTempSplit[0].Split();
        //Build ID string according to Unity Scene naming: InputScreen ULAA-U = Upper Left Arm Anterior - Upper
        targetID.Insert(1, IDTempWordSplit[2].Substring(0, 1));
        targetID.Insert(0, IDTempWordSplit[0].Substring(0, 1));
        targetID.Insert(0, IDTempWordSplit[1].Substring(0, 1));
        if (IDTempWordSplit[1].EndsWith("1"))
        {
            targetID.Append('U');
        } else if (IDTempWordSplit[1].EndsWith("2"))
        {
            targetID.Append('L');
        } else
        {
            targetID.Append('U');
            Debug.Log("Unknown ID string build error, appending default Upper to end of GameObject ID");
            Debug.Log(targetID.ToString());
        }
        targetID.Insert(0, "InputScreen ");
        Debug.Log(targetID.ToString());

        ScanData output = new ScanData(edemaVal, fatVal, drsCheck, targetID.ToString());
        return output;
    }
}
