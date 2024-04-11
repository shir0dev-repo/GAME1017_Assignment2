using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnEnable()
    {
        ScrollManager.OnLevelScroll += ScrollObject;
    }

    private void OnBecameInvisible()
    {
        ScrollManager.OnLevelScroll -= ScrollObject;
        gameObject.SetActive(false);
    }

    private void ScrollObject(float scrollSpeed)
    {
        transform.position += scrollSpeed * Time.deltaTime * Vector3.left;
    }
}
