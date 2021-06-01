using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearArea : MonoBehaviour
{
    public GameObject textUA;
    public GameObject textLA;
    public GameObject textH;
    public int id;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onClick()
    {
        switch (id) 
        {
            case 0:
                textUA.GetComponent<Text>().text = "";
                break;
            case 1:
                textLA.GetComponent<Text>().text = "";
                break;
            case 2:
                textH.GetComponent<Text>().text = "";
                break;
            default:
                Debug.Log("Unknown case, check id");
                break;
        }
    }
}
