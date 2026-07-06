using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    public float speed = 8.0f;
    public float startX = -12.0f; // 始点（左の出現位置）
    public float endX = 12.0f;    // 終点（右の消える位置）

    void Update()
    {
        // 現在の位置を取得
        Vector3 pos = transform.position;

        // 常に右へ移動し続ける
        pos.x += Time.deltaTime * speed; 

        // もし右端（終点）を超えたら
        if (pos.x > endX) 
        {
            // 左端（始点）にワープさせる
            pos.x = startX;
        }

        // 計算した新しい位置を反映
        transform.position = pos;
    }
}