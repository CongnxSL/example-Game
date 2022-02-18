using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MngScene1 : MonoBehaviour
{
    public GameObject icon, stage, ground;
    public Text stageText;
    RectTransform rectBanner, rectIcon;

    [SerializeField] private int screen = 1;
    private int stagePick = 1;

    void Start()
    {
        rectBanner = GetComponent<RectTransform>();
        rectIcon = icon.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (screen == 1)
        {
            if (rectBanner.localPosition.y > 0)
            {
                rectBanner.localPosition += Vector3.down;
            }
            if (rectBanner.localPosition.y <= 0)
            {
                rectBanner.localPosition = new Vector3(0, 0, 0);
            }
            if (rectBanner.localPosition.y==0)
            {
                screen = 2;
                icon.gameObject.SetActive(true);
            }
        }
        if (Input.anyKeyDown && screen == 1)
        {
            rectBanner.localPosition = new Vector3(0, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch(screen)
            {
                case 2:
                    screen = 3;
                    ground.gameObject.SetActive(true);
                    stage.gameObject.SetActive(true);
                    break;
                case 3:
                    MngGame.Instance.stage = stagePick - 1;
                    MngGame.Instance.tank1Life = 3;
                    MngGame.Instance.tank1lvBullet = 1;
                    MngGame.Instance.tank1BulletCanShoot = 1;
                    if(MngGame.Instance.player2)
                    {
                        MngGame.Instance.tank2Life = 3;
                        MngGame.Instance.tank2lvBullet = 1;
                        MngGame.Instance.tank2BulletCanShoot = 1;
                    }
                    MngGame.Instance.NextStage();
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            switch(screen)
            {
                case 2:
                    MngGame.Instance.player2 = !MngGame.Instance.player2;
                    break;
                case 3:
                    if (stagePick < MngMap.Instance.stageMap.Count)
                    {
                        stagePick += 1;
                        stageText.text = "STAGE   " + stagePick;
                    }
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            switch (screen)
            {
                case 2:
                    MngGame.Instance.player2 = !MngGame.Instance.player2;
                    break;
                case 3:
                    if (stagePick > 1)
                    {
                        stagePick -= 1;
                        stageText.text = "STAGE   " + stagePick;
                    }
                    break;
            }
        }

        if (!MngGame.Instance.player2 && screen == 2)
        {
            rectIcon.localPosition = new Vector3(-125, -45, 0);
        }
        else { rectIcon.localPosition = new Vector3(-125, -83, 0); }
    }
}
