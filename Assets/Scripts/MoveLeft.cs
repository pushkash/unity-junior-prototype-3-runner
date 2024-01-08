using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 10;
    private PlayerController playerController;
    private float leftBound = -15;


    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.gameOver == false)
        {
            if (!playerController.doubleSpeed)
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            } else
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed * 2);
            }
        }

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }

        
    }
}
