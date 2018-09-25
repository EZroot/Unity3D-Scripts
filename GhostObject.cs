using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostObject : MonoBehaviour 
{
	public int collideCount = 0;

	void Update()
	{
		if (collideCount > 1)
		{
			Renderer[] renderer = gameObject.GetComponentsInChildren<Renderer> ();
			foreach (Renderer rend in renderer) 
			{
				Material[] mat = new Material[rend.materials.Length];
				for (int i = 0; i < rend.materials.Length; i++) 
				{
					mat [i] = rend.material;
					mat [i].color = Color.red;
				}
				rend.materials = mat;
			}
		}
		if (collideCount == 1)
		{
			Renderer[] renderer = gameObject.GetComponentsInChildren<Renderer> ();
			foreach (Renderer rend in renderer) 
			{
				Material[] mat = new Material[rend.materials.Length];
				for (int i = 0; i < rend.materials.Length; i++) 
				{
					mat [i] = rend.material;
					mat [i].color = Color.green;
				}
				rend.materials = mat;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
			collideCount++;
	}

	void OnTriggerExit(Collider other)
	{
			collideCount--;
	}
}
