using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameState currentGameState;
    public Transform player;
    public Animator menuCanvas;
    public Animator enemySpawner;
    public TextMeshProUGUI playButtonText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    private static readonly int Close = Animator.StringToHash("Close");
    private static readonly int Open = Animator.StringToHash("Open");
    
    private int score;
    private ObjectPooler objectPooler;
    private Vector2 playerInitialPosition;
    private Quaternion playerInitialRotation;

    public enum GameState
    {
        MENU,
        GAME
    }

    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        objectPooler = ObjectPooler.Instance;
        playerInitialPosition = player.position;
        playerInitialRotation = player.rotation;
        LoadPlayerScore();
    }

    private void LoadPlayerScore()
    {
        scoreText.text = PlayerPrefs.GetInt("LastScore").ToString();
        bestScoreText.text = PlayerPrefs.GetInt("BestScore").ToString();
    }

    public void OnClickStartGame()
    {
        currentGameState = GameState.GAME;
        menuCanvas.SetTrigger(Close);
        score = 0;
        scoreText.text = "0";
        player.SetPositionAndRotation(playerInitialPosition, playerInitialRotation);
    }

    public void GameOver()
    {
        currentGameState = GameState.MENU;
        objectPooler.DisableAllPooledObjects();
        SavePlayerScore();
        playButtonText.text = "Tap to restart";
        menuCanvas.SetTrigger(Open);

    }

    private void SavePlayerScore()
    {
        if (int.Parse(scoreText.text) > int.Parse(bestScoreText.text))
            bestScoreText.text = scoreText.text;
        PlayerPrefs.SetInt("BestScore", int.Parse(bestScoreText.text));
        PlayerPrefs.SetInt("LastScore", int.Parse(scoreText.text));
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString();
        if (score % 50 == 0)
            enemySpawner.speed += 0.2f;
    }

    //Method called in Animation Event
    public void SetTimeScale(int value)
    {
        Time.timeScale = value;
    }
}
