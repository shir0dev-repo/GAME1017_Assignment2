using UnityEngine;
using System;
using System.Timers;
using System.Collections;

public class ScrollManager : Singleton<ScrollManager>
{
  [SerializeField] private Camera _mainCam;
  [SerializeField] private float _startingScrollSpeed = 1f, _maxScrollSpeed = 7.5f;
  public bool ShouldScroll = true;
  public static Action<int> OnLevelScroll;

  public static float DistanceTravelled { get; private set; }
  public float ScrollSpeed { get; private set; }

  private float _scrollAcceleration = 0.025f;

  protected override void Awake()
  {
    base.Awake();
    ScrollSpeed = _startingScrollSpeed;
    StartCoroutine(SpeedRampCoroutine());
  }

  private void Update()
  {
    if (!ShouldScroll) return;

    transform.position += Vector3.right * ScrollSpeed * Time.deltaTime;
    if (SaveDataManager.Instance != null)
      SaveDataManager.Instance.UpdateDistanceTravelled(Mathf.RoundToInt(transform.position.x));
    OnLevelScroll?.Invoke(Mathf.RoundToInt(transform.position.x));
  }

  private void OnDestroy() => StopAllCoroutines();

  private IEnumerator SpeedRampCoroutine()
  {
    while (true)
    {
      if (ShouldScroll == false) yield break;

      ScrollSpeed = Mathf.Clamp(ScrollSpeed + _scrollAcceleration * Time.deltaTime / 5f, _startingScrollSpeed, _maxScrollSpeed);
      yield return new WaitForEndOfFrame();
    }
  }
}