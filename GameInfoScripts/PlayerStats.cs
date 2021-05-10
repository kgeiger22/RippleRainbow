//Saving and importing player data

using UnityEngine;
using System.IO;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    public PlayerData Data;

    public static int[] ScoreThresholds = new int[] { 5000, 15000, 50000 };
    public static int[] ComboThresholds = new int[] { 10, 20, 30 };
    public static int[] LevelThresholds = new int[] { 5, 8, 12 };
    public static int[] TotalBubblesPoppedThresholds = new int[] { 100, 1000, 10000 };
    public static int[] LevelsFullyClearedThresholds = new int[] { 1, 3, 10 };

    private void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        PlayerData _data = LoadData();
        if (_data != null)
        {
            Data = _data;
        }
        else
        {
            Data = new PlayerData();
        }
    }

    private string GetFilePath()
    {
        return Application.persistentDataPath + "/JSONs/" + "PlayerData.json";
    }

    public void SaveData()
    {
        string filePath = GetFilePath();
        FileInfo file = new System.IO.FileInfo(filePath);
        file.Directory.Create(); 
        File.WriteAllText(filePath, JsonUtility.ToJson(Data));
    }

    public PlayerData LoadData()
    {
        string filePath = GetFilePath();

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            return JsonUtility.FromJson<PlayerData>(dataAsJson);
        }
        else
        {
            return null;
        }
    }
}

[System.Serializable]
public class PlayerData
{
    public int BestScore;
    public int BestCombo;
    public int BestLevel;
    public int TotalBubblesPopped;
    public int LevelsFullyCleared;

    public PlayerData()
    {
        BestScore = 0;
        BestCombo = 0;
        BestLevel = 0;
        LevelsFullyCleared = 0;
        TotalBubblesPopped = 0;
    }
}