using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour 
{
	public List<GameObject> waypointList = new List<GameObject>();

	public void AddWaypoint()
	{
		GameObject point = new GameObject (transform.name + " Waypoint " + waypointList.Count);
		//point.transform.SetParent (this.transform);
		waypointList.Add (point);
	}

	public void DeleteWaypoint()
	{
		GameObject last = waypointList [waypointList.Count - 1];

		//Final
		DestroyImmediate (last);
		waypointList.Remove(last);
	}

	public List<GameObject> GetWaypoints()
	{
		return waypointList;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		foreach (GameObject go in waypointList) 
		{
			Gizmos.DrawWireSphere (go.transform.position, .5f);
		}
	}
}
