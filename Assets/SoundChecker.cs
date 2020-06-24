using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundChecker : MonoBehaviour
{
    public AudioClip menu_tutorial;
    private AudioSource audio;
    private bool hasPlayed = false;

    public int time = 0;
    // Start is called before the first frame update
    void Start()
    {
        audio = this.GetComponent<AudioSource>();
        hasPlayed = false;
    }




    //Use fixed update beacuase its called every fixed framerate frame
    void FixedUpdate()
    {

        if (!Input.anyKey && !hasPlayed)
        {

            //Starts counting when no button is being pressed
            time = time + 1;
        }
        else
        {

            // If a button is being pressed restart counter to Zero
            time = 0;
        }

        //Now after 100 frames of nothing being pressed it will do activate this if statement
        if (time == 200)
        {
            Debug.Log("100 frames passed with no input");
            audio.PlayOneShot(menu_tutorial);
            hasPlayed = true;
            //Now you could set time too zero so this happens every 100 frames
            time = 0;
        }

    }
}


