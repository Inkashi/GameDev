using System.Collections;
using System.Drawing;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class Mush : StateMachineBehaviour
{
    public float speed = 1.9f;
    public float attackRange = 0.3f;
    private float AgroDistance = 4f;

    Transform player;
    Rigidbody2D rb;
    EntityLook Look;
    Transform mush;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        Look = animator.GetComponent<EntityLook>();
        mush = animator.GetComponent<Transform>();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Look.LookAtPlayer();
        float distance = player.position.x - mush.position.x;
        Vector3 pos = mush.position;
        pos -= mush.right * 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(pos, new Vector2(0, -2), 4f, LayerMask.GetMask("Default"));
        if (hit.collider != null)
        {
            if (Mathf.Abs(distance) < AgroDistance)
            {
                mush.position += -mush.right * speed * Time.deltaTime;
            }
        }
        else
        {
            if (mush.position.y < player.position.y - 20)
            {
                mush.GetComponent<Enemy>().Destroyed();
                player.GetComponent<HeroMovement>().SetPoint(-10);
            }
        }

        float Dist = Vector2.Distance(player.position, rb.position);

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
