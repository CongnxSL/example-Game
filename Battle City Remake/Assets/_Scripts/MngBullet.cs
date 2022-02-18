using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MngBullet : MonoBehaviour
{
    private Rigidbody2D bulletRb;
    private float speed;
    public int idBullet;
    public int levelBullet = 1;
    void Start()
    {
        bulletRb = gameObject.GetComponent<Rigidbody2D>();
        if(levelBullet==1)
        {
            speed = 50;
        }
        else { speed = 100; }
    }

    private void FixedUpdate()
    {
        if (transform.eulerAngles.z == 0)
        {
            bulletRb.velocity = Vector2.up * speed;
        }
        if (transform.eulerAngles.z == 90)
        {
            bulletRb.velocity = Vector2.left * speed;
        }
        if (transform.eulerAngles.z == 180)
        {
            bulletRb.velocity = Vector2.down * speed;
        }
        if (transform.eulerAngles.z == 270)
        {
            bulletRb.velocity = Vector2.right * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") ||
            collision.gameObject.CompareTag("Map") ||
            collision.gameObject.CompareTag("Player 1") ||
            collision.gameObject.CompareTag("Player 2") ||
            collision.gameObject.CompareTag("Enemy 1") ||
            collision.gameObject.CompareTag("Enemy 2") ||
            collision.gameObject.CompareTag("Enemy 3") ||
            collision.gameObject.CompareTag("Enemy 4") ||
            collision.gameObject.CompareTag("Bird"))
        {
            DestroyBullet();
        }
        if(collision.gameObject.CompareTag("Bird")) 
        { 
            MngGame.Instance.isGameOver = true;
            MngGame.Instance.isPlay = false;
            MngSound.Instance.PlaySound(MngSound.Instance.effectClip[3]);
            Instantiate(MngGame.Instance.effectAnim[1], collision.gameObject.transform.position, Quaternion.identity);
            collision.gameObject.SetActive(false);
        }
    }
    void DestroyBullet()
    {
        if (MngGame.Instance.countBulletShooted[idBullet] > 0) { MngGame.Instance.countBulletShooted[idBullet] -= 1; }
        Instantiate(MngGame.Instance.effectAnim[0], transform.position, transform.rotation);
        MngSound.Instance.PlaySound(MngSound.Instance.effectClip[1]);
        Destroy(gameObject);
    }
}
