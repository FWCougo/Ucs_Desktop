using DG.Tweening;
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
    private int clockTime = 11;

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

    [SerializeField] private GameObject WIN_GO;

    [Header("Ground")]
    [SerializeField] private SpriteRenderer groundSprite;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        changeStuffIDK = 0;
        timeSlider.maxValue = 7 * 60;
        timeSlider.value = 0;
        clockTime = 11;
        startedRound = false;
        isSpawning = false;
        spawnRate = 3;

        WIN_GO.SetActive(false);

        groundSprite.color = startColor;
    }

    public void StartRound()
    {
        startedRound = true;
        isSpawning = true;
        currentEnemyList = enemyPool[0].enemyList;
        spawnRate = enemyPool[0].spawnRate;
        StartCoroutine(SpawnEnemies());
        groundSprite.DOColor(endColor, 7 * 60).SetEase(Ease.InSine);
    }

    private void Update()
    {
        if (!startedRound) return;

        elapsedTime += Time.deltaTime;
        changeStuffIDK += Time.deltaTime;

        timeSlider.value = elapsedTime;

        if (changeStuffIDK > 60 && clockTime != 6)
        {
            ChangeRound();
        }

        if(clockTime == 6 && GAME_MANAGER.Instance.enemyCount == 0)
        {
            WIN_GO.SetActive(true);
        }
    }

    private void ChangeRound()
    {
        source.PlayOneShot(clip_Bell);
        round++;
        //Pega a lista de Inimigos deste round
        currentEnemyList = enemyPool[round-1].enemyList;
        changeStuffIDK = 0;
        ChangeTime();
        spawnRate = enemyPool[round-1].spawnRate;

        //spawnRate -= 0.1f;
    }

    private void ChangeTime()
    {
        string postTime;

        if(clockTime == 11)
        {
            clockTime = 0;
            postTime = "PM";
        }
        else
        {
            clockTime++;
            postTime = "AM";
        }

        time_TXT.text = $"{clockTime} {postTime}";


        if (clockTime == 6)
        {
            isSpawning = false;
        }
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(5);

        while (isSpawning) {

            //SELECT SPAWN POINT
            int _spawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
            Vector3 _spawnPoint = spawnPoints[_spawnPointIndex].position;

            Vector2 randomCircle = UnityEngine.Random.insideUnitCircle * 4;
            Vector3 spawnPos = _spawnPoint + new Vector3(randomCircle.x, 0f, randomCircle.y);

            int selectedEnemy = UnityEngine.Random.Range(0,currentEnemyList.Length);

            ENEMY _enemy = currentEnemyList[selectedEnemy];

            //SPAWN MONSTER (LATER CHANGE SO IT USES OBJECT POOLING)
            Instantiate(_enemy, spawnPos, Quaternion.identity);
            GAME_MANAGER.Instance.enemyCount++;

            yield return new WaitForSeconds(spawnRate);

        }        
    }
}


[Serializable]
public class ENEMYPOOL
{
    public ENEMY[] enemyList;

    public float spawnRate = 3;
}
