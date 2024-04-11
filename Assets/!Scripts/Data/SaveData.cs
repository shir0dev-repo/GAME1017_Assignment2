using System.Linq;
using UnityEngine;

[System.Serializable]
public struct SaveData
{
  public static readonly SaveData Empty = new(0, 0, 0, null);
  // persistent through runs
  public float TotalDistance;
  public float TotalObstaclesVaulted;
  public float TotalObstaclesDucked;

  public Achievement[] UnlockedAchievements;

  // single run high scores
  public float FarthestDistance;
  public int MostObstaclesVaulted;
  public int MostObstaclesDucked;

  public SaveData(float distance, int vaulted, int ducked, Achievement[] achievements)
  {
    TotalDistance = FarthestDistance = distance;
    TotalObstaclesVaulted = MostObstaclesVaulted = vaulted;
    TotalObstaclesDucked = MostObstaclesDucked = ducked;
    UnlockedAchievements = achievements;
  }

  public SaveData(SaveData previous, float distance, int vaulted, int ducked, Achievement[] achievements = null)
  {
    TotalDistance = previous.TotalDistance + distance;
    FarthestDistance = Mathf.Max(previous.FarthestDistance, distance);

    TotalObstaclesVaulted = previous.TotalObstaclesVaulted + vaulted;
    MostObstaclesVaulted = Mathf.Max(previous.MostObstaclesVaulted, vaulted);

    TotalObstaclesDucked = previous.TotalObstaclesDucked + ducked;
    MostObstaclesDucked = Mathf.Max(previous.MostObstaclesDucked, ducked);

    UnlockedAchievements = achievements;
  }
}