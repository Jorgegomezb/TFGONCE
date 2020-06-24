using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimator : MonoBehaviour
{
  private Animator anim;
  // Start is called before the first frame update
  void Awake()
  {
    anim = GetComponent<Animator>();
  }

  public Animator GetAnim(){
    return anim;
  }

  public void Idle(bool idle){
    anim.SetBool("Idle", idle);
  }
  public void WalkF(bool walk){
    anim.SetBool("Walk Forward", walk);
  }

  public void WalkB(bool walk){
    anim.SetBool("Walk Backward", walk);
  }
  
  public void StrafeR(bool strafe){
    anim.SetBool("Strafe Right", strafe);
  }

  public void StrafeL(bool strafe){
    anim.SetBool("Strafe Left", strafe);
  }

  public void RunF(bool run){
    anim.SetBool("Run Forward", run);
  }

  public void RunB(bool run){
    anim.SetBool("Run Backward", run);
  }

  public void TurnLeft(){
    anim.SetTrigger("Turn Left");
  }

  public void TurnRight(){
    anim.SetTrigger("Turn Right");
  }

  public void Jump(){
    anim.SetTrigger("Jump");
  }

  public void Stab(){
    anim.SetTrigger("Stab Attack");
  }

  public void Smash(){
    anim.SetTrigger("Smash Attack");
  }

  public void Cast(){
    anim.SetTrigger("Cast Spell");
  }

  public void Defend(bool defend){
    anim.SetBool("Defend", defend);
  }

  public void TakeDamage(){
    anim.SetTrigger("Take Damage");
  }

  public void Die(){
    anim.SetTrigger("Die");
  }
}