using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	
	private GameObject Player;
    private AudioSource audio;
    public AudioClip walk_audio;
    public AudioClip chase_audio;
    public AudioClip attack_audio;
    public float walk_Speed = 0.2f;
    public float run_Speed = 1.5f;
    public float chase_Distance = 7f;
    private float current_Chase_Distance;
    public float attack_Distance = 1.8f;

    public float wait_Before_Attack = 2f;
    private EnemyAnimator enemy_Anim;
    public float movementSpeed = 4;
	void Awake(){
		Player = GameObject.Find("Player");
        enemy_Anim = GetComponent<EnemyAnimator>();
        audio = GetComponent<AudioSource>();
	}
    private bool audio_has_played = false;
    private float nextAudioTime = 10f;
    private float acumulateTime=0f;
    private string last_state="";
    void Update()
    {
        transform.LookAt(Player.transform);
        if(Vector3.Distance(transform.position, Player.transform.position) <= chase_Distance && Vector3.Distance(transform.position, Player.transform.position) > attack_Distance){
            enemy_Anim.Walk(false);
            enemy_Anim.Run(true);
            transform.position += transform.forward * run_Speed * Time.deltaTime;
            audio.clip = chase_audio;
            acumulateTime += Time.deltaTime;
            if(last_state!="chase" || acumulateTime > nextAudioTime){
                audio.Play();
                acumulateTime=0f;
                Debug.Log("chase");
            }
            
            last_state="chase";
            
        }
        else if(Vector3.Distance(transform.position, Player.transform.position) <= attack_Distance){
            enemy_Anim.Run(false);
            enemy_Anim.Walk(false);
            
            //ENEMY ATACKS
            enemy_Anim.Attack();
            transform.position+= transform.forward * 0 * Time.deltaTime;
            audio.clip = attack_audio;
            acumulateTime += Time.deltaTime;
            if(last_state!="attack" || acumulateTime > nextAudioTime){
                 audio.Play();
                 acumulateTime=0f;
                 Debug.Log("attck");
            }
            last_state="attack";
           
        }else{
            enemy_Anim.Walk(true);
             transform.position += transform.forward * walk_Speed * Time.deltaTime;
             audio.clip = walk_audio;
             acumulateTime += Time.deltaTime;
             if(last_state!="walk" || acumulateTime > nextAudioTime){
                audio.Play();
                acumulateTime=0f;
                Debug.Log("walk");
            }
            last_state="walk";
             
        }

       
    }
	
}
    