
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
}