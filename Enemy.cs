using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 12; //todo change the scorePerHit for each different enemy
    [SerializeField] int hits = 3; //same as above

    ScoreBoard scoreBoard;

    // Use this for initialization
    
    void Start()
        {
            AddBoxCollider();
            scoreBoard = FindObjectOfType<ScoreBoard>();
        }

    private void AddBoxCollider()
        {
            Collider boxCollider = gameObject.AddComponent<BoxCollider>();
            boxCollider.isTrigger = false;
        }
    

    void OnParticleCollision(GameObject other)
    {
        ProcessHit(); //same as hits = hits - 1;
        if (hits <= 0)
        {
            KillEnemy();
            scoreBoard.ScoreHit(scorePerHit);
        }
       
    }

    private void ProcessHit()
    {
        hits--;
    }

    private void KillEnemy()
    {
        deathFX.SetActive(true);
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        deathFX.SetActive(false);
        fx.transform.parent = parent;
        Destroy(gameObject, (float)0.25);
    }
}
