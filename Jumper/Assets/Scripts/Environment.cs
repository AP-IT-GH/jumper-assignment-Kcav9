using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Environment : MonoBehaviour
{

    /// NEWWWW
    public Obstacle policeCarPrefab;

    //private Cube cube;
    private TextMeshPro scoreBoard;
    private GameObject enemies;
    private Taxi taxi;
    public float SpawnTime;
    public float delayInBetween;


    void Start()
    {
        InvokeRepeating("SpawnEnemies", SpawnTime, delayInBetween);
    }

    public void OnEnable()
    {
        enemies = transform.Find("Enemies").gameObject;
        scoreBoard = transform.GetComponentInChildren<TextMeshPro>();
        taxi = transform.GetComponentInChildren<Taxi>();        
    }

    private void FixedUpdate()
    {
        //scoreBoard.text = taxi.GetCumulativeReward().ToString("f2");
    }
 


    public void ClearEnvironment()
    {
        enemies = transform.Find("Enemies").gameObject;

        foreach (Transform enemy in enemies.transform)
        {
            GameObject.Destroy(enemy.gameObject);
        }
    }


    public void SpawnEnemies()
    {
        GameObject newEnemy = Instantiate(policeCarPrefab.gameObject);

        newEnemy.transform.SetParent(enemies.transform);
        newEnemy.transform.localPosition = enemies.transform.localPosition;
        newEnemy.transform.localRotation = enemies.transform.localRotation;

    }
}
