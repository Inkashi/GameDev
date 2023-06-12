using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mush : StateMachineBehaviour
{

	public float speed = 1.2f;
	public float attackRange = 0.3f;

	Transform player;
	Rigidbody2D rb;
	EntityLook Look;

	Transform transform;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		rb = animator.GetComponent<Rigidbody2D>();
		Look = animator.GetComponent<EntityLook>();
		transform = animator.GetComponent<Transform>();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Look.LookAtPlayer();

		Vector2 target = new Vector2(player.position.x, rb.position.y);
		
		transform.position =  Vector2.MoveTowards(rb.position, target, 0.3f* Time.fixedDeltaTime);
        float Dist=Vector2.Distance(player.position, rb.position);
        
		if (Dist <= attackRange)
		{
			animator.SetTrigger("Attack");
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.ResetTrigger("Attack");
	}
}
