using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MngMapBrick : MonoBehaviour
{
    private Tilemap tilemapHome;

    private bool isHorizontalBullet;
    private int levelBullet;
    //public TileBase someTile;

    // Start is called before the first frame update
    void Start()
    {
        tilemapHome = GetComponent<Tilemap>();
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



            if (isHorizontalBullet && levelBullet < 4)
            {
                foreach (ContactPoint2D hit in collision.contacts)
                {
                    //right
                    hitPosition.x = hit.point.x + 0.1f;
                    hitPosition.y = hit.point.y;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x + 0.1f;
                    hitPosition.y = hit.point.y + 0.5f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x + 0.1f;
                    hitPosition.y = hit.point.y - 0.5f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    //left
                    hitPosition.x = hit.point.x - 0.1f;
                    hitPosition.y = hit.point.y;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x - 0.1f;
                    hitPosition.y = hit.point.y + 0.5f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x - 0.1f;
                    hitPosition.y = hit.point.y - 0.5f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);
                }
            }

            if (isHorizontalBullet && levelBullet == 4)
            {
                foreach (ContactPoint2D hit in collision.contacts)
                {
                    //right
                    hitPosition.x = hit.point.x + 0.1f;
                    hitPosition.y = hit.point.y;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x + 0.1f;
                    hitPosition.y = hit.point.y + 0.5f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x + 0.1f;
                    hitPosition.y = hit.point.y - 0.5f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x + 0.7f;
                    hitPosition.y = hit.point.y;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x + 0.7f;
                    hitPosition.y = hit.point.y + 0.5f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x + 0.7f;
                    hitPosition.y = hit.point.y - 0.5f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    //left
                    hitPosition.x = hit.point.x - 0.1f;
                    hitPosition.y = hit.point.y;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x - 0.1f;
                    hitPosition.y = hit.point.y + 0.5f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x - 0.1f;
                    hitPosition.y = hit.point.y - 0.5f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x - 0.7f;
                    hitPosition.y = hit.point.y;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x - 0.7f;
                    hitPosition.y = hit.point.y + 0.5f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x - 0.7f;
                    hitPosition.y = hit.point.y - 0.5f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);
                }
            }

            if (!isHorizontalBullet && levelBullet < 4)
            {
                foreach (ContactPoint2D hit in collision.contacts)
                {
                    //up
                    hitPosition.x = hit.point.x;
                    hitPosition.y = hit.point.y + 0.1f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x + 0.5f;
                    hitPosition.y = hit.point.y + 0.1f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x - 0.5f;
                    hitPosition.y = hit.point.y + 0.1f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    //down
                    hitPosition.x = hit.point.x;
                    hitPosition.y = hit.point.y - 0.1f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x - 0.5f;
                    hitPosition.y = hit.point.y - 0.1f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x + 0.5f;
                    hitPosition.y = hit.point.y - 0.1f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);
                }
            }

            if (!isHorizontalBullet && levelBullet == 4)
            {
                foreach (ContactPoint2D hit in collision.contacts)
                {
                    //up
                    hitPosition.x = hit.point.x;
                    hitPosition.y = hit.point.y + 0.1f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x + 0.5f;
                    hitPosition.y = hit.point.y + 0.1f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x - 0.5f;
                    hitPosition.y = hit.point.y + 0.1f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x;
                    hitPosition.y = hit.point.y + 0.7f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x + 0.5f;
                    hitPosition.y = hit.point.y + 0.7f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x - 0.5f;
                    hitPosition.y = hit.point.y + 0.7f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    //down
                    hitPosition.x = hit.point.x;
                    hitPosition.y = hit.point.y - 0.1f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x - 0.5f;
                    hitPosition.y = hit.point.y - 0.1f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x + 0.5f;
                    hitPosition.y = hit.point.y - 0.1f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x;
                    hitPosition.y = hit.point.y - 0.7f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x + 0.5f;
                    hitPosition.y = hit.point.y - 0.7f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);

                    hitPosition.x = hit.point.x - 0.5f;
                    hitPosition.y = hit.point.y - 0.7f;
                    tilemapHome.SetTile(tilemapHome.WorldToCell(hitPosition), null);
                }
            }
        }
    }
}
