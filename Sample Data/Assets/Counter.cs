using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Text count;
    public int hitCount = 0;

    public int shotCount = 0;
    public int targetHitCount = 0;
    public int bonusHitCount = 0;
    public int obstacleHitCount = 0;

    void Update()
    {
        this.count.text = hitCount.ToString() + "Hit";
    }

    public void ResetRecord()
    {
        hitCount = 0;
        shotCount = 0;
        targetHitCount = 0;
        bonusHitCount = 0;
        obstacleHitCount = 0;
    }
}