
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
  [SerializeField] private GameObject _settingsPanel;
  [SerializeField] private GameObject _backBtn;

  [Header("TitleScene")]
  [SerializeField] private GameObject _titlePanel;

  [Header("PlayScene")]
  [SerializeField] private GameObject _playPanel;
  [SerializeField] private TextMeshProUGUI _distanceUGUI;
  [SerializeField] private GameObject[] _lives;

  [Header("GameOverScene")]
  [SerializeField] private GameObject _gameOverPanel;

  [Space]
  [SerializeField] private TextMeshProUGUI _scoreUGUI;
  [SerializeField] private TextMeshProUGUI _highScoreUGUI;
  [SerializeField] private TextMeshProUGUI _totalScoreUGUI;

  [Space]
  [SerializeField] private TextMeshProUGUI _vaultUGUI;
  [SerializeField] private TextMeshProUGUI _mostVaultUGUI;
  [SerializeField] private TextMeshProUGUI _totalVaultUGUI;

  [Space]
  [SerializeField] private TextMeshProUGUI _duckUGUI;
  [SerializeField] private TextMeshProUGUI _mostDuckUGUI;
  [SerializeField] private TextMeshProUGUI _totalDuckUGUI;

  protected override void Awake()
  {
    base.Awake();

    ScrollManager.OnLevelScroll += UpdateDistanceText;
    SceneLoader.OnSceneLoaded += SetSceneUI;
  }

  private void SetSceneUI(int sceneIndex)
  {
    _titlePanel.SetActive(sceneIndex == 0);
    _playPanel.SetActive(sceneIndex == 1);
    _gameOverPanel.SetActive(sceneIndex == 2);

    ResetUIState();
  }

  private void ResetUIState()
  {
    if (PlayerController.Instance != null)
      UpdateLives(PlayerController.Instance.PlayerHealth.CurrentHealth);

    DisplayStats();
  }

  public void ToggleSettingsMenu(bool settingsActive)
  {
    if (settingsActive)
    {
      _settingsPanel.SetActive(true);
      _backBtn.SetActive(false);
      SetSceneUI(-1);
    }
    else
    {
      _settingsPanel.SetActive(false);
      _backBtn.SetActive(true);
      SetSceneUI(SceneManager.GetActiveScene().buildIndex);
    }
  }

  private void DisplayStats()
  {
    if (SaveDataManager.Instance == null) return;
    SaveDataManager sdm = SaveDataManager.Instance;

    _scoreUGUI.text = sdm.FinalScore.ToString() + "m";
    _highScoreUGUI.text = sdm.CurrentSave.FarthestDistance.ToString("F0") + 'm';
    _totalScoreUGUI.text = sdm.CurrentSave.TotalDistance.ToString("F0") + 'm';

    _vaultUGUI.text = sdm.ObstaclesVaulted.ToString();
    _mostVaultUGUI.text = sdm.CurrentSave.MostObstaclesVaulted.ToString();
    _totalVaultUGUI.text = sdm.CurrentSave.TotalObstaclesVaulted.ToString();

    _duckUGUI.text = sdm.ObstaclesDucked.ToString();
    _mostDuckUGUI.text = sdm.CurrentSave.MostObstaclesDucked.ToString();
    _totalDuckUGUI.text = sdm.CurrentSave.TotalObstaclesDucked.ToString();
  }

  public void UpdateLives(int livesRemaining)
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