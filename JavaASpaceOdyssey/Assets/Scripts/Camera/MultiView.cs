using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiView : MonoBehaviour {
    public GameObject[] cameras;
    public string[] shortcuts;
    public bool changeAudioListener = true;

    
    
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
        {
            for (int i = 0; i < cameras.Length; i++)
            {
                if (Input.GetKeyDown(shortcuts[i]))
                    SwitchCamera(i);
            }
        }
    }

    void SwitchCamera(int indexToSelect)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            // test whether current array index matches camera to make active
        bool cameraActive = (i == indexToSelect);
            cameras[i].GetComponent<Camera>().enabled = cameraActive;
            if (changeAudioListener)
            { 
                cameras[i].GetComponent<AudioListener>().enabled =
                cameraActive;
            }
        }
    }
}
