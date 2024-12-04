
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
  public static GameManager instance = null;

  public bool gameOver = false;

  [SerializeField]
  private TextMeshProUGUI scoreText;

  private int score = 0;

  private float scoreInterval = 0.1f;
  private float lastScoreTime = 0;

  void Awake()
  {
    if (instance == null) {
      instance = this;
    }
  }

  void Update() 
  {
    if (!gameOver && Time.time - lastScoreTime > scoreInterval) {
      score++;
      lastScoreTime = Time.time;
      scoreText.SetText(score.ToString());
    }
  }

  public void GameOver() {
    Debug.Log("Game Over!");
    gameOver = true;
  }
}
