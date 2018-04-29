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
    private Text t;

    void Start ()
    {
        t = GameObject.Find("Info").GetComponentInChildren<Text>();
        //box.enabled = false;
        t.enabled = false;

        if (gameObject.GetComponent<BoxCollider>() == null)
            gameObject.AddComponent<BoxCollider>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnMouseOver()
    {
        //box.SetActive(true);
        t.enabled = true;
        t.text = i1+i2+i3+i4+i5+i6+i7;
    }

    void OnMouseExit()
    {
        t.enabled = false;
        //box.SetActive(false);
    }
}
