using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public GameObject explosion;
    public GameObject scoreToSpawn;
    public GameObject enemyToSpawn;
    public float waitTime = 1f;
    Vector3 killPos;
    Quaternion killRot;
    bool bulletCollision = false; // to awoid hitting multiple spiders with same bullet

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Rabbit" && bulletCollision == false)
        {
            Destroy(collision.transform.gameObject);
            Scoring.score += 5;
            bulletCollision = true;
            killPos = collision.transform.position;
            killRot = collision.transform.rotation;
            StartCoroutine(SpawnEnemyAgain(waitTime));
            Destroy(Instantiate(explosion, collision.transform.position, collision.transform.rotation), waitTime);
            Destroy(Instantiate(scoreToSpawn, collision.transform.position,collision.transform.rotation),waitTime);
        }
    }
    IEnumerator SpawnEnemyAgain(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Instantiate(enemyToSpawn, killPos, killRot);
        bulletCollision = false;
        Destroy(gameObject);
    }
}
