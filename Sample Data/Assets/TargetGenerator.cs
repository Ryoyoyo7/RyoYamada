using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGenerator : MonoBehaviour
{
    private float positionX;
    private float positionY;
    private float positionZ;
    private float time;

    public float TargetSpawnDistance = 6.0f;
    public GameObject target;

    private void TargetGenerate() //Targetの生成
    {
        positionX = Random.Range(-8.0f, 8.0f);
        positionY = Random.Range(0.5f, 2.0f);
        positionZ = Random.Range(0.0f, 8.0f);

        Vector3 targetPosition = new Vector3(positionX, positionY, positionZ);
        GameObject randomTarget = Instantiate(target, targetPosition, Quaternion.identity);
    }

    public void StartTargetGenerate()
    {
        time = 0.0f;
        TargetGenerate();
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

        if (time >= TargetSpawnDistance)
        {
            time = 0.0f;
            TargetGenerate();
        }
    }
}
