using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerController playerController;

    public Transform startingPoint;

    public float lerpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerController.gameOver = true;

        StartCoroutine(PlayIntro());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlayIntro()
    {
        Vector3 startPos = playerController.transform.position;
        Vector3 endPos = startingPoint.position;
        float distanceLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;

        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney = distanceCovered / distanceLength;

        playerController.GetComponent<Animator>().SetFloat("SpeedMult_f", 0.5f);

        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            fractionOfJourney = distanceCovered / distanceLength;
            playerController.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);

            yield return null;
        }

        playerController.GetComponent<Animator>().SetFloat("SpeedMult_f", 1.0f);
        playerController.gameOver = false;
    }
}
