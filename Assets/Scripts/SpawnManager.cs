using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    private Vector3 spawnLocation = new Vector3(17, 0, 0);
    public float startDelay = 2;
    public float repeatRate = 2;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnObstacle()
    {
        int objectIndex = Random.Range(0, obstaclePrefabs.Length);

        if (playerController.gameOver == false)
        {
            Instantiate(obstaclePrefabs[objectIndex], spawnLocation, obstaclePrefabs[objectIndex].transform.rotation);
        }
    }
}
