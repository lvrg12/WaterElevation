using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInfo : MonoBehaviour
{
    public bool isClicked = false;
    public string i0 = "ID";
    public string i1 = "Location";
    public string i2 = "County";
    public string i3 = "Well Depth";
    public string i4 = "Land Elevation";
    public string i5 = "";
    public string i6 = "";
    public string i7 = "";
    public string[] i8 = new string[20];
    

    private Text t;
    private Text t2;
    private Canvas CanvasObject;

    void Start ()
    {
        t = GameObject.Find("Info").GetComponentInChildren<Text>();
        t2 = GameObject.Find("Info2").GetComponentInChildren<Text>();
        t.enabled = false;
        t2.enabled = false;
        CanvasObject = GameObject.Find("UserCanvas").GetComponent<Canvas>();
        CanvasObject.GetComponent<Canvas>().enabled = false;

        if (gameObject.GetComponent<BoxCollider>() == null)
            gameObject.AddComponent<BoxCollider>();
    }

    void OnMouseOver()
    {
        CanvasObject.enabled = true;
        t.enabled = true;
        t2.enabled = true;

        t.text = i1+i2+i3+i4;
        string i8All = "Saturated Thickness\n";

        for (int i = 0; i<i8.Length; i++)
        {
            i8All += i8[i];
        }

        t2.text = i8All;
    }

    void OnMouseExit()
    {
        CanvasObject.enabled = false;
        t.enabled = false;
        t2.enabled = false;
    }
}