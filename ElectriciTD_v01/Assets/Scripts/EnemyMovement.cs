using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

	#region Variables
	public float startSpeed = 10f;

	[HideInInspector]
	public float speed;
	private int wavepointIndex = 0;

	private Transform target;
	#endregion


	#region Unity Functions
	void Start()
    {
		target = Waypoints.points[0];
    }

    void Update()
    {
		Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * startSpeed * Time.deltaTime, Space.World);

		if (Vector3.Distance(transform.position, target.position) <= .4f)
		{
			GetNextWaypoint();
		}
    }

	#endregion

	#region User Functions
	//Lets the enemy select the next waypoint from the array
	void GetNextWaypoint()
	{
		if (wavepointIndex >= Waypoints.points.Length - 1)
		{
			//enemy is destroyed when reaching end region
			Destroy(gameObject);
			return;
		}

		wavepointIndex++;
		target = Waypoints.points[wavepointIndex];

	}

	#endregion
}
