using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class menuScript : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip game_over_audio;
    public AudioClip victory_audio;
    public AudioClip menu_tutorial;
    public AudioClip enter_audio;
    private AudioSource audio;
    private bool hasPlayed = false;
    private int time = 0;
    public GameObject firstButton;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        hasPlayed = false;
        int gameOver = PlayerPrefs.GetInt("gameOver", 0);
        int victory = PlayerPrefs.GetInt("victory", 0);
        if (gameOver == 1)
        {
            audio.PlayOneShot(game_over_audio);
            Debug.Log("GAMEOVER");
            
        }else if (victory == 1)
        {
            audio.PlayOneShot(victory_audio);
            Debug.Log("VICTORY");
            
        }
        else
        {
            audio.clip = enter_audio;
            audio.Play();

        }
        //clear selected
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);

    }

    //Use fixed update beacuase its called every fixed framerate frame
    void FixedUpdate()
    {
        PlayerPrefs.SetInt("gameOver", 0);
        PlayerPrefs.SetInt("victory", 0);
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
        if (time == 200 && !audio.isPlaying)
        {
            Debug.Log("100 frames passed with no input");
            audio.PlayOneShot(menu_tutorial);
            hasPlayed = true;
            //Now you could set time too zero so this happens every 100 frames
            time = 0;
        }

    }
}


