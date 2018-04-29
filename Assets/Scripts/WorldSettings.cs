using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSettings : MonoBehaviour
{
	public GameObject runner;
	public Terrain terrain;
	public Material orig_mat;
	public Material other_mat;
	public ParticleSystem rain;
    public AudioSource rain_sound;
	private bool visible;

	// Use this for initialization
	void Start ()
	{
		visible = true;
		Cursor.visible = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Cursor.visible = true;
	}

	public void ToggleTerrain ()
	{
		if(terrain.materialTemplate == orig_mat)
		{
			terrain.materialTemplate = other_mat;
		}
		else
		{
			terrain.materialTemplate = orig_mat;
		}
	}

	public void ToggleWater ()
	{
		if(visible==true)
		{
			runner.GetComponent<GenerateWells> ().SetVisibility(false);
			visible = false;
		}
		else
		{
			runner.GetComponent<GenerateWells> ().SetVisibility(true);
			visible = true;
		}
		
	}

	public void StartRain ()
	{
		rain.Play();
        rain_sound.Play();
	}

	public void StopRain ()
	{
		rain.Stop();
        rain_sound.Stop();
	}

	public void printTest()
	{
		print("Testing");
	}
}
