

using System.Collections.Generic;

public class AchievementManager : Singleton<AchievementManager>
{
  public static List<Achievement> _achievementsList = new();

  private void Start()
  {
    _achievementsList.Add(new Achievement(
      "Happy Ducking",
      "Slide under 5 walls in a single run.",
      () => { return SaveDataManager.Instance.CurrentSave.MostObstaclesDucked >= 5; }
    ));

    _achievementsList.Add(new Achievement(
      "Jumpman, Jumpman",
      "Jump over 5 roadblocks in a single run.",
      () => { return SaveDataManager.Instance.CurrentSave.MostObstaclesVaulted >= 5; }
    ));

    _achievementsList.Add(new Achievement(
      "Baby's First Steps",
      "Survive for 500m.",
      () => { return SaveDataManager.Instance.CurrentSave.FarthestDistance >= 100; }
    ));

    _achievementsList.Add(new Achievement(
      "Marathon runner",
      "Survive for 2600m.",
      () => { return SaveDataManager.Instance.CurrentSave.FarthestDistance >= 2600; }
    ));
  }

  public Achievement[] GetUnlockedAchievements()
  {
    List<Achievement> unlocked = new();

    foreach (var achievement in _achievementsList)
    {
      if (achievement.CheckStatus())
        unlocked.Add(achievement);

    }

    return unlocked.ToArray();
  }
}