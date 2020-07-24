using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PacingNpcAi : NpcAi
{
    // TODO: add variables for health and attack damage, etc.
    
    // AI path finding variables
    // public Transform target;
    protected Vector2 targetPosition;
    [DraggablePoint]
    public Vector2 FirstTargetPosition;
    [DraggablePoint]
    public Vector2 secondTargetPosition;
    protected int currentTarget;

    [SerializeField]
    protected float speed = 100f;
    [SerializeField]
    protected float nextWaypointDistance = 0.5f;
    [SerializeField]
    protected float pathStartTime = 0f;
    [SerializeField]
    protected float strollTime = 10f;  // update path/change destination every _ seconds

    protected Path path;
    protected int currentWaypoint = 0;
    protected bool reachedEndOfPath = false;

    protected Seeker seeker;
    // protected Rigidbody2D rb;
    // protected SpriteRenderer npcGraphics;

    protected Vector2 direction;
    protected float velocityX;
    protected float distToWaypoint; 

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        seeker = GetComponent<Seeker>();
        targetPosition = FirstTargetPosition;
        currentTarget = 1;
        InvokeRepeating("UpdatePath", pathStartTime, strollTime);
    }

    protected virtual void UpdatePath(){
        if (seeker.IsDone()){
            if(currentTarget == 1){
                currentTarget = 2;
                targetPosition = secondTargetPosition;
            } else if (currentTarget == 2){            
                currentTarget = 1;
                targetPosition = FirstTargetPosition;
            }
            seeker.StartPath(npcRb.position, targetPosition, OnPathComplete);
        }
    }

    protected virtual void OnPathComplete(Path p){
        if(!p.error){
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(onPath){
            WalkPath();
        } else {
            ReactToPlayer();
        }
    }

    protected virtual void WalkPath(){
        if(path == null)
            return;
        if(currentWaypoint >= path.vectorPath.Count){
            reachedEndOfPath = true;
            Debug.Log("reached end of path");
            npcRb.velocity = new Vector2(0,0);
            return;
        } else {
            reachedEndOfPath = false;
        }

        direction = ((Vector2)path.vectorPath[currentWaypoint] - npcRb.position).normalized;
        velocityX = direction.x * speed * Time.deltaTime;
        npcRb.velocity = new Vector2(velocityX, npcRb.velocity.y);

        distToWaypoint = Vector2.Distance(npcRb.position, path.vectorPath[currentWaypoint]);

        if(distToWaypoint < nextWaypointDistance){
            currentWaypoint++;
        }
    }

}
