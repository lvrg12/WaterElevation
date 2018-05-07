using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleCanvas : MonoBehaviour {

    private Canvas CanvasObject; 

	// Use this for initialization
	void Start () {
        CanvasObject = GetComponent<Canvas>();
        CanvasObject.enabled = false;
	}
	
	// Update is called once per frame
	/*void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            CanvasObject.enabled = !CanvasObject.enabled;
        }
    }*/

   /* void OnMouseOver()
    {
        CanvasObject.enabled = true;
    }

    void OnMouseExit()
    {
        CanvasObject.enabled = false;
    }*/
}
