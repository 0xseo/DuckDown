
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  public static GameManager instance = null;

  [HideInInspector]
  private bool gameOver = false;

  private bool isReverse = false;

  [SerializeField]
  private TextMeshProUGUI scoreText;

  [SerializeField]
  private GameObject gameOverPanel;

  [SerializeField]
  private GameObject reverseImage;

  [SerializeField]
  private float reverseInterval = 8f;
  private float lastReverseTime = 0;
  private int reverseCount = 0;

  private int score = 0;

  private float scoreInterval = 0.1f;
  private float lastScoreTime = 0;

  private float deltaTime = 0f;

  // get 3 images of ui panel and turn into black when lose life
  [SerializeField]
  private Image[] lifeImages;

  private int life = 3;

  [SerializeField]
  private GameObject countdownPanel;

  [SerializeField]
  private TextMeshProUGUI countdownText;

  private float countdownStartTime;
  private bool isCountdownActive = false;



  void Awake()
  {
    if (instance == null) {
      instance = this;
    }
  }

  void Update() 
  {
    deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    Application.targetFrameRate = 60;

    if (!gameOver && Time.time - lastScoreTime > scoreInterval) {
      score++;
      lastScoreTime = Time.time;
      scoreText.SetText(score.ToString());
    }

    if (!gameOver && Time.time - lastReverseTime > reverseInterval) {
      if (score < 70) {
        isReverse = false;
      } else {
        isReverse = !isReverse;
      }
      lastReverseTime = Time.time;

      if (isReverse) {
        countdownPanel.SetActive(false);
      }
    }

    if (!gameOver && Time.time - lastReverseTime > reverseInterval - 3f && !isCountdownActive) {
      countdownPanel.SetActive(true);
      countdownStartTime = Time.time;
      isCountdownActive = true;
    }

    if (!gameOver && isCountdownActive) {
      float countdown = 3f - (Time.time - countdownStartTime);
      if (countdown <= 0) {
        countdownPanel.SetActive(false);
        isCountdownActive = false;
      } else {
        countdownText.SetText(Mathf.CeilToInt(countdown).ToString());
      }
    }

    if (!gameOver && isReverse) {
      scoreText.color = Color.red;
      reverseImage.SetActive(reverseCount % 7 == 0);
      reverseCount++;
    } else {
      scoreText.color = Color.white;
      reverseImage.SetActive(false);
    }

    if (gameOver) {
      lastReverseTime = Time.time;
    }

  }

  public void GameOver() {
    Debug.Log("Game Over!");
    countdownPanel.SetActive(false);
    gameOver = true;
    EnemyRespawn enemySpawner = FindObjectOfType<EnemyRespawn>();
    if (enemySpawner != null) {
      enemySpawner.StopEnemyRoutine();
    }
    foreach (Image lifeImage in lifeImages) {
      lifeImage.color = new Color(0f, 0f, 0f, 0f);
    }
    Invoke("ShowGameOverPanel", 0.1f);
  }

  public void Restart() {
    Debug.Log("Restart!");
    gameOver = false;
    life = 3;
    foreach (Image lifeImage in lifeImages) {
      // set color to FF0000
      lifeImage.color = new Color(1f, 0f, 0f, 1f);
    }
    score = 0;
    scoreText.SetText(score.ToString());
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    lastScoreTime = Time.time;
    lastReverseTime = Time.time;
    isReverse = false;
  }

  public bool IsGameOver() {
    return gameOver;
  }

  public bool IsReverse() {
    return isReverse;
  }

  void ShowGameOverPanel() {
    gameOverPanel.SetActive(true);
  }

  public int GetLife() {
    return life;
  }

  public void LoseLife() {
    life--;
    if (lifeImages[2-life] != null) {
      lifeImages[2-life].color = new Color(0f, 0f, 0f, 1f);
    }
    Debug.Log("Life: " + life);
    if (life <= 0) {
      GameOver();
    }
  }
}