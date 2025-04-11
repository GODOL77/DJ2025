using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
  public static GameManager instance = null;

  public bool isGameOver = false;
  public TMP_Text scoreText = null;
  public GameObject gameOverUI = null;

  int score = 0;

  void Awake()
  {
    instance = this;
  }

  void Update()
  {
    if (isGameOver && Input.GetMouseButton(0))
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
  }

  public void AddScore(int newScore)
  {
    if (!isGameOver)
    {
      score += newScore;
      scoreText.text = "Score : " + score;
    }
  }
  public void OnPlayerDead()
  {
    isGameOver = true;
    gameOverUI.SetActive(true);
  }
}
