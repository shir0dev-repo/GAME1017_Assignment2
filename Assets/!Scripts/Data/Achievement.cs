using System;

[Serializable]
public struct Achievement
{
  public string Name;
  public string Description;
  public bool Unlocked;

  private Func<bool> _unlockDelegate;

  public Achievement(string name, string description, Func<bool> unlockDelegate)
  {
    Name = name;
    Description = description;
    _unlockDelegate = unlockDelegate;
    Unlocked = _unlockDelegate();
  }

  public bool CheckStatus()
  {
    Unlocked = _unlockDelegate();
    return Unlocked;
  }
}