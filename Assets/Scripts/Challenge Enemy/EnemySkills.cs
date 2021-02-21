using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkills : MonoBehaviour
{
    public int maxNumOfEnemies;
    public GameObject enemyPrefab;
    public Transform[] generatePoint;
    private int currentIndex;
    public Transform[] shootPoints;
    public GameObject bulletPrefab;
    public float atk = 1;
    public float attackDistance = 8;

    public void UseSkill()
    {
        if (!GenerateNewEnemy())
            Shooting();
    }

    private bool GenerateNewEnemy()
    {
        if (currentIndex == maxNumOfEnemies) return false;
        GameObject enemyGO = Instantiate(enemyPrefab, generatePoint[currentIndex++].position, Quaternion.identity);
        return true;
    }

    private void Shooting()
    {
        if(Random.Range(0,2)==0)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject bulletGO = Instantiate(bulletPrefab, shootPoints[i].position, Quaternion.identity);
                bulletGO.GetComponent<BigBullet>().targetPos = shootPoints[i].position + (shootPoints[i].position - transform.position).normalized * attackDistance;
            }
        }
        else
        {
            for (int i = 4; i < shootPoints.Length; i++)
            {
                GameObject bulletGO = Instantiate(bulletPrefab, shootPoints[i].position, Quaternion.identity);
                bulletGO.GetComponent<BigBullet>().targetPos = shootPoints[i].position + (shootPoints[i].position - transform.position).normalized * attackDistance;
            }
        }
           

    }
}
