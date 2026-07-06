using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 25.0f;
    Counter counter;

    private bool isHit = false; // すでに何かに当たったかどうか

    public void SetCounter(Counter c)
    {
        this.counter = c;
    }

    private void OnCollisionEnter(Collision collision) //Bulletが何かと衝突したとき
    {
        if (isHit)
        {
            return;
        }

        isHit = true;

        if (collision.gameObject.tag == "Target") //Targetに当たった場合
        {
            if (counter != null)
            {
                counter.hitCount++;
                Debug.Log(counter.hitCount + " Hit");
            }
        }
        else if (collision.gameObject.tag == "Obstacle") //Obstacleに当たった場合
        {
            if (counter != null)
            {
                counter.hitCount--;

                if (counter.hitCount < 0)
                {
                    counter.hitCount = 0;
                }

                Debug.Log("Obstacle Hit : -1");
            }
        }

        Invoke("destroyBullet", 1); //一秒後にdestroyBullet()を呼び出す
    }

    private void destroyBullet()
    {
        Destroy(this.gameObject); //Objectを消す       
    }

    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        this.rb.velocity = new Vector3(0, 0, this.speed); //初速度の設定
    }

    void Update()
    {

    }
}