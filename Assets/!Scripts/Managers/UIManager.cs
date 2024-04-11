
using System;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
  [Header("PlayScene")]
  [SerializeField] private TextMeshProUGUI _distanceUGUI;
  [SerializeField] private GameObject[] _lives;

  protected override void Awake()
  {
    base.Awake();

    ScrollManager.OnLevelScroll += UpdateDistanceText;

  }

  private void Start()
  {
    PlayerController.Instance.PlayerHealth.OnDamageTaken += UpdateLives;
  }

  private void UpdateLives(int livesRemaining)
  {
    for (int i = 0; i < _lives.Length; i++)
    {
      _lives[i].SetActive(i < livesRemaining);
    }
  }

  public void UpdateDistanceText(int value)
  {
    _distanceUGUI.text = "DISTANCE: " + value + "m";
  }
}