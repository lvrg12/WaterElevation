using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Material[] orig_mat;
	public Material[] other_mat;

	private List<GameObject> wells = new List<GameObject>();
	private List<GameObject> markers = new List<GameObject>();
	private List<GameObject> depths = new List<GameObject>();
	private List<GameObject> waters = new List<GameObject>();
	private List<GameObject> boxs = new List<GameObject>();
    private List<GameObject> points = new List<GameObject>();
	private List<float> points_orlen = new List<float>();
    private List<float> points_cur = new List<float>();
	private List<float> wes = new List<float>();
	private List<float> deps = new List<float>();
	private List<float> les = new List<float>();
	private List<float> sts = new List<float>();
    private List<float> sts_orlen = new List<float>();
	private float scale;
   
    void Start ()
	{
		SetWells();
		SetUGWater();
		UpdateColors();
    }

	void Update ()
	{
		
	}

	public void UpdateColors()
	{
		float a,b,x;
		for(int i=0; i<wells.Count; i++)
        {
			a = wes[i];
			x = les[i]-deps[i];
			b = wes[i]-sts[i];
            if(x < a && b < x)
			{
				markers[i].GetComponent<SpriteRenderer>().color = Color.green;
                depths[i].GetComponent<Renderer>().materials = orig_mat;
			}
			else
			{
				markers[i].GetComponent<SpriteRenderer>().color = Color.red;
                depths[i].GetComponent<Renderer>().materials = other_mat;
			}

            sts[i] = sts_orlen[i];
        }

        for(int i=0; i<points.Count; i++)
        {
            points_cur[i] = points_orlen[i];
        }
	}

	public void SetVisibility(bool b)
    {
        for(int i=0; i<points.Count; i++)
        {
            points[i].SetActive(b);
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

	void SetWells()
	{
		TextAsset txtAsset = (TextAsset)Resources.Load("Lubbock_optimized", typeof(TextAsset));
		string[] lines = txtAsset.text.Split('\n');
		scale = 0.0625f;

        for(int index =1;index < lines.Length-1; index++)
        {
            string[] values = lines[index].Split(',');
			float longitude,latitude,well_depth,thickness,water_el,land_el;
			latitude = float.Parse(values[2]);
			longitude = float.Parse(values[3]);
			well_depth = float.Parse(values[4]);
			land_el = float.Parse(values[5]);
			water_el = float.Parse(values[6]);
			thickness = float.Parse(values[7]);
			//lsd = float.Parse(values[8]);

			if (longitude >= -101.9217f && longitude <= -101.83467)
			{
				if (latitude >= 33.54782 && latitude <= 33.62077)
				{
					float xPos = (longitude - -102.0156f) * 1862.28756f;
					float zPos = (latitude - 33.47297f) * 2217.098262f;

					GameObject well = Instantiate(water_well, new Vector3 (xPos, scale*land_el+3.5f, zPos), Quaternion.identity);
					GameObject box = Instantiate(container_cube, new Vector3 (xPos, scale*land_el+3.5f, zPos), Quaternion.identity);
					GameObject marker = Instantiate(well_marker, new Vector3 (xPos, 150f, zPos), Quaternion.Euler(new Vector3(80,0,0)));
                    GameObject Wspring = Instantiate(Spring, new Vector3(xPos, scale * land_el, zPos), Quaternion.identity);
                    
					//GameObject depth = Instantiate(depth_object, new Vector3(xPos, scale*land_el , zPos), Quaternion.identity);
					//GameObject water = Instantiate(water_cyl, new Vector3(xPos, scale*water_el - (scale*thickness), zPos), Quaternion.identity);

					GameObject depth = Instantiate(depth_object, new Vector3(xPos, scale*land_el , zPos), Quaternion.identity);
					GameObject water = Instantiate(water_cyl, new Vector3(xPos, scale*water_el, zPos), Quaternion.identity);

                    depth.transform.localScale = new Vector3(0.75f, scale * well_depth, 0.75f);
					depth.transform.localPosition = new Vector3(xPos,scale*land_el,zPos);

					water.transform.localScale = new Vector3(5.0f, scale * thickness, 5.0f);
					float w = newYScale(water,scale * thickness);
					water.transform.localPosition = new Vector3(xPos,scale*water_el - w,zPos);

					marker.transform.localScale = new Vector3(10,10,10);

					well.name = values [0];
					box.name = values [0];
					marker.name = values[0]+"_marker";
					depth.name = values[0]+"_well";
					water.name = values[0]+"_st";

					var info1 = "\nLocation: "+ longitude +", "+latitude;
                    var info2 = "\nCounty: "+values[1];
					var info3 = "\nWell Depth: "+ well_depth;
                    var info4 = "\nLand Elevation: "+values[5];
					var info5 = "\nWater Elevation: " + values [6];
                    var info6 = "\nSaturated Thickness: " + values [7];
					var info7 = "\nLast Measurement On: " + values [9] + "/" + values [10] + "/" + values[11];

					box.GetComponent<DisplayInfo> ().i1 = info1;
                    box.GetComponent<DisplayInfo> ().i2 = info2;
                    box.GetComponent<DisplayInfo> ().i3 = info3;
                    box.GetComponent<DisplayInfo> ().i4 = info4;
                    box.GetComponent<DisplayInfo> ().i5 = info5;
                    box.GetComponent<DisplayInfo> ().i6 = info6;
                    box.GetComponent<DisplayInfo> ().i7 = info7;
					marker.GetComponent<SpriteRenderer>().color = Color.green;

					wells.Add(well);
					markers.Add(marker);
					depths.Add(depth);
					waters.Add(water);
					boxs.Add(box);
					wes.Add(water_el);
					sts.Add(thickness);
                    sts_orlen.Add(thickness);
					deps.Add(well_depth);
					les.Add(land_el);
				}
			}
        }
    }

	void SetUGWater()
	{
        TextAsset txtAsset = (TextAsset)Resources.Load("raster_to_point", typeof(TextAsset));
        string[] lines = txtAsset.text.Split('\n');

        for(int index =1;index < lines.Length-1; index++)
        {
            string[] values = lines[index].Split(',');
            float longitude = float.Parse(values[2]);
            float latitude = float.Parse(values[3]);
            float thickness = float.Parse(values[1]);

			if (longitude >= -102.0156f && longitude <= -101.74713)
			{
				if (latitude >= 33.47297 && latitude <= 33.69849) 
				{
					float xPos = (longitude - -102.0156f) * 1862.28756f;
					float zPos = (latitude - 33.47297f) * 2217.098262f;
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

	public void SetSpring()
    {
        TextAsset txtAsset = (TextAsset)Resources.Load("precipitation_data", typeof(TextAsset));
        string[] lines = txtAsset.text.Split(new char[] { '\n' });
        string[] values = lines[4].Split(',');
        
        float numVal1, numVal2, numVal3, numVal4, numVal5, numVal6, numVal7, numVal8, numVal9, numVal10, numVal11, numVal12;
        float.TryParse(values[1], out numVal1);
        float.TryParse(values[2], out numVal2);
        float.TryParse(values[3], out numVal3);
        float.TryParse(values[4], out numVal4);
        float.TryParse(values[5], out numVal5);
        float.TryParse(values[6], out numVal6);
        float.TryParse(values[7], out numVal7);
        float.TryParse(values[8], out numVal8);
        float.TryParse(values[9], out numVal9);
        float.TryParse(values[10], out numVal10);
        float.TryParse(values[11], out numVal11);
        float.TryParse(values[12], out numVal12);

        // calculate the average precipitation data of four seasons
        float average_Spring = (numVal1 + numVal11 + numVal12) / 3 * 0.0254f;
       
		for(int i=0; i<points.Count; i++)
        {
            points_cur[i] = points_orlen[i] + average_Spring;
			newYScale(points[i], scale * points_cur[i]);
        }

		for(int i=0; i<wells.Count; i++)
        {
            sts[i] = sts_orlen[i] + average_Spring;
			newYScale(waters[i], scale * sts[i]);
            boxs[i].GetComponent<DisplayInfo> ().i6 = "\nSaturated Thickness: " + sts[i];
        }
        
		UpdateColors();
    }

    // to-do-list:
    // 1.Read data from a csv file called "precipitation_data"
    // 2.read the data in each line into an array and read each value in the array into new array
    // 3.calculate the average precipitation data for the Summer
    // 4.read data from a csv file called "raster_to_point",then set the longitude, latitude and thickness
    // 5.Instantiate prefab called "water_cube", created the water layer
    // 6.set the average precipitation data to be the water layer thickness 
    public void SetSummer()
    {
        TextAsset txtAsset = (TextAsset)Resources.Load("precipitation_data", typeof(TextAsset));
        string[] lines = txtAsset.text.Split(new char[] { '\n' });
        string[] values = lines[4].Split(',');

        float numVal1, numVal2, numVal3, numVal4, numVal5, numVal6, numVal7, numVal8, numVal9, numVal10, numVal11, numVal12;
        float.TryParse(values[1], out numVal1);
        float.TryParse(values[2], out numVal2);
        float.TryParse(values[3], out numVal3);
        float.TryParse(values[4], out numVal4);
        float.TryParse(values[5], out numVal5);
        float.TryParse(values[6], out numVal6);
        float.TryParse(values[7], out numVal7);
        float.TryParse(values[8], out numVal8);
        float.TryParse(values[9], out numVal9);
        float.TryParse(values[10], out numVal10);
        float.TryParse(values[11], out numVal11);
        float.TryParse(values[12], out numVal12);

        // calculate the average precipitation data of four seasons
       
        float average_Summer = (numVal2 + numVal3 + numVal4) / 3 * 0.0254f;

		for(int i=0; i<points.Count; i++)
        {
            points_cur[i] = points_orlen[i] + average_Summer;
			newYScale(points[i], scale * points_cur[i]);
        }

		for(int i=0; i<wells.Count; i++)
        {
            sts[i] = sts_orlen[i] + average_Summer;
			newYScale(waters[i], scale * sts[i]);
            boxs[i].GetComponent<DisplayInfo> ().i6 = "\nSaturated Thickness: " + sts[i];
        }

		UpdateColors();

    }

    // to-do-list:
    // 1.Read data from a csv file called "precipitation_data"
    // 2.read the data in each line into an array and read each value in the array into new array
    // 3.calculate the average precipitation data for the Fall
    // 4.read data from a csv file called "raster_to_point",then set the longitude, latitude and thickness
    // 5.Instantiate prefab called "water_cube", created the water layer
    // 6.set the average precipitation data to be the water layer thickness 
    public void SetFall()
    {
        TextAsset txtAsset = (TextAsset)Resources.Load("precipitation_data", typeof(TextAsset));
        string[] lines = txtAsset.text.Split(new char[] { '\n' });
        string[] values = lines[4].Split(',');

        float numVal1, numVal2, numVal3, numVal4, numVal5, numVal6, numVal7, numVal8, numVal9, numVal10, numVal11, numVal12;
        float.TryParse(values[1], out numVal1);
        float.TryParse(values[2], out numVal2);
        float.TryParse(values[3], out numVal3);
        float.TryParse(values[4], out numVal4);
        float.TryParse(values[5], out numVal5);
        float.TryParse(values[6], out numVal6);
        float.TryParse(values[7], out numVal7);
        float.TryParse(values[8], out numVal8);
        float.TryParse(values[9], out numVal9);
        float.TryParse(values[10], out numVal10);
        float.TryParse(values[11], out numVal11);
        float.TryParse(values[12], out numVal12);

        // calculate the average precipitation data of four seasons
       
        float average_Fall = (numVal5 + numVal6 + numVal7) / 3 * 0.0254f;
        
        for(int i=0; i<points.Count; i++)
        {
            points_cur[i] = points_orlen[i] + average_Fall;
			newYScale(points[i], scale * points_cur[i]);
        }

		for(int i=0; i<wells.Count; i++)
        {
            sts[i] = sts_orlen[i] + average_Fall;
			newYScale(waters[i], scale * sts[i]);
            boxs[i].GetComponent<DisplayInfo> ().i6 = "\nSaturated Thickness: " + sts[i];
        }

		UpdateColors();
    }

    // to-do-list:
    // 1.Read data from a csv file called "precipitation_data"
    // 2.read the data in each line into an array and read each value in the array into new array
    // 3.calculate the average precipitation data for the Winter
    // 4.read data from a csv file called "raster_to_point",then set the longitude, latitude and thickness
    // 5.Instantiate prefab called "water_cube", created the water layer
    // 6.set the average precipitation data to be the water layer thickness 

    public void SetWinter()
    {
        TextAsset txtAsset = (TextAsset)Resources.Load("precipitation_data", typeof(TextAsset));
        string[] lines = txtAsset.text.Split(new char[] { '\n' });
        string[] values = lines[4].Split(',');

        float numVal1, numVal2, numVal3, numVal4, numVal5, numVal6, numVal7, numVal8, numVal9, numVal10, numVal11, numVal12;
        float.TryParse(values[1], out numVal1);
        float.TryParse(values[2], out numVal2);
        float.TryParse(values[3], out numVal3);
        float.TryParse(values[4], out numVal4);
        float.TryParse(values[5], out numVal5);
        float.TryParse(values[6], out numVal6);
        float.TryParse(values[7], out numVal7);
        float.TryParse(values[8], out numVal8);
        float.TryParse(values[9], out numVal9);
        float.TryParse(values[10], out numVal10);
        float.TryParse(values[11], out numVal11);
        float.TryParse(values[12], out numVal12);

        // calculate the average precipitation data of four seasons
        
        float average_Winter = (numVal8 + numVal9 + numVal10) / 3 * 0.0254f;

        for(int i=0; i<points.Count; i++)
        {
            points_cur[i] = points_orlen[i] + average_Winter;
			newYScale(points[i], scale * points_cur[i]);
        }

		for(int i=0; i<wells.Count; i++)
        {
            sts[i] = sts_orlen[i] + average_Winter;
			newYScale(waters[i], scale * sts[i]);
            boxs[i].GetComponent<DisplayInfo> ().i6 = "\nSaturated Thickness: " + sts[i];
        }

		UpdateColors();
    }

    public void SetRainTest()
    {        
        float average_test = 60.0f;

        for(int i=0; i<points.Count; i++)
        {
            points_cur[i] = points_orlen[i] + average_test;
			newYScale(points[i], scale * points_cur[i]);
        }

		for(int i=0; i<wells.Count; i++)
        {
            sts[i] = sts_orlen[i] + average_test;
			newYScale(waters[i], scale * sts[i]);
            boxs[i].GetComponent<DisplayInfo> ().i6 = "\nSaturated Thickness: " + sts[i];
        }

		UpdateColors();
    }

    public void SetDroughtTest()
    {        
        float average_test = -80.0f;

        for(int i=0; i<points.Count; i++)
        {
            if(points_orlen[i] + average_test <= 0)
                average_test = points_orlen[i] * -1 + 0.5f;

            points_cur[i] = points_orlen[i] + average_test;
			newYScale(points[i], scale * points_cur[i]);
        }

        average_test = -80.0f;

		// for(int i=0; i<wells.Count; i++)
        // {
        //     if(sts_orlen[i] + average_test <= 0)
        //         average_test = sts_orlen[i] * -1 + 0.5f;

        //     sts[i] = sts_orlen[i] + average_test;
		// 	newYScale(waters[i], scale * sts[i]);
        //     boxs[i].GetComponent<DisplayInfo> ().i6 = "\nSaturated Thickness: " + sts[i];
        // }

        for(int i=wells.Count-1; i>=0; i--)
        {
            if(sts_orlen[i] + average_test <= 0)
                average_test = sts_orlen[i] * -1 + 0.5f;

            sts[i] = sts_orlen[i] + average_test;
			newYScale(waters[i], scale * sts[i]);
            boxs[i].GetComponent<DisplayInfo> ().i6 = "\nSaturated Thickness: " + sts[i];
        }

		UpdateColors();
    }
}