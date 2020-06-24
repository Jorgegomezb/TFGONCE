using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public bool is_Player, is_Scarab, is_Boss;

    public float max_health = 10f;
    public float current_health = 10f;
    private bool is_Dead;
    public AudioClip life_audio;
    private AudioSource audio;
    private EnemyBehaviour enemy_script;
    private PlayerMovement player_script;
    BossAnimator boss_Anim;
    public MenuManager men;

    private float accumulateTime = 0;
    private float nextaudioTime = 2f;

    // Start is called before the first frame update
    void Awake()
    {
        enemy_script = GetComponent<EnemyBehaviour>();
        player_script = GetComponent<PlayerMovement>();
        audio = GetComponent<AudioSource>();
        audio.clip = life_audio;
        boss_Anim= GetComponent<BossAnimator>();
    }

    public void ApplyDamage(float damage)
    {

        // if we died don't execute the rest of the code
        if (is_Dead)
            return;
        if (is_Player)
        {
            current_health -= damage;
            //player_script.playerHurt();
        }
        if (is_Scarab)
        {
            enemy_script.Damaged();
            current_health -= damage;
        }
        // Vemos si es boss y si no está haciendo alguno de sus ataques para quitarle vida
        if (is_Boss && !boss_Anim.GetAnim().GetCurrentAnimatorStateInfo(0).IsName("Smash Attack") && !boss_Anim.GetAnim().GetCurrentAnimatorStateInfo(0).IsName("Stab Attack"))
        {
            boss_Anim.TakeDamage();
            current_health -= damage;

        }
        if (current_health <= 0f)
        {
            if (is_Player)
            {
                Cursor.lockState = CursorLockMode.None;
                StartCoroutine(changeGameOverScene("gameOver"));
                
            }
            if (is_Scarab)
            {
                enemy_script.Dead();
            }
            if (is_Boss)
        	{
	        	
                Debug.Log("BOSS DIES");
                StartCoroutine(changeGameOverScene("victory"));
                boss_Anim.Die();
            }
            is_Dead = true;
        }

    }

    IEnumerator changeGameOverScene(string _final)
    {
        yield return new WaitForSeconds(1.0f);
        PlayerPrefs.SetInt(_final, 1);
        men.loadScene("MenuScene");
    }

    private void Update()
    {
        if (is_Player)
        {
            if(current_health < max_health/2)
            {
                //Debug.Log("Health is not good");
                if (accumulateTime >= 1f)
                {
                    audio.clip = life_audio;
                    audio.Play();
                    accumulateTime = 0;
                }
                accumulateTime += Time.deltaTime;

            }
            else if(current_health < max_health/5)
            {
                //Debug.Log("Health BAD");
                if (accumulateTime >= 0.5f)
                {
                    audio.clip = life_audio;
                    audio.Play();
                    accumulateTime = 0;
                }
                accumulateTime += Time.deltaTime;
            }
        }
    }
}
