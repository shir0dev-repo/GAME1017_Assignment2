using UnityEngine;
using System;
using System.Timers;
using System.Collections;

public class ScrollManager : Singleton<ScrollManager>
{
  [SerializeField] private Camera _mainCam;
  public static Action<float> OnLevelScroll;
  public float ScrollSpeed { get; private set; }

  private float _startingScrollSpeed = 1f, _maxScrollSpeed = 7.5f;
  private float _scrollAcceleration = 0.025f;
  protected override void Awake()
  {
    base.Awake();
    ScrollSpeed = _startingScrollSpeed;
    StartCoroutine(SpeedRampCoroutine());
  }

  private void Update()
  {
    transform.position += Vector3.right * ScrollSpeed * Time.deltaTime;
    OnLevelScroll?.Invoke(ScrollSpeed);
  }

  private void OnDestroy() => StopAllCoroutines();

  private IEnumerator SpeedRampCoroutine()
  {
    while (true)
    {
      ScrollSpeed = Mathf.Clamp(ScrollSpeed + (_scrollAcceleration * Time.deltaTime) / 5f, _startingScrollSpeed, _maxScrollSpeed);
      yield return new WaitForEndOfFrame();
    }
  }
}