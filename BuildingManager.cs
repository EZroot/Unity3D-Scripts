using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour 
{
    public void CreateBuilding(Building building)
	{
		GameObject go = Instantiate (GameManager.Instance.buildingPrefabs[building.ID], building.Position, building.Rotation);
        go.AddComponent<BuildingManager> ();
	}

	//Create array of buildingMangers to update all building at once.
	public void UpdateBuilding(Building building)
	{
	}

	public void DestroyBuilding()
	{
		Destroy (this.gameObject);
	}
}

public class Building
{

    public int ID;
    public Vector3 Position;
	public Quaternion Rotation;

	public Building(int id, Vector3 pos, Quaternion rot)
	{
		this.ID = id;
		this.Position = pos;
		this.Rotation = rot;
	}
}