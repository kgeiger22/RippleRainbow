using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class LevelHelper
{
    private static string GetFilePathForLevel(string name)
    {
        return Application.dataPath + "/JSONs/" + name + ".json";
    }

    private static string GetJSONFromLevelData(LevelData _level)
    {
        return JsonUtility.ToJson(_level);
    }

    public static void SaveLevel(Level _level)
    {
        string filePath = GetFilePathForLevel(_level.gameObject.name);
        File.WriteAllText(filePath, GetJSONFromLevelData(_level.data));
    }

    public static LevelData LoadLevel(string name)
    {
        string filePath = GetFilePathForLevel(name);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            return JsonUtility.FromJson<LevelData>(dataAsJson);
        }
        else
        {
            Debug.Log("File " + filePath + " failed to load");
            return null;
        }
    }
}
