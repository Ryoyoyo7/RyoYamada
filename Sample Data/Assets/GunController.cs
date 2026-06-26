using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GunController : MonoBehaviour
{
    private Rigidbody rigidbody;
    public GameObject bullet;
    public float speed = 0.01f;
    public float jumpForce = 350.0f;
    Counter counter;

    private void Move() //Gunの移動に関するメソッド
    {
        if (Input.GetKey(KeyCode.W)) //Wキーを押している間
        {
            if (this.transform.position.z <= -5)
            {
                transform.Translate(0, 0, this.speed);
                Debug.Log("front");
            }
        }
        if (Input.GetKey(KeyCode.S)) //Sキーを押している間
        {
            if (this.transform.position.z >= -9)
            {
                transform.Translate(0, 0, -this.speed);
                Debug.Log("back");
            }
        }
        if (Input.GetKey(KeyCode.A)) //Aキーを押している間
        {
            if (this.transform.position.x >= -9.5)
            {
                transform.Translate(-this.speed, 0, 0);
                Debug.Log("left");
            }
        }
        if (Input.GetKey(KeyCode.D)) //Dキーを押している間
        {
            if (this.transform.position.x <= 9.5)
            {
                transform.Translate(this.speed, 0, 0);
                Debug.Log("right");
            }
        }
    }

    private void Jump() //Gunのジャンプに関するメソッド
    {      
        if(Input.GetKeyDown(KeyCode.Space) && this.rigidbody.velocity.y == 0) //Spaceキーを押したとき
        {
            this.rigidbody.AddForce(transform.up * this.jumpForce);
            Debug.Log("jump");
        }     
    }

    private void Shoot() //GunのBulletを生成するメソッド
    {
        if(Input.GetMouseButtonDown(0)) //マウスの左クリックを押したとき
        {
            Vector3 bulletPosition = transform.position + new Vector3(0, 0, 0.9f);
            GameObject newBullet = Instantiate(this.bullet, bulletPosition, Quaternion.identity); //BulletをnewBulletという名前で生成
            BulletController bulletController = newBullet.GetComponent<BulletController>(); //生成されたnewBullet内のBulletControllerを取得
            bulletController.SetCounter(counter); //Startで取得したcounterを、BulletController.cs内のSetCounterという関数に代入、実行
        }
    }

    void Start()
    {
        this.rigidbody = GetComponent<Rigidbody>();
        this.counter = GameObject.Find("GameDirector").GetComponent<Counter>(); //GameDirectorという名前のオブジェクトを探して、その中のCounterを取得
        Debug.Log("Start");
    }

    void Update()
    {
        Move();
        Jump();
        Shoot();     
    }
}

