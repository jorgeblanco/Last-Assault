using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject explosionFx;
    [SerializeField] private int numHits = 1;
    private Scoreboard _scoreboard;

    private void Start()
    {
        _scoreboard = FindObjectOfType<Scoreboard>();
    }

    private void OnParticleCollision(GameObject other)
    {
        // Assume all particle collisions are bullets
        _scoreboard.ScoreHit();
        numHits--;
        if (numHits <= 0)
        {
            Instantiate(explosionFx, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
