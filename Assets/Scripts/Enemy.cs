using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject explosionFx;
    private Scoreboard _scoreboard;

    private void Start()
    {
        _scoreboard = FindObjectOfType<Scoreboard>();
    }

    private void OnParticleCollision(GameObject other)
    {
        // Assume all particle collisions are bullets
        Instantiate(explosionFx, transform.position, Quaternion.identity);
        _scoreboard.ScoreHit();
        Destroy(gameObject);
    }
}
