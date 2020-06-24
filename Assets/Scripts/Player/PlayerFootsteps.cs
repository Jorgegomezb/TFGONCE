using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{

    private AudioSource footstep_Sound;

    [SerializeField]
    private AudioClip[] footstep_Clip;

    private CharacterController character_Controller;

    [HideInInspector]
    public float volume_Min, volume_Max;

    private float accumulated_Distance =1;

    [HideInInspector]
    private float step_Distance = 0.5f;

    // Start is called before the first frame update
    void Awake()
    {
        footstep_Sound = GetComponent<AudioSource>();

        character_Controller = GetComponentInParent<CharacterController>();

        Debug.Log("---------------"+step_Distance);
    }

    // Update is called once per frame
    void Update()
    {
        CheckToPlayFootstepSound();
    }

    void CheckToPlayFootstepSound()
    {
        if (!character_Controller.isGrounded)
        {
            //footstep_Sound.volume = Random.Range(volume_Min, volume_Max);
            //footstep_Sound.clip = footstep_Clip[3];
            //footstep_Sound.Play();
            return;
        }

        if(character_Controller.velocity.sqrMagnitude > 0)
        {
            
            // si la distancia acumulada es mayor que la distancia de un paso entra para reproducir el sonido
            if(accumulated_Distance > step_Distance)
            {
                footstep_Sound.PlayOneShot(footstep_Clip[Random.Range(0, footstep_Clip.Length - 1)]);
                accumulated_Distance = 0f;
            }
            // accumulated_Distance es el valor de cuanto podemos andar hasta que suenen los pasos
            accumulated_Distance += Time.deltaTime;
        }
        else
        {
            accumulated_Distance = 0f;
        }
    }
} 
