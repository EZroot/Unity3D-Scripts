using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using LitJson;

//Read/Write dependant on the tag of the building?
//We can use the tag to identify text files
//Tavern.json
//Home.json
//etc

public static class Json
{
    //Default text
    private static string writableText;

    public static void Write(string fileName, Building building)
    {
        writableText = "";

        string text = JsonUtility.ToJson(building);
        File.WriteAllText(Application.streamingAssetsPath + fileName, text);
    }

    public static void WriteAll(string fileName, Building[] buildings)
    {
        writableText = "";

        foreach (Building building in buildings)
        {
            writableText += JsonUtility.ToJson(building);
        }
        File.WriteAllText(Application.streamingAssetsPath + fileName, writableText);
    }

    public static Building Read(string fileName)
    {
        return JsonUtility.FromJson<Building>(File.ReadAllText(Application.streamingAssetsPath + fileName));
    }

    public static Resource ReadResource(string fileName)
    {
        return JsonUtility.FromJson<Resource>(File.ReadAllText(Application.streamingAssetsPath + fileName));
    }

    //gotta fix
    public static List<Building> ReadAll(string fileName, List<Building> buildingList)
    {
        List<Building> loadList = new List<Building>();

        foreach(Building building in buildingList)
            loadList.Add(JsonUtility.FromJson<Building>(Application.streamingAssetsPath + fileName));
        return loadList;
    }
}
