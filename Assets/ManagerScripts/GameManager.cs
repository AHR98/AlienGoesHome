using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Light lightLevel1;
    [SerializeField]
    private Light lightLevel2;
    public static GameManager instance;
    public UIManager getPanel;
    public GameObject MainMenuPanel;
    public GameObject IntroductionPanel;
    private GameObject slinkyPlayer;
    public GameObject niloPlayer;
    private SlikyController slikyController;
    private Gun slinkyGun;
    public bool loadeddata = false;
    private int level = 0;
    private int status = 0;
    private Vector3 positionLoad;

    public void saveData()
    {
        slikyController = slinkyPlayer.GetComponent<SlikyController>();
        slinkyGun = slinkyPlayer.GetComponent<Gun>();

        DataManager.savePlayer(slikyController, slinkyGun, slinkyPlayer);
    }

    public void loadData()
    {
        slikyController = slinkyPlayer.GetComponent<SlikyController>();
        slinkyGun = slinkyPlayer.GetComponent<Gun>();
        niloPlayer.GetComponent<NiloController>().followSlinky = true;
        niloPlayer.GetComponent<NiloController>().saveData = true;

        PlayerData data = DataManager.loadPlayer();
        
        level = data.level;
        slikyController.currentHealth = data.healthPlayer;
        slikyController.currentHypnosis = data.hpynosis;
        slinkyGun.currentBullets = data.bullets;
        positionLoad.x = data.positionPlayer[0];
        positionLoad.y = data.positionPlayer[1];
        positionLoad.z = data.positionPlayer[2];

        slinkyPlayer.GetComponent<CharacterController>().enabled = false;
        slinkyPlayer.transform.position = positionLoad;
        slinkyPlayer.GetComponent<CharacterController>().enabled = true;

        setLoadData();
    }

    private void setLoadData()
    {
        slikyController.setLoadData();
        slinkyGun.setLoadData();
        loadeddata = true;


    }
    private void Awake()
    {
        if(instance == null)
            instance = this;
        PauseGame();

    }

    private void Start()
    {
        slinkyPlayer = PlayerManager.instance.playerSlinky.gameObject;
       // StartCoroutine(CountDownRutine());
    }
    IEnumerator CountDownRutine()
    {
        
        yield return new WaitForSeconds(6);
        PauseGame();
        getPanel.ShowGameOverPanel();

    }
    private void Update()
    {

        slikyController = slinkyPlayer.GetComponent<SlikyController>();
        if(slikyController.isDead )
        {
            saveData();
            StartCoroutine(CountDownRutine());
            
        }
        if (!MainMenuPanel.activeInHierarchy && !IntroductionPanel.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
                getPanel.ShowPause();
            }
        }
        if(IntroductionPanel.activeInHierarchy)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                getPanel.ShowGamePanel();
                ResumeGame();
            }
        }
        if(loadeddata && MainMenuPanel.activeInHierarchy)
        {
            ResetTheGame();
        }
        
       
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
    public void ResetTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ResumeGame();
    }
    public int getGameLevel()
    {
        return level;
    }
    public int getStatusLevel()
    {
        return status;
    }
    public void setGameLevel(int _level)
    {
        level = _level;
       
    }
    public void setStatusLevel(int _status)
    {
        status = _status;
        
    }
    public enum GameLevel
    {
        Level1,
        Level2,
        Fight,
        MainMenu,
        GameOver

    }
}
