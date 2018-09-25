using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Waypoint))]
public class EnemyAI : MonoBehaviour 
{
	const float MAX_DIST = 0.45f;

	private Waypoint waypointScript;
	private UnitController controller;

	public Vector3[] waypoints;
	private int waypointIndex = 0;

	void Awake()
	{
		controller = gameObject.AddComponent<UnitController> ();
		waypointScript = GetComponent<Waypoint> ();

		waypoints = new Vector3[waypointScript.GetWaypoints().Count];

		for (int i = 0; i < waypointScript.GetWaypoints ().Count; i++) 
		{
			GameObject go = waypointScript.GetWaypoints () [i];
			waypoints [i] = go.transform.position;
		}
	}

	void Update()
	{
		PatrolWaypoints ();
	}

	void PatrolWaypoints()
	{
		controller.MovePosition (this.gameObject, waypoints [waypointIndex]);

		//If we reached our waypoint
		if ((transform.position - waypoints [waypointIndex]).magnitude < MAX_DIST) 
		{
			//check if last waypoint
			if (waypointIndex != waypoints.Length - 1)
				waypointIndex++;
			else
				waypointIndex = 0;
		}
	}
}
