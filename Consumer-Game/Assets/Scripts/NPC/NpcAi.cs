using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NpcAi : MonoBehaviour
{
    // TODO: add variables for health and attack damage, etc.
    
    // AI path finding variables
    public Transform target;

    public float speed = 100f;
    public float nextWaypointDistance = 1f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    // public Transform npcGraphics;
    SpriteRenderer npcGraphics;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        npcGraphics = GetComponent<SpriteRenderer>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath(){
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p){
        if(!p.error){
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(path == null)
            return;
        if(currentWaypoint >= path.vectorPath.Count){
            reachedEndOfPath = true;
            Debug.Log("reached end of path");
            return;
        } else {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 velocity = direction * speed * Time.deltaTime;
        // Debug.Log(force);
        // rb.AddForce(force);
        rb.velocity = velocity;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance){
            currentWaypoint++;
        }

        if (rb.velocity.x >= 0.01f){
            npcGraphics.flipX = false;
        } else if (rb.velocity.x < -0.01f){
            npcGraphics.flipX = true;
        }
    }
}
