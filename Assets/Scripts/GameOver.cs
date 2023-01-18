using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject player1, player2;
    public bool p1win;
    public bool gameEnded = false;
    public GameObject wintext, rematch, bg;

    List<GameObject> allObjects = new List<GameObject>();
    List<GameObject> blocks = new List<GameObject>();
    public float fallSpeed = 1.0f;
    public float killTime = 1.0f;
    public float animateDelay = .5f;

    public MusicController musicController;

    void Update()
    {
        if(!gameEnded)
        {
          if(player1.GetComponent<PlayerController>().health <= 0)
          {
            p1win = false;
            gameOver();
          }

          if(player2.GetComponent<PlayerController>().health <= 0)
          {
            p1win = true;
            gameOver();
          }
        }
    }

    void gameOver()
    {
      gameEnded = true;
      int player = p1win ? 1 : 2;

      bg.SetActive(true);
      wintext.SetActive(true);
      rematch.SetActive(true);

      musicController.gameMusic.Stop();
      musicController.gameOverMusic.Play();

      wintext.GetComponent<TMP_Text>().SetText("Player {0} Wins!", player);
    }

    public void rematchButton()
    {
      GameStart.numReady = 0;
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
