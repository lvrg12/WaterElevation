using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraSwitch : MonoBehaviour {

    public Camera[] cams;

    public void camMain()
    {
        cams[0].enabled = true;
        cams[1].enabled = false;
        cams[2].enabled = false;
    }

    public void camElevation()
    {
        cams[0].enabled = false;
        cams[1].enabled = true;
        cams[2].enabled = false;
    }

    public void camAerial()
    {
        cams[0].enabled = false;
        cams[1].enabled = false;
        cams[2].enabled = true;
    }
}
