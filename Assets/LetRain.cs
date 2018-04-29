using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetRain : MonoBehaviour {

    // create two particle systems and an audio
    public ParticleSystem rain,rain1;
    public AudioSource s;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // to-do-list:
    //1. let the first particle system play
    //2. let the second particle system play
    //3.Let the audioplay
    public void Activate()
    {
        rain.Play();
        rain1.Play();
        s.Play();
    }
}
