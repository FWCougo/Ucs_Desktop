using System.Collections;
using UnityEngine;

public class SPAWN_MANAGER : MonoBehaviour
{
    [SerializeField] GameObject goblin_prefab, slime_prefab;

    [SerializeField] private GameObject[] monsters_prefabs;

    [SerializeField] Transform[] spawnPoints;

    [SerializeField] private int round = 1;


    [SerializeField] private int killedInThisRound = 0;
    [SerializeField] private int killToChange = 10;

    [SerializeField] private int monstersToSpawn = 10;

    [SerializeField] private int timeToSpawn = 3;

    [SerializeField] private int timeBetweenRounds = 10;

    [SerializeField] private bool isSpawning = false;

    public static SPAWN_MANAGER Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        killedInThisRound = 0;
        killToChange = 20;
        monstersToSpawn = 20;
    }

    public void CreateList()
    {
        monsters_prefabs = new GameObject[2];

        monsters_prefabs[0] = goblin_prefab;
        monsters_prefabs[1] = slime_prefab;
    }

    public void SetKillsToChange()
    {
        round++;
        killedInThisRound = 0;
        killToChange = 20 * round;
        monstersToSpawn = killToChange;
    }

    public void ReceiveKillCount()
    {
        killedInThisRound++;

        if(killedInThisRound >= killToChange) 
        {          
            SetKillsToChange();

            StartRound();
        }


    }

    public void StartSpawning()
    {
        CreateList();
        StartRound();
    }

    public void StartRound()
    {
        StartCoroutine(SpawnMonsters());
    }


    IEnumerator SpawnMonsters()
    {
        yield return new WaitForSeconds(timeBetweenRounds);

        isSpawning = true;

        while (isSpawning && monstersToSpawn > 0)
        {
            //SELECT SPAWN POINT
            int _spawnPointIndex = Random.Range(0, spawnPoints.Length);
            Transform _spawnPoint = spawnPoints[_spawnPointIndex];

            //SELECT MONSTER
            int selectMonster = Random.Range(0, 100);


            int monsterIndex = 0;
            if (selectMonster > 20)
            {
                monsterIndex = 0;
            }
            else
            {
                monsterIndex = 1;
            }

           // int monsterIndex = Random.Range(0, monsters_prefabs.Length);
            GameObject monster = monsters_prefabs[monsterIndex];

            //SPAWN MONSTER (LATER CHANGE SO IT USES OBJECT POOLING)
            Instantiate(monster, _spawnPoint.position, Quaternion.identity);

            monstersToSpawn--;

            yield return new WaitForSeconds(timeToSpawn);

            yield return null;
        }
    }




}
