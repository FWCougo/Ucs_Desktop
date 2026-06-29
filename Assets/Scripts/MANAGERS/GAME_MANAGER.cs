using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
using static Unity.VisualScripting.Member;

public class GAME_MANAGER : MonoBehaviour
{
    public static GAME_MANAGER Instance;

    public PLAYER_MANAGER playerManager;

    public int enemyCount = 0;

    public bool isDead = false;
    [SerializeField] private bool isPaused;

    [Header("SFX")]
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip gameOver_SFX;

    [Header("Controles")]
    [SerializeField] private SpriteRenderer wasdSprite;
    [SerializeField] private SpriteRenderer mouseSprite;

    [Header("MUSIC")]
    [SerializeField] private AudioSource music_source;

    [Header("KILLS")]
    [SerializeField] private int killCount = 0;
    [SerializeField] private TMP_Text kill_TXT;

    [Header("COINS")]
    [SerializeField] private int coins = 0;
    [SerializeField] private TMP_Text coinsGame_TXT;
    [SerializeField] private TMP_Text coinsUpgrade_TXT;

    private void Awake()
    {
        Instance = this;
        LoadCoins();
    }

    private void Start()
    {
        Cursor.visible = true;
        killCount = 0;
        kill_TXT.text = killCount.ToString();
    }

    public void IncreaseKillCount()
    {
        killCount++;
        //SPAWN_MANAGER.Instance.ReceiveKillCount();
        kill_TXT.text = killCount.ToString();
    }

    public void StartGame()
    {
        //Camera
        //multiTargetCam.enabled = true;
        //cameraHolder.DORotate(new Vector3(80, 0, 0), 3f);

        //start Music
        music_source.Play();

        //Start Devil
        DEVIL_SCRIPT.Instance.StartGame();


        //Open Game Menu
        MENU_MANAGER.Instance.OpenMenu("IN-GAME_MENU");

        Cursor.visible = false;
        //SPAWN_MANAGER.Instance.StartSpawning();
        TIMESPAWN_MANAGER.Instance.StartRound();
        playerManager.StartGame();

        FadeControls();
    }

    public void FadeControls()
    {
        mouseSprite.DOFade(1, 1).OnComplete(() =>
        {
            mouseSprite.DOFade(0, 15);
        });
        wasdSprite.DOFade(1, 1).OnComplete(() =>
        {
            wasdSprite.DOFade(0, 15);
        });
    }

    public void GAMEOVER()
    {
        source.PlayOneShot(gameOver_SFX);
        Cursor.visible = true;
        isDead = true;
        MENU_MANAGER.Instance.OpenMenu("GAMEOVER_MENU");
    }

    public void ReloadMainMenu()
    {
        Time.timeScale = 1;
        DOTween.KillAll();
        SceneManager.LoadScene(0);
        killCount = 0;
    }

    public void PauseGame()
    {
        isPaused = !isPaused;

        if(isPaused)
        {
            music_source.Pause();
            Time.timeScale = 0;
            Cursor.visible = true;
            MENU_MANAGER.Instance.OpenMenu("PAUSE_MENU");
        }
        else
        {
            music_source.UnPause();
            Time.timeScale = 1;
            Cursor.visible = false;
            MENU_MANAGER.Instance.OpenMenu("IN-GAME_MENU");
        }
        
    }

    [SerializeField] private float extraDMG = 0;

    public void SetExtraDMG(float _extraDMG)
    {
        extraDMG = _extraDMG;
    }
    public float GetExtraDMG()
    {
        return extraDMG;
    }

    #region COINS
    public void LoadCoins()
    {
        coins = PlayerPrefs.GetInt("COIN_KEY");
        coinsUpgrade_TXT.text = $"{coins}";
        coinsGame_TXT.text = $"{coins}";
    }
    public void ChangeCoins(int _i)
    {
        coins += _i;
        coinsUpgrade_TXT.text = $"{coins}";
        coinsGame_TXT.text = $"{coins}";
        PlayerPrefs.SetInt("COIN_KEY", coins);
    }
    public void ResetCoins()
    {
        coins = 0;
        coinsUpgrade_TXT.text = $"{coins}";
        coinsGame_TXT.text = $"{coins}";
        PlayerPrefs.SetInt("COIN_KEY", coins);
    }
    public int GetCoins()
    {
        return coins;
    }
    #endregion
}
