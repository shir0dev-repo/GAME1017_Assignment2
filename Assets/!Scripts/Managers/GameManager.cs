
public class GameManager : PersistentSingleton<GameManager>
{
  public SoundManager SoundManager { get; private set; }
  public UIManager UIManager { get; private set; }

  protected override void Awake()
  {
    base.Awake();

    SoundManager = GetComponentInChildren<SoundManager>();
    UIManager = GetComponentInChildren<UIManager>();
  }
}