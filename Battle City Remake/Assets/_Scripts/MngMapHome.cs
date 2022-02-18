using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MngMapHome : MonoBehaviour
{
    public static MngMapHome Instance;

    private bool isHorizontalBullet;
    private int levelBullet;
    private Tilemap tilemapHome;

    public TileBase rock;
    public TileBase brick1;
    public TileBase brick2;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else { Destroy(gameObject); }
    }
    void Start()
    {
        tilemapHome = GetComponent<Tilemap>();
    }

    public void HomeToBrick()
    {
        Matrix4x4 matrix = Matrix4x4.Scale(new Vector3(4, 4));
        transform.localScale = new Vector3(1, 1, 1);
        for (int x = -4; x < 4; x++)
        {
            for(int y = 0;y>-30;y--)
            {
                Vector3Int p = new Vector3Int(x, y, 0);
                tilemapHome.SetTile(tilemapHome.WorldToCell(p), null);
            }
        }

        for (int x = -4; x < 4; x++)
        {
            for (int y = -21; y > -27; y--)
            {
                Vector3Int p = new Vector3Int(x, y, 0);
                if ((x + y) % 2 == 0)
                {
                    tilemapHome.SetTile(tilemapHome.WorldToCell(p), brick1);
                }
                else { tilemapHome.SetTile(tilemapHome.WorldToCell(p), brick2); }
                tilemapHome.SetTransformMatrix(p, matrix);
            }
        }
        for (int x = -2; x < 2; x++)
        {
            for (int y = -23; y > -27; y--)
            {
                Vector3Int p = new Vector3Int(x, y, 0);
                tilemapHome.SetTile(tilemapHome.WorldToCell(p), null);
            }
        }
        transform.localScale = new Vector3(0.5f, 0.5f, 1);
    }
    
    public void HomeToRock()
    {
        Matrix4x4 matrix = Matrix4x4.Scale(new Vector3(2, 2));
        transform.localScale = new Vector3(1, 1, 1);
        for (int x = -4; x < 4; x++)
        {
            for (int y = 0; y > -30; y--)
            {
                Vector3Int p = new Vector3Int(x, y, 0);
                tilemapHome.SetTile(tilemapHome.WorldToCell(p), null);
            }
        }
        for (int x = -2; x < 2; x++)
        {
            for (int y = -11; y > -14; y--)
            {
                Vector3Int p = new Vector3Int(x, y, 0);
                tilemapHome.SetTile(tilemapHome.WorldToCell(p), rock);
                tilemapHome.SetTransformMatrix(p, matrix);
            }
        }
        tilemapHome.SetTile(tilemapHome.WorldToCell(new Vector3(-1, -12, 0)), null);
        tilemapHome.SetTile(tilemapHome.WorldToCell(new Vector3(-1, -13, 0)), null);
        tilemapHome.SetTile(tilemapHome.WorldToCell(new Vector3(0, -12, 0)), null);
        tilemapHome.SetTile(tilemapHome.WorldToCell(new Vector3(0, -13, 0)), null);
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



            if (isHorizontalBullet && levelBullet < 4 && !MngGame.Instance.item3)
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

            if (isHorizontalBullet && levelBullet == 4 && !MngGame.Instance.item3)
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

            if (!isHorizontalBullet && levelBullet < 4 && !MngGame.Instance.item3)
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

            if (!isHorizontalBullet && levelBullet == 4 && !MngGame.Instance.item3)
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
