using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDown : StateMachineBehaviour
{
    Boss boss;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
     boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();  
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       boss.cooldown();
    }

    
}
