using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonFx : MonoBehaviour
{

    public AudioSource audio_source;
    public AudioClip hover_sound;
    public AudioClip click_sound;
    
    public void HoverSound()
    {
        int gameOver = PlayerPrefs.GetInt("gameOver", 0);
        int victory = PlayerPrefs.GetInt("victory", 0);
        Debug.Log("AUDIO is playing:" + audio_source.isPlaying);
        Debug.Log("victory:" + victory);
        Debug.Log("gao:" + gameOver);
        if (audio_source.isPlaying && audio_source.clip.name != "initial" && gameOver == 0 && victory == 0)
        {
            Debug.Log(audio_source.clip.name);
            audio_source.Stop();
            audio_source.PlayOneShot(hover_sound);
        }
        else if(!audio_source.isPlaying)
        {
            audio_source.PlayOneShot(hover_sound);
        }
    }

    public void ClickSound()
    {
        audio_source.PlayOneShot(click_sound);
    }
}
