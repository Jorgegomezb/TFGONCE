using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
	
	private GameObject Player;
    private Vector3 target;

    private AudioSource audio;
    public AudioClip boss_audio;
    public AudioClip hit_audio;
    public AudioClip hurt_audio;
    public AudioClip pre_hit_audio;
    public AudioClip pre_smash_audio;
    public AudioClip smash_audio;
    public AudioClip walk_audio;


    public float walk_Speed = 0.2f;
    public float run_Speed = 1.0f;
    public float run_Speed_Backward = 1.5f;
    public float chase_Distance = 5f;
    private float current_Chase_Distance;
    public float attack_Distance = 1.8f;
    public float damage = 1f;

    public Vector3 init_pos;

    public float wait_Before_Attack = 2f;
    private BossAnimator boss_Anim;
    public float movementSpeed = 4;

	void Awake(){
		Player = GameObject.Find("Player");
        boss_Anim = GetComponent<BossAnimator>();
        audio = GetComponent<AudioSource>();
        init_pos= new Vector3(transform.position.x,transform.position.y, transform.position.z);
	}

    private bool audio_has_played = false;
    private float nextAudioTime = 10f;
    private float nextAttack = 10f;
    private float acumulateTime=0f;
    private float chaseTime=0f;
    private string last_state="idle";
    void Update()
    {
        if(boss_Anim.GetAnim().GetCurrentAnimatorStateInfo(0).IsName("Idle")){
            if(audio.clip!=boss_audio){
                audio.clip=boss_audio;
                audio.Play();
                audio.loop=true;
            }
        }
        if(boss_Anim.GetAnim().GetCurrentAnimatorStateInfo(0).IsName("Run Forward")|| boss_Anim.GetAnim().GetCurrentAnimatorStateInfo(0).IsName("Return")){
            if(audio.clip!=walk_audio){
                audio.clip=walk_audio;
                audio.Play();
                audio.loop=true;
            }
        }
        if(boss_Anim.GetAnim().GetCurrentAnimatorStateInfo(0).IsName("Stab Attack")){
            if(audio.clip==pre_hit_audio){
                audio.clip=hit_audio;
                audio.Play();
                Player.transform.GetChild(1).transform.GetComponent<HealthScript>().ApplyDamage(damage);
            }
            if(audio.clip!=hit_audio){
                audio.clip=pre_hit_audio;
                audio.Play();
                audio.loop=false;

            }
        }
        if(boss_Anim.GetAnim().GetCurrentAnimatorStateInfo(0).IsName("Smash Attack")){
            if(audio.clip==pre_smash_audio){
                audio.clip=smash_audio;
                audio.Play();
                Player.transform.GetChild(1).transform.GetComponent<HealthScript>().ApplyDamage(damage);
            }
            if(audio.clip!=smash_audio){
                audio.clip=pre_smash_audio;
                audio.Play();
                audio.loop=false;

            }
        }

        if(boss_Anim.GetAnim().GetCurrentAnimatorStateInfo(0).IsName("Die")){
            if(audio.clip!=hurt_audio){
                audio.clip=hurt_audio;
                audio.Play();
                audio.loop=false;
                Destroy(gameObject, 1F);
            }
        }

    }
	
}