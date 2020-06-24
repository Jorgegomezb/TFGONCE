using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{
    private GameObject Player;
    private Vector3 target;
    public Vector3 init_pos;
    public float chase_Distance = 10f;
    public float attack_Distance = 2.5f;
    public float run_Speed = 2.0f;

    private int rand;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!Player){
            Player = GameObject.Find("Player");
            target= new Vector3(Player.transform.position.x,Player.transform.position.y,Player.transform.position.z);
        }
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Run Forward", false);
        animator.transform.position += animator.transform.forward * run_Speed * Time.deltaTime;
        rand= Random.Range(0,2);
        if(Vector3.Distance(animator.transform.position, Player.transform.position) <= attack_Distance || Vector3.Distance(init_pos, animator.transform.position) >= chase_Distance ){
            if(rand==0){
                animator.SetTrigger("Smash Attack");
            }else{
                animator.SetTrigger("Stab Attack");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
