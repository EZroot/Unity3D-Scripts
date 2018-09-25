using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 *  RESOURCE    ID
 *  Wood        0
 *  Stone       1
 *  Coal        2
 */
public class Inventory : MonoBehaviour
{
    public List<Resource> resourceList = new List<Resource>();

	public Resource wood = AddResource(new Resource (0, "Wood", 0));
	public Resource stone = AddResource(new Resource(1,"Stone",0));
	public Resource coal = AddResource(new Resource(2,"Coal",0));

    public Resource AddResource(Resource resource)
    {
        if(!resourceList.Contains(resource))
            resourceList.Add(resource);
        return resource;
    }

    public Resource GetResource(int id)
    {
        foreach (Resource r in resourceList)
        {
            if(r.ID == id)
            {
                return r;
            }
        }
        Debug.LogError("Resource "+id.ToString()+" not found in inventory.");
        return null;
    }
}

[System.Serializable]
public class Resource
{
    public int ID;
    public string name;
    public int amount;

    //Copy constructor
    public Resource(Resource resource)
    {
        this.ID = resource.ID;
        this.name = resource.name;
        this.amount = resource.amount;
    }
    //Constructor
    public Resource(int id, string name, int amount)
    {
        this.ID = id;
        this.name = name;
        this.amount = amount;
    }
}
