using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/*
 * Create a custom BuildingManager to call methods from
 * Create a custom Building to copy data to
 * 
 * using bm we call methods, using custom body we can translate the information to building were about to create
 */
 //[RequireComponent(typeof(BuildMenu))]
public class PlaceObject : MonoBehaviour 
{
	BuildingManager buildingManager;
    BuildMenu buildMenu;
    
    //Set in BuildMenu.cs
    private Building selectedBuilding;
	public LayerMask groundMask;
	public LayerMask buildingMask;

	private GameObject ghostObj = null;
	private GhostObject ghostScript;
	public Material transparentMat;
	public bool isColliding = false;

	void Start()
	{
		buildingManager = gameObject.AddComponent<BuildingManager> ();
        //Menus
        buildMenu = gameObject.GetComponent<BuildMenu>();
	}

	public void MouseInput()
	{
		//While mouse not on UI
		if (!EventSystem.current.IsPointerOverGameObject()) 
		{
            //***************
            // Build Menu
            //***************
            if (buildMenu.GetBuildMode())
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                //If hitting ground
                if (Physics.Raycast(ray, out hit, 100.0f, groundMask.value))
                {
					UpdateGhostBuilding (ghostObj, hit.point);

                    //Left Click
					if (Input.GetMouseButtonDown(0) && ghostScript.collideCount == 1)
                    {
						//Apply ghost transform to selectedBuilding
						selectedBuilding.Position = ghostObj.transform.position;
						selectedBuilding.Rotation = ghostObj.transform.rotation;

                        //If not colliding with any other objects (just ground = 1)
                        if (selectedBuilding != null)
                        {
                            CreateBuilding(selectedBuilding);
                        }
                        else
                        {
                            Debug.LogError("Selected Building returned Null!");
                        }
                    }
                }

                //Right Click delete
                if (Input.GetMouseButtonDown(1))
                {
                    //If hitting building 
                    if (Physics.Raycast(ray, out hit, 100.0f, buildingMask.value))
                    {
                        DeleteBuilding(hit.transform.GetComponent<BuildingManager>());
                    }
                }
            }
		}

		//Delete ghost obj
		if (!buildMenu.GetBuildMode () && ghostObj != null)
			Destroy (ghostObj);
	}

	private void CreateBuilding(Building building)
	{
		buildingManager.CreateBuilding (building);
	}

	private void DeleteBuilding(BuildingManager manager)
	{
		manager.DestroyBuilding ();
	}

    public void SetSelectedBuilding(Building building)
    {
        selectedBuilding = building;
		CreateGhostBuilding (building, transparentMat);
    }

    public bool GetBuildMode()
    {
        return buildMenu.GetBuildMode();
    }

	private void CreateGhostBuilding(Building building, Material transparentMaterial)
	{
		if (ghostObj != null)
			Destroy (ghostObj);
		ghostObj = Instantiate (GameManager.Instance.buildingPrefabs [building.ID]);
		ghostScript = ghostObj.AddComponent<GhostObject> ();
		Rigidbody rb = ghostObj.AddComponent<Rigidbody> ();
		rb.isKinematic = true;
		rb.useGravity = false;
		ghostObj.GetComponent<BoxCollider> ().isTrigger = true;

		Renderer[] renderer = ghostObj.GetComponentsInChildren<Renderer> ();
		foreach (Renderer rend in renderer) 
		{
			Material[] mat = new Material[rend.materials.Length];
			for (int i = 0; i < rend.materials.Length; i++) 
			{
				mat [i] = transparentMaterial;
				print ("Loaded Mats");
			}
			rend.materials = mat;
		}
	}

	private void UpdateGhostBuilding(GameObject ghostObj, Vector3 pos)
	{
		//Input
		if (ghostObj != null) 
		{
			if (Input.GetKey (KeyCode.E))
				ghostObj.transform.Rotate (Vector3.up * -90 * Time.deltaTime);
			if (Input.GetKey (KeyCode.Q))
				ghostObj.transform.Rotate (Vector3.up * 90 * Time.deltaTime);

			ghostObj.transform.position = pos;
		}
	}
}
