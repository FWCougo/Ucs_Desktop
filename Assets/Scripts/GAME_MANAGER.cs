using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GAME_MANAGER : MonoBehaviour
{
    public static GAME_MANAGER Instance;

    public GameObject player_1;

    public bool isDead = false;

    [SerializeField] private bool isPaused;

    [SerializeField] private int killCount = 0;

    [SerializeField] private TMP_Text kill_TXT;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        killCount = 0;
        kill_TXT.text = "KILLS: " + killCount;
    }

    public void IncreaseKillCount()
    {
        killCount++;
        SPAWN_MANAGER.Instance.ReceiveKillCount();
        kill_TXT.text = "KILLS: " + killCount;
    }

    public void StartGame() 
    { 
        player_1.SetActive(true);
        SPAWN_MANAGER.Instance.StartSpawning();
    }

    public void GAMEOVER()
    {
        isDead = true;
        MENU_MANAGER.Instance.OpenMenu("GAMEOVER_MENU");
    }

    public void ReloadMainMenu()
    {
        SceneManager.LoadScene(0);
        killCount = 0;
    }

    public void PauseGame()
    {
        isPaused = !isPaused;

        if(isPaused)
        {
            Time.timeScale = 0;

            MENU_MANAGER.Instance.OpenMenu("PAUSE_MENU");
        }
        else
        {

            Time.timeScale = 1;

            MENU_MANAGER.Instance.OpenMenu("IN-GAME_MENU");
        }
        
    }
}
