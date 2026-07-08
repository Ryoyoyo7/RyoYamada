using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusTargetGenerator : MonoBehaviour
{
    public GameObject bonusTarget;
    public float bonusSpawnTime = 8.0f;

    private float time = 0.0f;

    private void GenerateBonusTarget()
    {
        float positionX = Random.Range(-6.0f, 6.0f);
        float positionY = Random.Range(1.0f, 2.5f);
        float positionZ = Random.Range(1.0f, 7.0f);

        Vector3 bonusPosition = new Vector3(positionX, positionY, positionZ);
        Instantiate(bonusTarget, bonusPosition, Quaternion.identity);
    }

    void Start()
    {
        time = 0.0f;
    }

    void Update()
    {
        if (!GameTimer.isPlaying || GameTimer.isGameOver)
        {
            return;
        }

        time += Time.deltaTime;

        if (time >= bonusSpawnTime)
        {
            time = 0.0f;
            GenerateBonusTarget();
        }
    }
}