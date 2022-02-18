using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MngMapRock : MonoBehaviour
{
    private Tilemap destroyBrick;

    private bool isHorizontalBullet;
    private int levelBullet;
    // Start is called before the first frame update
    void Start()
    {
        destroyBrick = GetComponent<Tilemap>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Vector3 hitPosition = Vector3.zero;

            if (collision.gameObject.transform.eulerAngles.z == 0 ||
               collision.gameObject.transform.eulerAngles.z == 180)
            {
                isHorizontalBullet = false;
            }
            else { isHorizontalBullet = true; }

            levelBullet = collision.gameObject.GetComponent<MngBullet>().levelBullet;

            if (isHorizontalBullet && levelBullet == 4)
            {
                foreach (ContactPoint2D hit in collision.contacts)
                {
                    hitPosition.x = hit.point.x + 0.1f;
                    hitPosition.y = hit.point.y;
                    destroyBrick.SetTile(destroyBrick.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x - 0.1f;
                    hitPosition.y = hit.point.y;
                    destroyBrick.SetTile(destroyBrick.WorldToCell(hitPosition), null);
                }
            }

            if (!isHorizontalBullet && levelBullet == 4)
            {
                foreach (ContactPoint2D hit in collision.contacts)
                {
                    hitPosition.x = hit.point.x;
                    hitPosition.y = hit.point.y + 0.1f;
                    destroyBrick.SetTile(destroyBrick.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x;
                    hitPosition.y = hit.point.y - 0.1f;
                    destroyBrick.SetTile(destroyBrick.WorldToCell(hitPosition), null);
                }
            }
        }
    }
}
