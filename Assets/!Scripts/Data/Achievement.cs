using System;
using UnityEngine;

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

  public readonly string SerializeToJSON()
  {
    string json = JsonUtility.ToJson(this);
    json = json.Replace("true", "Unlocked");

    return json;
  }

  public bool CheckStatus()
  {
    Unlocked = _unlockDelegate();
    return Unlocked;
  }
}