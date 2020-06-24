using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Return : StateMachineBehaviour
{
    public float run_Speed = 2.5f;
    public Vector3 init_pos;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Smash Attack");
        animator.ResetTrigger("Stab Attack");

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.transform.position -= animator.transform.forward * run_Speed * Time.deltaTime;
        animator.transform.position = Vector3.MoveTowards(animator.transform.position, init_pos, run_Speed*Time.deltaTime);
        if(Vector3.Distance(animator.transform.position, init_pos) <= 0f){
            animator.SetBool("Idle", true);
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
