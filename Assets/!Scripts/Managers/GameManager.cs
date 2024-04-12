
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
  public SoundManager SoundManager { get; private set; }
  public UIManager UIManager { get; private set; }
  public SaveDataManager SaveDataManager { get; private set; }

  public static int ObstaclesVaulted { get; private set; }
  public static int ObstaclesDucked { get; private set; }

  protected override void Awake()
  {
    base.Awake();

    SoundManager = GetComponentInChildren<SoundManager>();
    UIManager = GetComponentInChildren<UIManager>();
    SaveDataManager = GetComponentInChildren<SaveDataManager>();
  }
  public void IncrementVaultedCounter() => ObstaclesVaulted++;
  public void IncrementDuckedCounter() => ObstaclesDucked++;

  public void GameOver()
  {
    SaveDataManager.Save();
    SceneLoader.LoadScene(2);
  }

  public void TogglePause(bool shouldPause)
  {
    Time.timeScale = shouldPause ? 0 : 1;
  }

  public void Quit()
  {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
  }
}