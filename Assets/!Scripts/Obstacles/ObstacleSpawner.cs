using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private const float _MAX_SPAWNED_OBSTACLES = 5;
    [SerializeField] private GameObject _jumpObstacle, _rollObstacle;
    private List<GameObject> _currentObstacles = new();

    public void SpawnObstacle(Vector3 spawnPoint)
    {
        GameObject obst;
        if (_currentObstacles.Count < _MAX_SPAWNED_OBSTACLES)
        {
            obst = Random.value < 0.5f ? _jumpObstacle : _rollObstacle;
            _currentObstacles.Add(Instantiate(obst, spawnPoint, Quaternion.identity));
        }
        else
        {
            obst = _currentObstacles.Find(ob =>
            {
                return Random.value > 0.5f ? ob == _jumpObstacle : ob == _rollObstacle;
            });

            obst.SetActive(true);
            obst.transform.position = spawnPoint;
        }
    }

}
