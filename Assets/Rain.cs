using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rain : MonoBehaviour {

    // create two particle systems and an audio
    public ParticleSystem rain, rain1;
    public AudioSource s;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // to-do-list:
    //1. let the first particle system stop playing
    //2. let the second particle system stop playing
    //3. Let the audio stop playing
    public void StopRain()
    {
        rain.Stop();
        rain1.Stop();
        s.Stop();

    }
}
