using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GunController : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject bullet;
    public float speed = 5.0f;
    public float jumpForce = 350.0f;
    Counter counter;
    SoundManager soundManager;

    private void Move()
    {
        float move = speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
        {
            if (this.transform.position.z <= -5)
            {
                transform.Translate(0, 0, move);
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (this.transform.position.z >= -9)
            {
                transform.Translate(0, 0, -move);
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (this.transform.position.x >= -9.5)
            {
                transform.Translate(-move, 0, 0);
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (this.transform.position.x <= 9.5)
            {
                transform.Translate(move, 0, 0);
            }
        }
    }

    private void Jump() //Gunのジャンプに関するメソッド
    {      
        if(Input.GetKeyDown(KeyCode.Space) && this.rb.velocity.y == 0) //Spaceキーを押したとき
        {
            this.rb.AddForce(transform.up * this.jumpForce);
            Debug.Log("jump");
        }     
    }

    private void Shoot() //GunのBulletを生成するメソッド
    {
        if (!GameTimer.isPlaying || GameTimer.isGameOver)
        {
            return;
        }

        if(Input.GetMouseButtonDown(0)) //マウスの左クリックを押したとき
        {
            Vector3 bulletPosition = transform.position + new Vector3(0, 0, 0.9f);
            GameObject newBullet = Instantiate(this.bullet, bulletPosition, Quaternion.identity); //BulletをnewBulletという名前で生成
            BulletController bulletController = newBullet.GetComponent<BulletController>(); //生成されたnewBullet内のBulletControllerを取得
            bulletController.SetCounter(counter); //Startで取得したcounterを、BulletController.cs内のSetCounterという関数に代入、実行

            counter.shotCount++;

            if (soundManager != null)
            {
                soundManager.PlayShoot();
            }
        }
    }

    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        this.counter = GameObject.Find("GameDirector").GetComponent<Counter>(); //GameDirectorという名前のオブジェクトを探して、その中のCounterを取得
        Debug.Log("Start");
        soundManager = GameObject.Find("GameDirector").GetComponent<SoundManager>();
    }

    void Update()
    {
        Move();
        Jump();
        Shoot();     
    }
}

