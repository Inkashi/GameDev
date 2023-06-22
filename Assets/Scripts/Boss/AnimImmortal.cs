using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimImmortal : StateMachineBehaviour
{
    BossControl bossControl;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossControl = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossControl>();
        bossControl.immortal = true;
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       bossControl.immortal = false;
    }

   
}
