using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public KillCount cont;

    private GameObject Player;
    private Animator animator;
    private AudioSource audio;
    private Rigidbody enemyRigidbody;
    public float damage=2;
    private Vector3 moveDirection;
    private float accumulateTime=7f;
    private float nextDirectionTime=7f;
    private float nextAttackTime = 4f;
    private bool is_dead = false;
    private bool is_damaged = false;
    public float speed = 1.5f;
    private float speed_variable;
    private bool pre_attackSound = true;

    /*Audio*/
    public AudioClip walk_audio;
    public AudioClip hit_audio;
    public AudioClip attack_audio;
    public AudioClip dead_audio;
    public AudioClip pre_hit_audio;
    /*Private functions animation*/
    public void Walk(bool walk)
    {
        if (is_dead == true)
        {
            return;
        }
        animator.SetBool("Walk Forward", walk);
    }

    public void Attack()
    {
        if (is_dead == true)
        {
            return;
        }
        animator.SetTrigger("Smash Attack");
    }
    public void Damaged()
    {
        if (is_dead == true)
        {
            return;
        }
        Debug.Log("Im hurt");
        this.Walk(false);
        animator.SetTrigger("Take Damage");
        audio.Stop();
        audio.clip = hit_audio;
        audio.Play();
        Debug.Log("Im hurt2");

    }


    public void Dead()
    {
        Walk(false);
        animator.SetTrigger("Die");
        Debug.Log("Im dead");
        if (audio.isPlaying)
        {
            audio.Stop();
        }
        audio.PlayOneShot(dead_audio,1F);
        Destroy(gameObject,0.7F);
        
        is_dead = true;

        cont.count++;

        /*Destroy(this.gameObject); //destroys the object after animation ended
        Debug.Log(animator.GetCurrentAnimatorClipInfo(0));
        AnimatorClipInfo[] m_CurrentClipInfo = animator.GetCurrentAnimatorClipInfo(0);
        float m_CurrentClipLength = m_CurrentClipInfo[0].clip.length;
        Debug.Log(m_CurrentClipLength);*/
    }

    /*Awake*/
    private void Awake()
    {
        Player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        enemyRigidbody = GetComponent<Rigidbody>();
        speed_variable = this.speed;
        pre_attackSound = true;

    }

    private void canWalk()
    {
        if (is_dead == true)
        {
            return;
        }
        if (audio.isPlaying && audio.clip.name == "Hurt")
        {
            this.Walk(false);
            this.speed_variable = 0;

        }
        else
        {
            this.Walk(true);
            
            this.speed_variable = this.speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(is_dead == true)
        {
            return;
        }
        if (Vector3.Distance(Player.transform.position, this.transform.position) < 10) //si entra en la zona de ataque
        {

            moveDirection = Vector3.zero;
            //Gets direction of player
            Vector3 direction = Player.transform.position - this.transform.position;
            direction.y = 0;
            //Rotates in direction of player
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                        Quaternion.LookRotation(direction), 0.1f);
            if (direction.magnitude > 2.5)
            {
                moveDirection = direction.normalized;
                canWalk();
                if (accumulateTime >= nextAttackTime)
                {
                    audio.clip = walk_audio;
                    audio.Play();
                    accumulateTime = 0f;
                }
            }
            else
            {
                this.Walk(false);
                if(accumulateTime >= nextAttackTime)
                {
                    this.Attack();
                    audio.PlayOneShot(attack_audio);
                    Debug.Log("AUDIO PLAY ATTACK");
                    //audio.Play();
                    Player.transform.GetChild(1).transform.GetComponent<HealthScript>().ApplyDamage(damage);
                    accumulateTime = 0f;
                    pre_attackSound = true;

                }
                else
                {
                    if (pre_attackSound)
                    {
                        audio.PlayOneShot(pre_hit_audio);
                        pre_attackSound = false;
                    }
                    
                }
                
            }
            accumulateTime += Time.deltaTime;
        }
        else
        {
            if (accumulateTime >= nextDirectionTime) //Cada x tiempo cambia de direccion aleatoriamente
            {
                moveRandomDir();
                audio.clip = walk_audio;
                audio.Play();
            }
            else //Sigue moviendose
            {
                canWalk();
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                            Quaternion.LookRotation(moveDirection), 0.1f);
                accumulateTime += Time.deltaTime;
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if(accumulateTime > 1f && collision.gameObject.name != "Player" && collision.gameObject.name != "Plane")
        {
            moveDirection.x = -1 * moveDirection.x;
            moveDirection.z = -1 * moveDirection.z;
            accumulateTime = 0f;
        }

    }
    private void moveRandomDir()
    {
        moveDirection = Vector3.zero;
        Vector3 direction = (new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f)));
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                    Quaternion.LookRotation(direction), 0.1f);
        moveDirection = direction.normalized;
        accumulateTime = 0f;

        this.Walk(true);
    }
    void FixedUpdate()
    {
        if(is_dead == true)
        {
            return; 
        }
        this.enemyRigidbody.velocity = (moveDirection * speed_variable);
    }
}
