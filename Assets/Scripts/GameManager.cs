
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

  private int score = 0;

  private float scoreInterval = 0.1f;
  private float lastScoreTime = 0;

  private float deltaTime = 0f;


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
    }

    if (!gameOver && isReverse) {
      scoreText.color = Color.red;
      // 이미지 깜빡거리게 하기
        reverseImage.SetActive(!reverseImage.activeSelf);
    } else {
      scoreText.color = Color.white;
      reverseImage.SetActive(false);
    }

    if (gameOver) {
      lastReverseTime = Time.time;
    }

    Debug.Log(1.0f / deltaTime);
  }

  public void GameOver() {
    Debug.Log("Game Over!");
    gameOver = true;
    EnemyRespawn enemySpawner = FindObjectOfType<EnemyRespawn>();
    if (enemySpawner != null) {
      enemySpawner.StopEnemyRoutine();
    }
    Invoke("ShowGameOverPanel", 0.1f);
  }

  public void Restart() {
    Debug.Log("Restart!");
    gameOver = false;
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
}
