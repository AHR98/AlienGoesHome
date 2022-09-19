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
    private SlikyController slikyController;
    private int level = 0;
    private void Awake()
    {
        if(instance == null)
            instance = this;
    //    PauseGame();

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
