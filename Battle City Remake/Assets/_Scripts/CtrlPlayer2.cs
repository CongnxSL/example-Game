using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlPlayer2 : MonoBehaviour
{
    public int idTank = 1;
    public GameObject bullet;
    private Rigidbody2D tankRb;
    private Animator animator;

    private float speed = 20;
    private int canMove = 1;
    private MngSensor sensor;
    private float PosX;
    private float PosY;
    private int direction;
    private bool isHorizontalTank;
    private float timeDelayShoot;


    void Start()
    {
        tankRb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        sensor = gameObject.transform.GetChild(0).GetComponent<MngSensor>();
        animator.SetInteger("lv", MngGame.Instance.tank2lvBullet);
    }


    void Update()
    {
        PosX = (Mathf.Round(transform.position.x));
        PosY = (Mathf.Round(transform.position.y));
        Direction(); 
        if (sensor.sensor)
        {
            canMove = 0;
        }
        else { canMove = 1; }

        if (Input.GetKey(KeyCode.Space))
        {
            Shoot();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            timeDelayShoot = 0;
        }
        if (MngGame.Instance.tank2Item1) { gameObject.transform.GetChild(1).gameObject.SetActive(true); }
        else { gameObject.transform.GetChild(1).gameObject.SetActive(false); }

        if ((Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.DownArrow) ||
            Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.RightArrow)) && MngGame.Instance.isPlay)
        {
            MngSound.Instance.OnPlayerMoveSound();
        }
        else
        {
            MngSound.Instance.OffPlayerMoveSound();
        }
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && !MngGame.Instance.tank2Item1)
        {
            DestroyPlayer();
        }
        if (collision.gameObject.CompareTag("Item 1"))
        {
            MngGame.Instance.Item1(idTank, gameObject.transform.GetChild(1).gameObject);
            eatItem(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Item 2"))
        {
            MngGame.Instance.Item2();
            eatItem(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Item 3"))
        {
            MngMap.Instance.ResetMap();
            MngMap.Instance.Item3();
            eatItem(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Item 4"))
        {
            UpBullet(1);
            eatItem(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Item 5"))
        {
            MngGame.Instance.Item5();
            eatItem(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Item 6"))
        {
            eatItem(collision.gameObject);
            MngGame.Instance.UpLife(idTank);
        }
        if (collision.gameObject.CompareTag("Item 7"))
        {
            UpBullet(2);
            eatItem(collision.gameObject);
        }
    }
    void Direction()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            direction = 1;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            direction = 2;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            direction = 3;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = 4;
        }

        if (Input.GetKeyUp(KeyCode.A) ||
            Input.GetKeyUp(KeyCode.D) ||
            Input.GetKeyUp(KeyCode.W) ||
            Input.GetKeyUp(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.D))
            {
                direction = 3;
            }
            if (Input.GetKey(KeyCode.A))
            {
                direction = 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction = 2;
            }
            if (Input.GetKey(KeyCode.W))
            {
                direction = 4;
            }
            if (!Input.GetKey(KeyCode.W) &&
                !Input.GetKey(KeyCode.S) &&
                !Input.GetKey(KeyCode.A) &&
                !Input.GetKey(KeyCode.D))
            {
                direction = 0;
            }
        }
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.A) && direction == 1)
        {
            if (!isHorizontalTank)
            {
                transform.position = new Vector2(transform.position.x, PosY);
            }
            tankRb.velocity = Vector2.left * speed * canMove;
            transform.eulerAngles = new Vector3(0, 0, 90);
            isHorizontalTank = true;

        }
        if (Input.GetKey(KeyCode.D) && direction == 3)
        {
            if (!isHorizontalTank)
            {
                transform.position = new Vector2(transform.position.x, PosY);
            }
            tankRb.velocity = Vector2.right * speed * canMove;
            transform.eulerAngles = new Vector3(0, 0, 270);
            isHorizontalTank = true;
        }
        if (Input.GetKey(KeyCode.W) && direction == 4)
        {
            if (isHorizontalTank)
            {
                transform.position = new Vector2(PosX, transform.position.y);
            }
            tankRb.velocity = Vector2.up * speed * canMove;
            transform.eulerAngles = new Vector3(0, 0, 0);
            isHorizontalTank = false;
        }
        if (Input.GetKey(KeyCode.S) && direction == 2)
        {
            if (isHorizontalTank)
            {
                transform.position = new Vector2(PosX, transform.position.y);
            }
            tankRb.velocity = Vector2.down * speed * canMove;
            transform.eulerAngles = new Vector3(0, 0, 180);
            isHorizontalTank = false;
        }
        if (direction != 0 && MngGame.Instance.isPlay)
        {
            animator.SetBool("isMove", true);
        }
        else
        {
            animator.SetBool("isMove", false);
        }
    }

    private void Shoot()
    {
        if (MngGame.Instance.countBulletShooted[idTank] < MngGame.Instance.tank2BulletCanShoot && 
            timeDelayShoot == 0 && 
            MngGame.Instance.isPlay)
        {
            GameObject tmp = Instantiate(bullet, transform.position, transform.rotation);
            tmp.GetComponent<MngBullet>().idBullet = idTank;
            tmp.GetComponent<MngBullet>().levelBullet = MngGame.Instance.tank2lvBullet;
            MngSound.Instance.PlaySound(MngSound.Instance.effectClip[9]);
            MngGame.Instance.countBulletShooted[idTank] += 1;
        }
        timeDelayShoot += Time.deltaTime;
        if (timeDelayShoot > 0.2f) timeDelayShoot = 0;
    }
    private void UpBullet(int lv)
    {
        MngGame.Instance.UpLvBullet(idTank, lv);
        direction = 0;
        animator.SetBool("isMove", false);
        animator.SetInteger("lv", MngGame.Instance.tank1lvBullet);
        if (Input.GetKey(KeyCode.A)) { direction = 1; }
        else if (Input.GetKey(KeyCode.D)) { direction = 3; }
        else if (Input.GetKey(KeyCode.W)) { direction = 4; }
        else if (Input.GetKey(KeyCode.S)) { direction = 2; }
    }
    private void eatItem(GameObject item)
    {
        MngSound.Instance.PlaySoundOnly(MngSound.Instance.effectClip[0]);
        MngGame.Instance.tank2Point += 500;
        Instantiate(MngGame.Instance.effectAnim[7], transform.position, Quaternion.identity);
        MngGame.Instance.addPointP2(500);
        Destroy(item);
    }
    private void DestroyPlayer()
    {
        MngSound.Instance.PlaySound(MngSound.Instance.effectClip[3]);
        Instantiate(MngGame.Instance.effectAnim[1], transform.position, Quaternion.identity);
        MngGame.Instance.tank2Life -= 1;
        MngGame.Instance.tank2lvBullet = 1;
        MngGame.Instance.tank2BulletCanShoot = 1;
        MngScene2.Instance.SetLife2UIShow();
        animator.SetBool("isMove", false);
        MngSound.Instance.OffPlayerMoveSound();
        if (MngGame.Instance.tank2Life > 0) { MngGame.Instance.SpawnPlayer(2); }
        MngGame.Instance.CheckGameOver();
        Destroy(gameObject);
    }
}