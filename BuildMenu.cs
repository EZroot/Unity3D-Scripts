using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Navmesh Updates are done through here
public class BuildMenu : MonoBehaviour
{
    /*init building based on gui to follow mouse
     * when click, init another building there
     * 
     * on building gui switch
     * destroy()building
     * repeat
----------------------------------------------------------------------------------------------
	*/

	/*
	Use: Hide/Show menu's to hide/show all the button children
	*/
    public GameObject menuPanel;
    public GameObject buildMenuPanel;
    public GameObject goBackPanel;

    public Button[] buildButtons;
    public Button buildModeButton;
    public Button goBackButton;

    //Must be initialized
	/*
	 * read json files, set array to file.count;
	 */
    Building[] buildings = new Building[2];

    PlaceObject placeObject;

    //Add placemode to disable gui and enable placement
    //Buildmode/placemode cannot be at the same time
    void Start()
    {
        placeObject = GetComponent<PlaceObject>();
        LoadContent();
    }

    void OnEnable()
    {
        //Menu
        //BuildMode
        buildModeButton.onClick.AddListener(delegate { SetBuildMode(true); HideMenu(menuPanel); ShowMenu(buildMenuPanel); });

        //Disable gui on click, only reenable when esc button or if clicked 'build menu' again
        buildButtons[0].onClick.AddListener(delegate { SetCurrentBuilding(buildings[0]); HideMenu(buildMenuPanel); ShowMenu(goBackPanel); });
        buildButtons[1].onClick.AddListener(delegate { SetCurrentBuilding(buildings[1]); HideMenu(buildMenuPanel); ShowMenu(goBackPanel); });

        //GoBack 
        //FIX THIS BY GOING BACK AUTOMATICALLY AFTER YOU PLACE? but what if you dont want to place? right click to go back? + esc?
        goBackButton.onClick.AddListener(delegate { SetBuildMode(false); HideMenu(goBackPanel); ShowMenu(menuPanel); });
    }

    private void LoadContent()
    {
        //Foreach building in prefabs,
        //Read json file for it
        //For now
        //Init all buildings and building settings
        /*
         * foreach(building b in buildingList)
         *  buildings[b.id] = b;
         */
        buildings[0] = Json.Read("/Buildings/tavern.json"); //tavern
        buildings[1] = Json.Read("/Buildings/redhouse.json"); //redhouse

        //Generate Navmesh
        GameManager.Instance.GenerateNavMesh();

        Debug.Log("Content loaded.");
    }

    private void SetCurrentBuilding(Building building)
    {
        placeObject.SetSelectedBuilding(building);
    }

    public bool GetBuildMode()
    {
        if (GameManager.Instance.mode == GameManager.GameState.Build)
            return true;
        else
            return false;
    }

    //Update our NavMesh
    public void SetBuildMode(bool a)
    {
        if (a == true)
        {
            GameManager.Instance.mode = GameManager.GameState.Build;
        }
        else
        {
            GameManager.Instance.GenerateNavMesh();
            GameManager.Instance.mode = GameManager.GameState.Play;
        }
    }

    public void HideMenu(GameObject menu)
    {
        menu.SetActive(false);
    }

    public void ShowMenu(GameObject menu)
    {
        menu.SetActive(true);
    }
}
