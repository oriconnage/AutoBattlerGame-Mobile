using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Singleton<EnemySpawner>
{
    public GameObject objectToSpawn;
    public int numberToSpawn;
    public float proximity;
    private float checkRate;
    private float nextCheck;
    private Transform myTransform;
    private Transform playerTransform;
    private Vector3 spawnPosition;
    public Transform[] waypoints;

    void Start()
    {
        SetInitialReferences();
    }

    void Update()
    {
        CheckDistance();
    }

    void SetInitialReferences()
    {
        myTransform = transform;
        playerTransform = GmRef.Instance.UnitPos;
        checkRate = Random.Range(0.8f, 1.2f);
    }

    void CheckDistance()
    {
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;
            if (Vector3.Distance(myTransform.position, playerTransform.position) < proximity)
            {
                SpawnObjects();
                this.enabled = false;
            }
        }
    }

    void SpawnObjects()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            spawnPosition = myTransform.position + Random.insideUnitSphere *5;
            //Instantiate(objectToSpawn, spawnPosition, myTransform.rotation);
            float x_point = Random.Range(-3, 3);
            float z_point = Random.Range(-7, -3);
            Vector3 inv = new Vector3(0, 0, 0); 
            GameObject go = (GameObject)Instantiate(objectToSpawn, inv, myTransform.rotation);
            go.transform.position = new Vector3(x_point,0.5f, z_point);

            if (waypoints.Length > 0)
            {
                if (go.GetComponent<StatePattern>() != null)
                {
                    go.GetComponent<StatePattern>().waypoints = waypoints;
                }
            }
        }
    }
}

