using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GAME_MANAGER : MonoBehaviour
{
    public static GAME_MANAGER Instance;

    public PLAYER_MANAGER playerManager;

    public bool isDead = false;

    [SerializeField] private bool isPaused;

    [SerializeField] private int killCount = 0;

    [SerializeField] private TMP_Text kill_TXT;

    [SerializeField] private MultipleTargetCamera multiTargetCam;
    [SerializeField] private Transform cameraHolder;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Cursor.visible = true;
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
        //Camera
        multiTargetCam.enabled = true;
        cameraHolder.DORotate(new Vector3(80, 0, 0), 3f);

        //Open Game Menu
        MENU_MANAGER.Instance.OpenMenu("IN-GAME_MENU");

        Cursor.visible = false;
        SPAWN_MANAGER.Instance.StartSpawning();
        playerManager.StartGame();
    }

    public void GAMEOVER()
    {
        Cursor.visible = true;
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
            Cursor.visible = true;
            MENU_MANAGER.Instance.OpenMenu("PAUSE_MENU");
        }
        else
        {

            Time.timeScale = 1;
            Cursor.visible = false;
            MENU_MANAGER.Instance.OpenMenu("IN-GAME_MENU");
        }
        
    }
}
