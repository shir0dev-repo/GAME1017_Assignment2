
using System;

public class ScrollManager : Singleton<ScrollManager>
{
  public readonly float ScrollSpeedRO = 5f;

  public static Action<float> OnLevelScroll;

  private void Update()
  {
    OnLevelScroll?.Invoke(ScrollSpeedRO);
  }
}