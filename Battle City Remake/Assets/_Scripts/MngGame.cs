using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MngGame : MonoBehaviour
{
    public static MngGame Instance;

    public List<GameObject> tank;
    public List<GameObject> effectAnim; // 0: destroy bullet, 1: destroy tank, 2: spawn
    public bool pause,isBannerIn;

    public bool isPlay;
    public bool isGameOver;
    public bool player2;
    public int stage = 0;

    public int tank1Life = 0;
    public int tank1lvBullet = 1;
    public int tank1BulletCanShoot;
    public bool tank1Item1;
    public int tank1Point = 0;
    public List<int> tank1CountEnemy = new List<int> { 0, 0, 0, 0 };

    public int tank2Life = 0;
    public int tank2lvBullet = 0;
    public int tank2BulletCanShoot = 0;
    public bool tank2Item1;
    public int tank2Point = 0;
    public List<int> tank2CountEnemy = new List<int> { 0, 0, 0, 0 };

    public List<int> countBulletShooted = new List<int> { 0, 0, 5, 5, 5, 5 };
    public List<int> listEnemy;
    private List<int> listEnemy1 = new List<int> { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3 };
    private List<int> listEnemy2 = new List<int> { 5, 5, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };

    private float timerEnemySpawn;
    private float timeLoopSpawn = 3;
    private Vector3 posESpawn;
    public int enemyID, enemyOnMap = 0;

    public bool item2 = false;
    public bool item3 = false;
    public bool item5 = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ResetGame();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            pause = !pause;
            if (pause)
            {
                Time.timeScale = 0;
                MngSound.Instance.PlaySoundOnly(MngSound.Instance.effectClip[7]);
            }
            else
            {
                Time.timeScale = 1;
                MngSound.Instance.PlaySoundOnly(MngSound.Instance.effectClip[7]);
            }
        }
        if(isPlay)
        {
            if(enemyID < listEnemy.Count) { SpawnEnemy(); }
            if (enemyID == listEnemy.Count && enemyOnMap == 0)
            {
                isPlay = false;
                ShowScore();
                Invoke("NextStage", 8);
                /*
                if (stage < MngMap.Instance.stageMap.Count)
                {
                    Invoke("NextStage", 8);
                }
                if (stage == MngMap.Instance.stageMap.Count) { isGameOver = true; }
                */
            }
            if (item2 || enemyOnMap == 0 || pause) { MngSound.Instance.OffEnemyMoveSound(); }
            else { MngSound.Instance.OnEnemyMoveSound(); }
        }
    }
    public void ResetGame()
    {
        StopAllCoroutines();
        MngSound.Instance.OffEnemyMoveSound();
        pause = false;
        isPlay = false;
        isGameOver = false;
        isBannerIn = false;
        item2 = false;
        item3 = false;
        item5 = false;
        stage = 0;

        tank1Life = 0;
        tank1lvBullet = 0;
        tank1Item1 = false;
        tank1Point = 0;
        tank1CountEnemy = new List<int> { 0, 0, 0, 0 };

        tank2Life = 0;
        tank2lvBullet = 0;
        tank2BulletCanShoot = 0;
        tank2Item1 = false;
        tank2Point = 0;
        tank2CountEnemy = new List<int> { 0, 0, 0, 0 };

        countBulletShooted = new List<int> { 0, 0, 5, 5, 5, 5 };
        enemyID = 0; 
        enemyOnMap = 0;
        SceneManager.LoadScene(0);
    }
    public void NextStage()
    {
        StopAllCoroutines();
        MngMap.Instance.ResetMap();
        stage += 1;
        enemyID = 0;
        item2 = false;
        item3 = false;
        item5 = false;

        tank1Item1 = false;
        tank1CountEnemy = new List<int> { 0, 0, 0, 0 };

        tank2Item1 = false;
        tank2CountEnemy = new List<int> { 0, 0, 0, 0 };
        

        SceneManager.LoadScene(1);
        MngSound.Instance.PlaySoundOnly(MngSound.Instance.effectClip[5]);
        Invoke("LoadingStage", 2);
        if (stage == 1) { listEnemy = listEnemy1; }
        else if ( stage == 2) { listEnemy = listEnemy2; }
        else { MakeListEnemy(); }
    }
    public void LoadingStage()
    {
        MngMap.Instance.BuildMap(stage);
        if (tank1Life > 0) { SpawnPlayer(1); }
        if (tank2Life > 0) { SpawnPlayer(2); }
        isPlay = true;
    }
    void MakeListEnemy()
    {
        listEnemy = new List<int> { };
        for(int i = 0; i < listEnemy1.Count; i++)
        {
            var per = Random.Range(0, 100);
            if (per < 60)
            {
                listEnemy.Add(2);
            }
            if (per < 75 && per >= 60)
            {
                listEnemy.Add(3);
            }
            if (per >= 75 && per < 90)
            {
                listEnemy.Add(4);
            }
            if (per >= 90)
            {
                listEnemy.Add(5);
            }
        }
    }
    public void addPointP1(int point)
    {
        tank1Point += point;
        if (tank1Point > 20000) { UpLife(0); }
    }

    public void addPointP2(int point)
    {
        tank2Point += point;
        if (tank2Point > 20000) { UpLife(1); }
    }

    public void Item1(int idtank,GameObject shield)
    {
        StartCoroutine(Item1Timer(idtank, shield));
    }
    public void Item2()
    {
        StartCoroutine(Item2Timer());
    }
    public void Item5()
    {
        StartCoroutine(Item5Timer());
    }

    public void UpLvBullet(int idTank, int lv)
    {
        switch (idTank)
        {
            case 0:
                lv = lv + tank1lvBullet;
                if (lv > 4) { lv = 4; }
                tank1lvBullet = lv;
                if (tank1lvBullet <= 2) { tank1BulletCanShoot = 1; }
                else { tank1BulletCanShoot = 2; }
                break;
            case 1:
                lv = lv + tank2lvBullet;
                if (lv > 4) { lv = 4; }
                tank2lvBullet = lv;
                if (tank2lvBullet <= 2) { tank2BulletCanShoot = 1; }
                else { tank2BulletCanShoot = 2; }
                break;
        }
    }
    public void UpLife(int idTank)
    {
        switch (idTank)
        {
            case 0:
                tank1Life += 1;
                MngScene2.Instance.SetLife1UIShow();
                break;
            case 1:
                tank2Life += 1;
                MngScene2.Instance.SetLife1UIShow();
                break;
        }
        MngSound.Instance.PlaySoundOnly(MngSound.Instance.effectClip[6]);
    }

    void SpawnEnemy()
    {
        if (enemyOnMap < 4) { timerEnemySpawn += Time.deltaTime; }
        if (timerEnemySpawn >= timeLoopSpawn)
        {
            for (int i = 0; i < countBulletShooted.Count; i++)
            {
                if (countBulletShooted[i] == 5)
                {
                    StartCoroutine(Enemy(enemyID % 3, i));
                    break;
                }
            }

            timerEnemySpawn = 0;
            
        }
    }
    public void SpawnPlayer(int id)
    {
        switch(id)
        {
            case 1:
                StartCoroutine(Player(0, new Vector3(-4, -12, 0)));
                break;
            case 2:
                StartCoroutine(Player(1, new Vector3(4, -12, 0)));
                break;
        }
    }
    
    public void CheckGameOver()
    {
        if (tank1Life == 0 && tank2Life == 0)
        {
            isGameOver = true;
            isPlay = false;
        }
    }
    public void ShowScore()
    {
        StartCoroutine(ShowScoreTimer());
    }
    IEnumerator Player(int idTank, Vector3 p)
    {
        Instantiate(effectAnim[2], p, Quaternion.identity);
        yield return new WaitForSeconds(1);

        Instantiate(tank[idTank], p, Quaternion.identity);
    }
    IEnumerator Enemy(int location, int id)
    {
        switch(location)
        {
            case 0:
                posESpawn = new Vector3(0, 12, 0);
                break;
            case 1:
                posESpawn = new Vector3(12, 12, 0);
                break;
            case 2:
                posESpawn = new Vector3(-12, 12, 0);
                break;
        }
        Instantiate(effectAnim[2], posESpawn, Quaternion.identity);
        yield return new WaitForSeconds(1);

        GameObject tankPrefab = Instantiate(tank[listEnemy[enemyID]], posESpawn, Quaternion.Euler(0, 0, 180));

        tankPrefab.GetComponent<MngEnemy>().idTank = id;
        enemyOnMap += 1;
        enemyID += 1;
        countBulletShooted[id] = 0;
        MngScene2.Instance.SetEnemyUIShow();

        if (enemyID == 3 || enemyID == 10 || enemyID == 17) { tankPrefab.GetComponent<MngEnemy>().item = true; }
    }
    IEnumerator Item1Timer(int idtank, GameObject shield)
    {
        shield.SetActive(true);
        switch (idtank)
        {
            case 0:
                tank1Item1 = true;
                break;
            case 1:
                tank2Item1 = true;
                break;
        }
        yield return new WaitForSeconds(15);
        shield.SetActive(false);
        switch (idtank)
        {
            case 0:
                tank1Item1 = false;
                break;
            case 1:
                tank2Item1 = false;
                break;
        }
    }
    IEnumerator Item2Timer()
    {
        item2 = true;
        yield return new WaitForSeconds(15);
        item2 = false;
    }
    IEnumerator Item5Timer()
    {
        item5 = true;
        yield return new WaitForSeconds(0.5f);
        item5 = false;
    }
    IEnumerator ShowScoreTimer()
    {
        yield return new WaitForSeconds(2);
        MngScene2.Instance.ScoreShowOn();
        yield return new WaitForSeconds(3);
        MngScene2.Instance.ScoreShowOff();
        yield return new WaitForSeconds(1);
        isBannerIn = true;
        yield return new WaitForSeconds(1);
        isBannerIn = false;
    }
}
