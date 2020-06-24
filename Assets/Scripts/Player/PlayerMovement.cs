using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerMovement : MonoBehaviour
{
    private const float WALL_PLONK_COOLDOWN = 1;
    private const float OUCH_COOLDOWN = 1;

    private float lastPlonkTime;
    private float nextTimeToPlonk;
    private float lastOuchTime;
    private float nextTimeToOuch;

    private bool plonked;
    private bool iNeedHealing;

    private CharacterController character_Controller;

    private Vector3 move_Direction;

    public float speed = 5f;
    private float gravity = 20f;

    public float jump_Force = 10f;
    private float vertical_Velocity;

    public AudioSource knock_sound;
    public AudioSource hurt_sound;

    void Awake(){
        character_Controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update(){
        MoveThePlayer();
    }

    void MoveThePlayer(){
        move_Direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL));

        move_Direction = transform.TransformDirection(move_Direction);
        move_Direction *= speed * Time.deltaTime;
        //deltaTime es el tiempo entre dos frames

        ApplyGravity();

        character_Controller.Move(move_Direction);
    }

    void ApplyGravity()
    {
        // aplica la gravedad independientemente de si ha saltado o no para evitar problemas a la hora de movimiento
        vertical_Velocity -= gravity * Time.deltaTime;

        PlayerJump();

        // si no multiplicas por Time.deltaTime salta demasiado
        move_Direction.y = vertical_Velocity * Time.deltaTime;
    } // gravedad

    void PlayerJump()
    {
        // solo salta si esta en el suelo y se pulsa el espacio
        if(character_Controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            //Handheld.Vibrate();
            vertical_Velocity = jump_Force;
            
        }

    }

    public void playerHurt()
    {
        Debug.Log("Player hurt");
        if (!iNeedHealing && Time.time > nextTimeToOuch)
        {
            hurt_sound.Play();
            iNeedHealing = true;
            nextTimeToOuch = Time.time + OUCH_COOLDOWN;
        }
        
    }

    void OnCollisionEnter (Collision collision){
            if(collision.gameObject.tag=="Wall" && Time.time > nextTimeToPlonk && !plonked){
                nextTimeToPlonk= Time.time + WALL_PLONK_COOLDOWN;
                knock_sound.Play();
                plonked = true;
            }
    	}
	
	void OnCollisionStay (Collision collision){
        if(collision.gameObject.tag=="Wall"){
            GamePad.SetVibration(0,1,0);
        } else {
            GamePad.SetVibration(0,0,0);
        }
	}

	void OnCollisionExit (Collision collisionInfo){
		if(collisionInfo.collider.tag=="Enemy" && iNeedHealing){
            iNeedHealing = false;
        } else if (collisionInfo.collider.tag=="Wall" && plonked) {
            plonked = false;
		}
	}
			
}
