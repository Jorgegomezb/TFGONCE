using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioskip : MonoBehaviour
{
    private AudioSource audio;
    private MenuManager menum;
    public string next_scene;
    // Start is called before the first frame update
    void Start()
    {
        audio = this.GetComponent<AudioSource>();
        menum = GameObject.Find("MenuManager").GetComponent<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (audio.isPlaying)
        {
            if ((Input.anyKey))
            {
                audio.Stop();
                menum.loadScene(next_scene);
            }
        }
        else
        {
            menum.loadScene(next_scene);
        }
    }
}
