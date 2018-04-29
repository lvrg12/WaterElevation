using System.Collections;
using System.Collections.Generic;
 using UnityEngine;
 
 public class Teletransport : MonoBehaviour {
     public GameObject top_viewer;
     public GameObject bot_viewer;
	 public GameObject top_cam;
	 public GameObject bot_cam;
 
     public void SwitchCam ()
	 {	
		if(top_viewer.activeSelf)
		{
			Vector3 p = top_viewer.transform.position;
			bot_viewer.transform.position = p;

			bot_viewer.SetActive(true);
			bot_cam.SetActive(true);

			top_viewer.SetActive(false);
			top_cam.SetActive(true);

		}
		else if(bot_viewer.activeSelf)
		{
			Vector3 p = bot_viewer.transform.position;
			top_viewer.transform.position = p;

			top_viewer.SetActive(true);
			top_cam.SetActive(true);

			bot_viewer.SetActive(false);
			bot_cam.SetActive(true);

		}
 	}
	 
 }