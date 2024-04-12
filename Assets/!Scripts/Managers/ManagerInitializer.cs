using UnityEngine;

public class ManagerInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _managerPrefab;

    private void Awake()
    {
        if (GameManager.Instance == null)
            Instantiate(_managerPrefab);

        Destroy(gameObject);
    }
}
