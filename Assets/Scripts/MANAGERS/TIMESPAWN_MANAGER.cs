using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class TIMESPAWN_MANAGER : MonoBehaviour
{
    public bool startedRound = false;

    [SerializeField]
    private int round = 1;
    [SerializeField]
    private float spawnRate = 3;
    [SerializeField]
    private bool isSpawning = false;

    [SerializeField]
    private int clockTime = 6;

    [SerializeField]
    private float elapsedTime;
    [SerializeField]
    private Slider timeSlider;
    [SerializeField]
    private TMP_Text time_TXT;

    [SerializeField] Transform[] spawnPoints;
    public ENEMYPOOL[] enemyPool;
    public ENEMY[] currentEnemyList;

    public static TIMESPAWN_MANAGER Instance;

    float changeStuffIDK;

    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip_Bell;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        changeStuffIDK = 0;
        timeSlider.value = 0;
        clockTime = 6;
        startedRound = false;
        isSpawning = false;
        spawnRate = 3;
    }

    public void StartRound()
    {
        startedRound = true;
        isSpawning = true;
        currentEnemyList = enemyPool[round].enemyList;
        StartCoroutine(SpawnEnemies());
    }

    private void Update()
    {
        if (!startedRound) return;

        elapsedTime = Time.time;
        changeStuffIDK += Time.deltaTime;

        timeSlider.value = elapsedTime;

        if (changeStuffIDK > 60)
        {
            ChangeRound();
        }
    }

    private void ChangeRound()
    {
        source.PlayOneShot(clip_Bell);
        round++;
        //Pega a lista de Inimigos deste round
        currentEnemyList = enemyPool[round].enemyList;
        changeStuffIDK = 0;
        ChangeTime();
        spawnRate -= 0.1f;
    }

    private void ChangeTime()
    {
        string postTime;

        if(clockTime == 11)
        {
            clockTime = 0;
            postTime = "AM";
        }
        else
        {
            clockTime++;
            postTime = "PM";
        }

        time_TXT.text = $"{clockTime} {postTime}";
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(5);

        while (isSpawning) {

            //SELECT SPAWN POINT
            int _spawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
            Transform _spawnPoint = spawnPoints[_spawnPointIndex];

            int selectedEnemy = UnityEngine.Random.Range(0,currentEnemyList.Length);

            ENEMY _enemy = currentEnemyList[selectedEnemy];

            //SPAWN MONSTER (LATER CHANGE SO IT USES OBJECT POOLING)
            Instantiate(_enemy, _spawnPoint.position, Quaternion.identity);

            yield return new WaitForSeconds(spawnRate);

        }        
    }
}


[Serializable]
public class ENEMYPOOL
{
    public ENEMY[] enemyList;
}
