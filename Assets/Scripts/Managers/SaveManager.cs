using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveObject
{
    public string gameName;
    public string timePlayed;
    public string timeJsonMade;
    public string score;
}

public static class SaveManager
{
    public static void Save()
    {
        SaveObject saveObject = new SaveObject();
        string json = string.Empty;

        saveObject.gameName = Application.productName;
        saveObject.timePlayed = ((int)Math.Round(GameManager.Instance.timePlayed, 0)).ToString();

        long unixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        saveObject.timeJsonMade = unixTime.ToString();
        saveObject.score = GameManager.Instance.points.ToString();

        json = JsonUtility.ToJson(saveObject);
        string path = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "save.json";
        File.WriteAllText(path, json);
    }
}
