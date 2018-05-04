using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSettings : MonoBehaviour
{
	public GameObject runner;
	private GameObject terrain;
	private Material current_mat;
	public Material transparent_mat;
	public ParticleSystem rain;
    public AudioSource rain_sound;
	private bool visibleUG;
	private bool visibleT;

	// Use this for initialization
	void Start ()
	{
		visibleUG = true;
		visibleT = true;
		terrain = runner.GetComponent<GenerateWells>().terr;
		current_mat = terrain.GetComponentInChildren<Renderer>().material;
		Cursor.visible = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Cursor.visible = true;
	}

	public void ToggleTerrain()
	{
		if(visibleT==true)
		{
			terrain.GetComponentInChildren<Renderer>().material = transparent_mat;
			visibleT = false;
		}
		else
		{
			terrain.GetComponentInChildren<Renderer>().material = current_mat;
			visibleT = true;
		}
		
	}

	public void ToggleWater()
	{
		if(visibleUG==true)
		{
			runner.GetComponent<GenerateWells> ().SetUGVisibility(false);
			visibleUG = false;
		}
		else
		{
			runner.GetComponent<GenerateWells> ().SetUGVisibility(true);
			visibleUG = true;
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
