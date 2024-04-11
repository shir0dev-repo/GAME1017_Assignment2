using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private const float _MAX_SPAWNED_OBSTACLES = 5;
    [SerializeField] private GameObject _jumpObstacle, _rollObstacle;
    private List<GameObject> _currentObstacles = new();

    private bool _spawnWithTrigger = false;

    private void Start()
    {
        StartCoroutine(SpawnObstacleCoroutine());
        PlayerController.Instance.PlayerHealth.OnInvulnerabilityChanged += ToggleObstacleTriggers;
    }

    public void SpawnObstacle(Vector3 spawnPoint)
    {
        GameObject obst;
        if (_currentObstacles.Count < _MAX_SPAWNED_OBSTACLES)
        {
            obst = Random.value < 0.5f ? _jumpObstacle : _rollObstacle;
            obst.GetComponent<Collider2D>().isTrigger = _spawnWithTrigger;
            _currentObstacles.Add(Instantiate(obst, spawnPoint, Quaternion.identity));
        }
        else
        {
            obst = _currentObstacles[Random.Range(0, _currentObstacles.Count)];
            obst.GetComponent<Collider2D>().isTrigger = _spawnWithTrigger;
            obst.SetActive(true);
            obst.transform.position = spawnPoint;
        }


    }

    private void ToggleObstacleTriggers(bool isInvulnerable)
    {
        _spawnWithTrigger = isInvulnerable;
        foreach (GameObject obst in _currentObstacles)
        {
            obst.GetComponent<Collider2D>().isTrigger = _spawnWithTrigger;
        }
    }

    private void CheckForOffScreenObstacles()
    {
        foreach (GameObject obstacle in _currentObstacles)
        {
            if (!obstacle.GetComponentInChildren<SpriteRenderer>().isVisible &&
                obstacle.transform.position.x < ScrollManager.Instance.transform.position.x)
                obstacle.SetActive(false);
        }
    }

    private IEnumerator SpawnObstacleCoroutine()
    {
        float timeElapsed = 0;
        float spawnInterval;
        float cameraXExtents = Camera.main.orthographicSize * Screen.width / Screen.height;
        while (true)
        {
            timeElapsed += Time.deltaTime;
            //v = dt, t = d/v
            spawnInterval = 2f * cameraXExtents / ScrollManager.Instance.ScrollSpeed;

            if (timeElapsed > spawnInterval)
            {
                timeElapsed = 0;

                Vector3 spawnPos = new(Camera.main.transform.position.x + 8f, -5f, 0);
                SpawnObstacle(spawnPos);
            }

            CheckForOffScreenObstacles();

            yield return new WaitForEndOfFrame();
        }
    }
}
