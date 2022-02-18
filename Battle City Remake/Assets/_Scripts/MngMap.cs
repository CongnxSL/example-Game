using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MngMap : MonoBehaviour
{
    public static MngMap Instance;
    public List<GameObject> stageMap;
    private GameObject map;
    private MngMapHome homeManager;

    private bool blink;
    private float timerBlink;
    private int countBlink;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ResetMap();
        }
        if (blink)
        {
            timerBlink += Time.deltaTime;
            BlinkHome();
        }
    }
    public void ResetMap()
    {
        StopAllCoroutines();
        blink = false;
        timerBlink = 0;
        countBlink = 0;
    }
    public void BuildMap(int stage)
    {
        map = Instantiate(stageMap[(stage - 1) % 7], new Vector3(0, 0, 0), Quaternion.identity);
        homeManager = GameObject.Find("Home").GetComponent<MngMapHome>();
    }
    public void Item3()
    {
        StartCoroutine(Item3Timer());
    }
    private void BlinkHome()
    {
        if (timerBlink > 0.5f)
        {
            timerBlink = 0;
            countBlink += 1;
        }
        if (countBlink % 2 == 0)
        {
            homeManager.HomeToRock();
        }
        else { homeManager.HomeToBrick(); }
        if (countBlink == 10) 
        { 
            blink = false;
            homeManager.HomeToBrick();
            MngGame.Instance.item3 = false;
        }
    }
    IEnumerator Item3Timer()
    {
        homeManager.HomeToRock();
        MngGame.Instance.item3 = true;
        yield return new WaitForSeconds(15);
        homeManager.HomeToBrick();
        blink = true;
    }
}
