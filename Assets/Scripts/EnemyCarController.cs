using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarController : MonoBehaviour
{
    public Transform[] waypoints;
    private float moveSpeed = 2f;
    private int waypointIndex = 0;

	private void Start () 
    {
        this.transform.position = this.waypoints[this.waypointIndex].transform.position;
	}
	
	private void Update () 
    {
        if (this.waypointIndex <= this.waypoints.Length - 1)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position,
               this.waypoints[this.waypointIndex].transform.position,
               moveSpeed * Time.deltaTime);

            if (this.transform.position == this.waypoints[this.waypointIndex].transform.position)
            {
                this.waypointIndex += 1;
            }
        }
        else 
        {
            this.waypointIndex = 0;
        }
	}
}
