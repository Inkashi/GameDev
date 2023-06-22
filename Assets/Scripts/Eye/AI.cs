using UnityEngine;
using Pathfinding;
public class AI : MonoBehaviour
{
    Transform target;
    public float speed = 200f;
    public float nextWaypointDist = 3f;
    int curWayPoint = 0;
    public bool EndOfPath = false;
    public bool isFlipped = true;
    float AgroDistance = 5f;
    Path path;
    Seeker seek;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        seek = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        InvokeRepeating("UpdatePath", 0, .1f);
    }

    void Update()
    {
        LookAtPlayer();
    }
    void UpdatePath()
    {
        if (seek.IsDone())
        {
            seek.StartPath(rb.position, target.position, PathComplete);
        }
    }

    void PathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            curWayPoint = 0;
        }
    }
    void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;

        if (transform.position.x > target.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            transform.Find("hpbar").gameObject.transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < target.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            transform.Find("hpbar").gameObject.transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
    void FixedUpdate()
    {

        if (path == null)
        {
            return;
        }
        if (curWayPoint >= path.vectorPath.Count)
        {
            EndOfPath = true;
            return;
        }
        else
        {
            EndOfPath = false;
        }
        if (Mathf.Abs(target.position.x - transform.position.x) < AgroDistance)
        {
            Vector2 direction = ((Vector2)path.vectorPath[curWayPoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;
            rb.AddForce(force);
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[curWayPoint]);

        if (distance < nextWaypointDist)
        {
            curWayPoint++;
        }
    }

}
