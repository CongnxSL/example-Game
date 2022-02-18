using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MngScene2 : MonoBehaviour
{
    public static MngScene2 Instance;

    public GameObject up, down, stage, scoreShow, player2Score, player2UI, gameOver, hiScore;
    public List<GameObject> enemyUI;
    RectTransform rectUp, rectDown, rectGameOer;
    public Text stageTextScene1, stageTextScene2, p1Point, p1Ce1, p1Ce2, p1Ce3, p1Ce4,
                                                  p2Point, p2Ce1, p2Ce2, p2Ce3, p2Ce4,
                                                  totalP1, totalP2, score, hiScoreText,
                                                  stageUI, p1Life, p2Life;
    private float timerShowGameOver;
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
        rectUp = up.GetComponent<RectTransform>();
        rectDown = down.GetComponent<RectTransform>();
        rectGameOer = gameOver.GetComponent<RectTransform>();

        stageUI.text = (MngGame.Instance.stage).ToString();
        if (MngGame.Instance.player2) { player2UI.SetActive(true); }
        stageTextScene1.text = "STAGE   " + MngGame.Instance.stage;
        SetLife1UIShow();
        SetLife2UIShow();
    }

    // Update is called once per frame
    void Update()
    {
        if (MngGame.Instance.isPlay) { BannerOut(); }
        if (MngGame.Instance.isGameOver) { GameOer(); }
        if (rectGameOer.localPosition.y >= 0 && timerShowGameOver < 20)
        {
            timerShowGameOver += Time.deltaTime;
            if (timerShowGameOver > 2)
            {
                gameOver.gameObject.SetActive(false);
                ScoreShowOn();
                MngGame.Instance.Item2();
                MngSound.Instance.OffEnemyMoveSound();
            }
            if (timerShowGameOver >= 6) 
            { 
                HiScore();
            }
            if (timerShowGameOver >= 15)
            {
                MngGame.Instance.ResetGame();
            }
        }
        if (MngGame.Instance.isBannerIn)
        {
            MngScene2.Instance.BannerIn();
        }
    }
    public void BannerIn()
    {
        if (rectUp.localPosition.y > 150)
        {
            rectUp.localPosition += Vector3.down * 6;
        }
        if (rectDown.localPosition.y < -150)
        {
            rectDown.localPosition += Vector3.up * 6;
        }
        if (rectDown.localPosition.y >= -150)
        {
            stage.gameObject.SetActive(true);
            stageTextScene1.text = "STAGE   " + (MngGame.Instance.stage + 1).ToString();
        }
    }
    void BannerOut()
    {
        stage.gameObject.SetActive(false);
        if (rectUp.localPosition.y < 460)
        {
            rectUp.localPosition += Vector3.up * 6;
        }
        if (rectDown.localPosition.y > -460)
        {
            rectDown.localPosition += Vector3.down * 6;
        }
    }
    public void ScoreShowOn()
    {
        scoreShow.gameObject.SetActive(true);
        if (MngGame.Instance.player2)
        {
            player2Score.gameObject.SetActive(true);

            p2Point.text = MngGame.Instance.tank2Point.ToString();
            p2Ce1.text = (MngGame.Instance.tank2CountEnemy[0]).ToString();
            p2Ce2.text = (MngGame.Instance.tank2CountEnemy[1]).ToString();
            p2Ce3.text = (MngGame.Instance.tank2CountEnemy[2]).ToString();
            p2Ce4.text = (MngGame.Instance.tank2CountEnemy[3]).ToString();
            totalP2.text = (MngGame.Instance.tank2CountEnemy[0] + MngGame.Instance.tank2CountEnemy[1] +
                            MngGame.Instance.tank2CountEnemy[2] + MngGame.Instance.tank2CountEnemy[3]).ToString();
        }
        stageTextScene2.text = "STAGE   " + MngGame.Instance.stage;

        p1Point.text = MngGame.Instance.tank1Point.ToString();
        p1Ce1.text = (MngGame.Instance.tank1CountEnemy[0]).ToString();
        p1Ce2.text = (MngGame.Instance.tank1CountEnemy[1]).ToString();
        p1Ce3.text = (MngGame.Instance.tank1CountEnemy[2]).ToString();
        p1Ce4.text = (MngGame.Instance.tank1CountEnemy[3]).ToString();
        totalP1.text = (MngGame.Instance.tank1CountEnemy[0] + MngGame.Instance.tank1CountEnemy[1] +
                        MngGame.Instance.tank1CountEnemy[2] + MngGame.Instance.tank1CountEnemy[3]).ToString();
    }
    public void ScoreShowOff()
    {
        scoreShow.gameObject.SetActive(false);
    }
    public void SetEnemyUIShow()
    {
        enemyUI[enemyUI.Count - MngGame.Instance.enemyID].gameObject.SetActive(false);
    }
    public void SetLife1UIShow()
    {
        p1Life.text = (MngGame.Instance.tank1Life).ToString();
    }
    public void SetLife2UIShow()
    {
        p2Life.text = (MngGame.Instance.tank2Life).ToString();
    }
    void GameOer()
    {
        if (rectGameOer.localPosition.y < 0)
        {
            rectGameOer.localPosition += Vector3.up * 2;
        }
        
    }
    void HiScore()
    {
        hiScore.SetActive(true);
        if(MngGame.Instance.tank1Point> MngGame.Instance.tank2Point) { score.text = (MngGame.Instance.tank1Point).ToString(); }
        else { score.text = (MngGame.Instance.tank2Point).ToString(); }
    }
}
