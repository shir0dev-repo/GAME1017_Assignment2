using System;
using System.IO;
using UnityEngine;

public class SaveDataManager : Singleton<SaveDataManager>
{
    private string FilePath => Path.Combine(Application.persistentDataPath, "player.dat");

    private SaveData _currentSave;
    public SaveData CurrentSave => _currentSave;

    private int _score;
    private int _obstaclesVaulted, _obstaclesDucked;

    protected override void Awake()
    {
        base.Awake();
        _currentSave = Load();
    }

    public void Save()
    {
        _currentSave = File.Exists(FilePath) ?
        new SaveData(JsonUtility.FromJson<SaveData>(File.ReadAllText(FilePath)), _score, _obstaclesVaulted, _obstaclesDucked, null) :
        new SaveData(_score, _obstaclesVaulted, _obstaclesDucked, null);

        _currentSave.UnlockedAchievements = AchievementManager.Instance.GetUnlockedAchievements();

        string saveDataJSON = JsonUtility.ToJson(_currentSave, true);
        File.WriteAllText(FilePath, saveDataJSON);
        Debug.Log("Save file was written to: " + FilePath + " successfully.");
    }

    public SaveData Load()
    {
        try
        {
            return JsonUtility.FromJson<SaveData>(File.ReadAllText(FilePath));
        }
        catch (Exception e)
        {
            Debug.LogWarningFormat("Could not load save data from " + FilePath, e);
            return new SaveData();
        }
    }

    public void UpdateDistanceTravelled(int distanceTravelled) => _score = distanceTravelled;
    public void VaultObstacle() => _obstaclesVaulted++;
    public void DuckObstacle() => _obstaclesDucked++;

    protected override void OnApplicationQuit()
    {
        Save();

        base.OnApplicationQuit();
    }
}