using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorShower : MonoBehaviour
{
	public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

	// Use this for initialization
	void Start ()
	{
		Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
		Cursor.visible = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Cursor.visible = true;
	}

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
