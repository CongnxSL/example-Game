using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MngEnemy : MonoBehaviour
{
    private Rigidbody2D tankRb;
    private Animator animator;
    public GameObject bullet;
    public List<GameObject> listItem;
    public bool item;

    public int idTank;
    public int typeTank;

    private int speed;
    private int heal;
    private int point;
    private int canMove = 1;
    private MngSensor sensor;

    private int bulletCanShoot;
    private int direction = 0;
    private float timer, timeLoop;

    void Start()
    {
        tankRb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        sensor = gameObject.transform.GetChild(0).GetComponent<MngSensor>();

        SetupTank(typeTank);
        if (item) { animator.SetBool("item", true); }
        InvokeRepeating("Shoot", 1, 1);
        
    }

    void Update()
    {
        if (sensor.sensor) 
        { 
            canMove = 0;
            RotateTank();
        }
        else { canMove = 1; }

        timer += Time.deltaTime;
        if (timer > timeLoop && !MngGame.Instance.item3)
        {
            timer = 0;
            RotateTank();
            timeLoop = Random.Range(0f, 3f);
        }
        if (MngGame.Instance.item5) { DestroyEnemy(); }
        if (MngGame.Instance.item2) { animator.enabled = false; }
        else { animator.enabled = true; }
    }
    private void FixedUpdate()
    {
        if (!MngGame.Instance.item2)
        {
            switch(direction)
            {
                case 0:
                    tankRb.velocity = Vector2.up * speed * canMove;
                    break;
                case 1:
                    tankRb.velocity = Vector2.down * speed * canMove;
                    break;
                case 2:
                    tankRb.velocity = Vector2.left * speed * canMove;
                    break;
                case 3:
                    tankRb.velocity = Vector2.right * speed * canMove;
                    break;
            }
        }
        if (MngGame.Instance.item5) { DestroyEnemy(); }
    }
    void SetupTank(int type)
    {
        switch (type)
        {
            case 0:
                speed = 20;
                bulletCanShoot = 1;
                heal = 1;
                point = 100;
                break;
            case 1:
                speed = 40;
                bulletCanShoot = 1;
                heal = 1;
                point = 200;
                break;
            case 2:
                speed = 20;
                bulletCanShoot = 2;
                heal = 1;
                point = 300;
                break;
            case 3:
                speed = 15;
                bulletCanShoot = 2;
                heal = 4;
                point = 400;
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Item 1") ||
            collision.gameObject.CompareTag("Item 2") ||
            collision.gameObject.CompareTag("Item 3") ||
            collision.gameObject.CompareTag("Item 4") ||
            collision.gameObject.CompareTag("Item 5") ||
            collision.gameObject.CompareTag("Item 6") ||
            collision.gameObject.CompareTag("Item 7"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            
            if (item)
            {
                Vector3 location = new Vector3(Random.Range(-12, 12), Random.Range(-12, 12));
                Instantiate(listItem[Random.Range(0, listItem.Count)], location, Quaternion.identity);
                if(gameObject.CompareTag("Enemy 4")) { animator.SetBool("item", false); }
                item = false;
            }
            heal -= 1;
            if (gameObject.CompareTag("Enemy 4"))
            { 
                animator.SetInteger("heal", heal);
                MngSound.Instance.PlaySound(MngSound.Instance.effectClip[8]);
            }
            if (heal <= 0)
            {
                if (collision.gameObject.GetComponent<MngBullet>().idBullet == 0) 
                { 
                    MngGame.Instance.addPointP1(point);
                    MngGame.Instance.tank1CountEnemy[typeTank] += 1;
                }
                else 
                { 
                    MngGame.Instance.addPointP2(point);
                    MngGame.Instance.tank2CountEnemy[typeTank] += 1;
                }
                DestroyEnemy();
            }
        }
        RotateTank();
    }
    void RotateTank()
    {
        if (!MngGame.Instance.pause && !MngGame.Instance.item2)
        {
            var percentile = Random.Range(0, 101);
            if (percentile < 40)
            {
                if (transform.position.x > 0) { direction = 2; }
                if (transform.position.x < 0) { direction = 3; }
            }
            if (percentile > 60 && transform.position.y > -12) { direction = 1; }
            if (percentile >= 40 && percentile <= 60) { direction = Random.Range(0, 4); }
            switch (direction)
            {
                case 0:
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    break;
                case 1:
                    transform.eulerAngles = new Vector3(0, 0, 180);
                    break;
                case 2:
                    transform.eulerAngles = new Vector3(0, 0, 90);
                    break;
                case 3:
                    transform.eulerAngles = new Vector3(0, 0, 270);
                    break;
            }
        }
    }
    void Shoot()
    {
        var percentile = Random.Range(0, 101);
        if (MngGame.Instance.countBulletShooted[idTank] < bulletCanShoot && percentile < 50 && !MngGame.Instance.item2)
        {
            GameObject bulletPrefabs = Instantiate(bullet, transform.position, transform.rotation);
            bulletPrefabs.GetComponent<MngBullet>().idBullet = idTank;
            MngSound.Instance.PlaySound(MngSound.Instance.effectClip[9]);
            MngGame.Instance.countBulletShooted[idTank] += 1;
        }
    }

    void DestroyEnemy()
    {
        MngGame.Instance.countBulletShooted[idTank] += 5;
        Instantiate(MngGame.Instance.effectAnim[1], transform.position, Quaternion.identity);
        if (!MngGame.Instance.item5)
        {
            switch (point)
            {
                case 100:
                    Instantiate(MngGame.Instance.effectAnim[3], transform.position, Quaternion.identity);
                    break;
                case 200:
                    Instantiate(MngGame.Instance.effectAnim[4], transform.position, Quaternion.identity);
                    break;
                case 300:
                    Instantiate(MngGame.Instance.effectAnim[5], transform.position, Quaternion.identity);
                    break;
                case 400:
                    Instantiate(MngGame.Instance.effectAnim[6], transform.position, Quaternion.identity);
                    break;
            }
        }
        MngSound.Instance.PlaySound(MngSound.Instance.effectClip[2]);
        MngGame.Instance.enemyOnMap -= 1;
        Destroy(gameObject);
    }
}
