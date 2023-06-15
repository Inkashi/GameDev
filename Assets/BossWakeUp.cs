using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWakeUp : StateMachineBehaviour
{
    Transform player;
    Transform Boss;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
       Boss = animator.GetComponent<Transform>();
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float Dist = Vector2.Distance(player.position, Boss.position);
        Debug.Log(Dist);
        if (Dist< 1f)
         {
            animator.SetBool("WakeUp",true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

}
