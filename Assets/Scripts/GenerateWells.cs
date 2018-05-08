using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Text;
using System;

public class GenerateWells : MonoBehaviour
{
    public GameObject info;
	public GameObject water_well;
	public GameObject well_marker;
    public GameObject depth_object;
	public GameObject water_cyl;
	public GameObject container_cube;
	public GameObject water_cube;
    public GameObject Spring;
    public GameObject lubbock_map;
    public GameObject amarillo_map;
    public GameObject lamesa_map;
    public GameObject plainview_map;
    public GameObject midland_map;

    public Material[] orig_mat;
	public Material[] other_mat;
    private Material current_mat;
	public Material transparent_mat;
    public Slider mainSlider;
    private GameObject terr;

	private List<GameObject> wells = new List<GameObject>();
	private List<GameObject> markers = new List<GameObject>();
	private List<GameObject> depths = new List<GameObject>();
	private List<GameObject> waters = new List<GameObject>();
	private List<GameObject> boxs = new List<GameObject>();
    private List<GameObject> points = new List<GameObject>();
	private List<float> points_orlen = new List<float>();
    private List<float> points_cur = new List<float>();
	private List<List<float>> wes = new List<List<float>>();
	private List<float> deps = new List<float>();
	private List<float> les = new List<float>();
	private List<List<float>> sts = new List<List<float>>();
	private float scale;
    private string city;
    private string datafile;
    private int currentYearIndex;
    private float[] coor = {0,0,0,0};
   
    void Start ()
	{
        SetTerrain();
		SetWells();
		//SetUGWater();
		UpdateColors();

        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
    }

	public void UpdateColors()
	{
		float a,b,x;
		for(int i=0; i<wells.Count; i++)
        {
			a = wes[i][currentYearIndex];
			x = les[i]-deps[i];
			b = wes[i][currentYearIndex]-sts[i][currentYearIndex];

            if(wes[i][currentYearIndex] == 0.0f)
			{
				markers[i].GetComponent<SpriteRenderer>().color = Color.black;
                depths[i].GetComponent<Renderer>().materials = orig_mat;
			}            
            else if(x < a && b < x)
			{
				markers[i].GetComponent<SpriteRenderer>().color = Color.green;
                depths[i].GetComponent<Renderer>().materials = orig_mat;
			}
			else
			{
				markers[i].GetComponent<SpriteRenderer>().color = Color.red;
                depths[i].GetComponent<Renderer>().materials = other_mat;
			}
        }
	}

	public void SetUGVisibility(bool b)
    {
        for(int i=0; i<points.Count; i++)
        {
            points[i].SetActive(b);
        }
    }

    public void SetTerrainVisibility(bool b)
    {
        if(b==true)
		{
            terr.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = current_mat;
		}
		else
		{
            terr.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = transparent_mat;
		}
    }

	public float newYScale(GameObject theGameObject, float newSize)
	{
		float size = theGameObject.GetComponent<Renderer> ().bounds.size.y;
		Vector3 rescale = theGameObject.transform.localScale;
		rescale.y = newSize * rescale.y / size;
		theGameObject.transform.localScale = rescale;
		return rescale.y;
	}

    public void SetYear()
    {
        currentYearIndex = (int)mainSlider.value;

        for(int i=0; i<waters.Count; i++)
        {
            var x = waters[i].transform.localPosition.x;
            var y = scale*wes[i][currentYearIndex];
            var z = waters[i].transform.localPosition.z;
            float w = newYScale(waters[i], scale * sts[i][currentYearIndex]);
            waters[i].transform.localPosition = new Vector3(x,y-w,z);
        }

        UpdateColors();
    }

    void SetTerrain()
    {
        TextAsset txtAsset = (TextAsset)Resources.Load("coordinates", typeof(TextAsset));
        string[] lines = txtAsset.text.Split('\n');
        //print(lines[1]);

        txtAsset = (TextAsset)Resources.Load("currentCity", typeof(TextAsset));
		city = "" + txtAsset.text[0] + txtAsset.text[1];

        string[] coords;

        if(city == "Lu")
        {
            terr = Instantiate(lubbock_map, new Vector3 (0,1.9f, 0), Quaternion.identity);
            datafile = "Lubbock_optimized_May";
            coords = lines[1].Split(',');
        }
        else if(city == "Am")
        {
            terr = Instantiate(amarillo_map, new Vector3 (0,1.9f, 0), Quaternion.identity);
            datafile = "Randall_optimized_May";
            coords = lines[2].Split(',');
        }
        else if(city == "La")
        {
            terr = Instantiate(lamesa_map, new Vector3 (0,-5.4f, 0), Quaternion.identity);
            datafile = "Dawson_optimized_May";
            coords = lines[5].Split(',');
        }
        else if(city == "Mi")
        {
            terr = Instantiate(midland_map, new Vector3 (0,-5.0f, 0), Quaternion.identity);
            datafile = "Midland_optimized_May";
            coords = lines[3].Split(',');
        }
        else if(city == "Pl")
        {
            terr = Instantiate(plainview_map, new Vector3 (0,-3.0f, 0), Quaternion.identity);
            datafile = "Hale_optimized_May";
            coords = lines[4].Split(',');
        }
        else
        {
            terr = Instantiate(lubbock_map, new Vector3 (0,1.9f, 0), Quaternion.identity);
            datafile = "Lubbock_optimized";
            coords = lines[1].Split(',');
        }

        current_mat = terr.GetComponentInChildren<Renderer>().material;
        terr.name = txtAsset.text;
        coor[0] = float.Parse(coords[2]);
        coor[1] = float.Parse(coords[3]);
        coor[2] = float.Parse(coords[4]);
        coor[3] = float.Parse(coords[5]);
    }

	void SetWells()
	{
        float left = coor[0];
        float right = coor[1];
        float up = coor[2];
        float down = coor[3];

		TextAsset txtAsset = (TextAsset)Resources.Load(datafile+"WE", typeof(TextAsset));
		TextAsset txtAssetST = (TextAsset)Resources.Load(datafile+"ST", typeof(TextAsset));
		string[] lines = txtAsset.text.Split('\n');
        string[] linesST = txtAssetST.text.Split('\n');
		scale = 0.0625f;
        int num_years = 20;

        for(int index =1; index < lines.Length-1; index++)
        {
            string[] values = lines[index].Split(',');
			float latitude = float.Parse(values[2]);
			float longitude = float.Parse(values[3]);

			if (longitude >= left && longitude <= right)
			{
				if (latitude >= down && latitude <= up)
				{
					float xPos = (longitude - left) * 500f / (right - left);
					float zPos = (latitude - down) * 500f / (up - down);
                    float well_depth = float.Parse(values[4]);
			        float land_el = float.Parse(values[5]);
                    string[] water_elString = new string[num_years];
                    string[] thicknessString = new string[num_years];
                    Array.Copy(values,7,water_elString,0,num_years);
                    Array.Copy(linesST[index].Split(','),7,thicknessString,0,num_years);
			        // float lsd = float.Parse(values[6]);

                    List<float> water_el = new List<float>();
                    List<float> thickness = new List<float>();
                    for(int i = 0; i<num_years; i++)
                    {
                        water_el.Add(float.Parse(water_elString[i]));

                        var x = float.Parse(water_elString[i]);
                        if(x == 0.0)
                        {
                            x = 0.005f;
                        }
                        thickness.Add(x/3.28084f);
                    }

                    // wells represent 1995 data at start
					GameObject well = Instantiate(water_well, new Vector3 (xPos, scale * land_el+3.5f, zPos), Quaternion.identity);
					GameObject box = Instantiate(container_cube, new Vector3 (xPos, scale * land_el+3.5f, zPos), Quaternion.identity);
					GameObject marker = Instantiate(well_marker, new Vector3 (xPos, 150f, zPos), Quaternion.Euler(new Vector3(80,0,0)));
                    GameObject Wspring = Instantiate(Spring, new Vector3(xPos, scale * land_el, zPos), Quaternion.identity);
					GameObject depth = Instantiate(depth_object, new Vector3(xPos, scale * land_el , zPos), Quaternion.identity);
					GameObject water = Instantiate(water_cyl, new Vector3(xPos, scale * water_el[0], zPos), Quaternion.identity);

                    depth.transform.localScale = new Vector3(0.75f, scale * well_depth, 0.75f);
					depth.transform.localPosition = new Vector3(xPos,scale*land_el,zPos);

					water.transform.localScale = new Vector3(5.0f, scale * thickness[0], 5.0f);
					float w = newYScale(water,scale * thickness[0]);
					water.transform.localPosition = new Vector3(xPos,scale*water_el[0] - w,zPos);

					marker.transform.localScale = new Vector3(10,10,10);

					well.name = values [0]+"_welltop";
					box.name = values [0]+"_box";
					marker.name = values[0]+"_marker";
					depth.name = values[0]+"_well";
					water.name = values[0]+"_st";
                    Wspring.name = values[0]+"_spring";

					var info0 = "ID: "+values[0];
                    var info1 = "\nLocation: "+ longitude +", "+latitude;
                    var info2 = "\nCounty: "+values[1];
					var info3 = "\nWell Depth: "+ well_depth;
                    var info4 = "\nLand Elevation: "+land_el;
					// var info5 = "\nWater Elevation: " + values [6];
                    // var info6 = "\nSaturated Thickness: " + values [7];
					// var info7 = "\nLast Measurement On: " + values [9] + "/" + values [10] + "/" + values[11];

					box.GetComponent<DisplayInfo> ().i0 = info0;
                    box.GetComponent<DisplayInfo> ().i1 = info1;
                    box.GetComponent<DisplayInfo> ().i2 = info2;
                    box.GetComponent<DisplayInfo> ().i3 = info3;
                    box.GetComponent<DisplayInfo> ().i4 = info4;
                    // box.GetComponent<DisplayInfo> ().i5 = info5;
                    // box.GetComponent<DisplayInfo> ().i6 = info6;
                    // box.GetComponent<DisplayInfo> ().i7 = info7;

                    for(int i = 0; i<num_years; i++)
                    {
                        box.GetComponent<DisplayInfo> ().i8[i] = (i+1995)+": "+thickness[i]+"\n";
                    }

					wells.Add(well);
					markers.Add(marker);
					depths.Add(depth);
					waters.Add(water);
					boxs.Add(box);
					wes.Add(water_el);
					sts.Add(thickness);
					deps.Add(well_depth);
					les.Add(land_el);
				}
			}
        }
    }

	void SetUGWater()
	{
        float left = coor[0];
        float right = coor[1];
        float up = coor[2];
        float down = coor[3];

        TextAsset txtAsset = (TextAsset)Resources.Load("raster_to_point", typeof(TextAsset));
        string[] lines = txtAsset.text.Split('\n');

        for(int index =1;index < lines.Length-1; index++)
        {
            string[] values = lines[index].Split(',');
            float longitude = float.Parse(values[2]);
            float latitude = float.Parse(values[3]);
            float thickness = float.Parse(values[1]);

			if (longitude >= left && longitude <= right)
			{
				if (latitude >= down && latitude <= up) 
				{
					float xPos = (longitude - left) * 500f / (right - left);
					float zPos = (latitude - down) * 500f / (up - down);
                    var point = Instantiate(water_cube, new Vector3(xPos, 55f + scale*thickness/2, zPos), Quaternion.identity);
					point.transform.localScale = new Vector3(10.8f, newYScale( point, scale * thickness ), 10.8f);
					point.name = values [0];
                    points.Add(point);
                    points_cur.Add(thickness);
					points_orlen.Add(thickness);
				}
			}
        }
    }

}