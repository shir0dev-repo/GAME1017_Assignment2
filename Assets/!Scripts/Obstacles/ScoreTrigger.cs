using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    public enum ObstType { Vaulted, Ducked }
    [SerializeField] private ObstType _type;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Health health)) return;
        else if (health.IsInvulnerable) return;

        if (_type == ObstType.Vaulted)
            SaveDataManager.Instance.VaultObstacle();
        else
            SaveDataManager.Instance.DuckObstacle();
    }
}
