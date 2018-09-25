using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    public void MovePosition(GameObject unit, Vector3 goal)
    {
        NavMeshAgent agent = unit.GetComponent<NavMeshAgent>();
        if (agent == null)
            Debug.LogError("No NavMeshAgent found!");
        agent.destination = goal;
    }

    public void CancelMove()
    {
        transform.position = transform.position;
    }
}
