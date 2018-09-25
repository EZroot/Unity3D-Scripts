using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* Generate when exiting buildmode
 */
public class GenerateNavMesh : MonoBehaviour
{
    NavMeshSurface surface;

    void Start()
    {
        surface = gameObject.GetComponent<NavMeshSurface>();
    }

    public void BuildNavMesh()
    {
        surface.BuildNavMesh();
    }
}
