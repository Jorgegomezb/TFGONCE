using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject enemyPrefab;
    private AudioSource audio;
    // Update is called once per frame
    private void Awake()
    {
        audio = this.GetComponent<AudioSource>();
    }
    void Update()
    {
        if (audio.isPlaying)
        {
            if ((Input.anyKey))
            {
                audio.Stop();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire2"))
            {
                Instantiate(enemyPrefab, this.transform.position , Quaternion.identity);
            }
        }
       
        
    }
}
