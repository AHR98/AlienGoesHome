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
    private GameObject slinkyPlayer;
    public GameObject niloPlayer;
    private SlikyController slikyController;
    private Gun slinkyGun;

    private int level = 0;
    private Vector3 positionLoad;

    public void saveData()
    {
        slikyController = slinkyPlayer.GetComponent<SlikyController>();
        slinkyGun = slinkyPlayer.GetComponent<Gun>();

        DataManager.savePlayer(slikyController, slinkyGun, slinkyPlayer);
    }

    public void loadData()
    {
        Quaternion quaternion = new Quaternion();
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

        Debug.Log("Vector3" + positionLoad.ToString());
        slinkyPlayer.GetComponent<CharacterController>().enabled = false;
        slinkyPlayer.transform.position = positionLoad;
        slinkyPlayer.GetComponent<CharacterController>().enabled = true;

        setLoadData();
    }

    private void setLoadData()
    {
        slikyController.setLoadData();
        slinkyGun.setLoadData();
        
    }
    private void Awake()
    {
        if(instance == null)
            instance = this;
        //PauseGame();

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
        else if (!MainMenuPanel.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
                getPanel.ShowPause();
            }
        }
        
       
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
     
    public void setGameLevel(int _level)
    {
        level = _level;
        if(level == 2)
            saveData();
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
