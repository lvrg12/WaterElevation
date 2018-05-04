using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInfo : MonoBehaviour
{
    public bool isClicked = false;
    public string i1 = "Location";
    public string i2 = "County";
    public string i3 = "Well Depth";
    public string i4 = "Land Elevation";
    public string i5 = "Water Elevation";
    public string i6 = "Saturated Thickness";
    public string i7 = "Last Measurement On";
    public string[] i8 = {"","","","","","","","","","","","","","","","","","","",""};

    private Text t;
    private Text t2;

    void Start ()
    {
        t = GameObject.Find("Info").GetComponentInChildren<Text>();
        t2 = GameObject.Find("Info2").GetComponentInChildren<Text>();
        //box.enabled = false;
        t.enabled = false;
        t2.enabled = false;

        if (gameObject.GetComponent<BoxCollider>() == null)
            gameObject.AddComponent<BoxCollider>();
    }

    void OnMouseOver()
    {
        //box.SetActive(true);
        t.enabled = true;
        t2.enabled = true;

        t.text = i1+i2+i3+i4+i5+i6+i7;
        string i8All = "";

        for(int i = 0; i<i8.Length; i++)
        {
            i8All += i8[i];
        }

        t2.text = i8All;
    }

    void OnMouseExit()
    {
        t.enabled = false;
        t2.enabled = false;
        //box.SetActive(false);
    }
}
