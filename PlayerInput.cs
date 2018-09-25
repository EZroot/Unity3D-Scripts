using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //Used to Place/Delete buildings that we select from BuildMenu
    PlaceObject placeObject;
    UnitController unitController;
    Inventory inventory;

    public GameObject[] playerUnits;
	public Transform canvasTransform;

    private void Start()
    {
        //Get Componenets
        placeObject = GetComponent<PlaceObject>();

        //Create Componenets
        unitController = gameObject.AddComponent<UnitController>();
        inventory = gameObject.AddComponent<Inventory>();
    }

    private void Update()
    {
		InventoryControls();
		UnitControls(playerUnits);
		TimeControls();
    }

	//Draw resources
	private void OnGUI()
	{
		GUIStyle style = new GUIStyle();
		foreach (Resource r in inventory.resourceList) 
		{
            style.fontSize = 18;
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.black;
            GUI.Label (new Rect (48, (Screen.height/2-50) + r.ID*25, 100, 25), r.name + ": " + r.amount, style);
            style.fontSize = 17;
            style.normal.textColor = Color.white;
            GUI.Label(new Rect(50, (Screen.height / 2 - 50) + r.ID * 25, 100, 25), r.name + ": " + r.amount, style);
        }
    }

    private void InventoryControls()
    {
		//Wood Debug
        Resource myResource = inventory.GetResource(0);

        if(Input.GetKeyDown(KeyCode.O))
        {
            myResource.amount += 5;
            print(myResource.amount);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            myResource.amount -= 5;
            print(myResource.amount);
        }
    }
    private void UnitControls(GameObject[] unit)
    {
        placeObject.MouseInput();

        if (Input.GetMouseButtonDown(1) && !placeObject.GetBuildMode())
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast (ray, out hit, 100.0f, placeObject.groundMask.value)) {
				for (int i = 0; i < unit.Length; i++) {
					unitController.MovePosition (unit [i], new Vector3 (hit.point.x,
						hit.point.y,
						hit.point.z + i));
				}
			}
        }
    }

    private void TimeControls()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.timeScale == 1)
                Time.timeScale = 0;
            else
                Time.timeScale = 1; 
        }
    }
}
