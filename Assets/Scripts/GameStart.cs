using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public static int numReady = 0;
    public PlayerController player1, player2;
    public GameObject readyCanvas;
    public MusicController musicController;

    public GameObject credits, spellInfo, P1c, P2c;

    void Update()
    {
      if(numReady >= 2)
      {
        player1.ready();
        player2.ready();
        musicController.pregameMusic.Stop();
        musicController.gameMusic.Play();
        P1c.SetActive(true);
        P2c.SetActive(true);
        readyCanvas.SetActive(false);
      }
    }

    public void showCredits()
    {
      credits.SetActive(true);
    }

    public void hideCredits()
    {
      credits.SetActive(false);
    }

    public void showSpellInfo()
    {
      spellInfo.SetActive(true);
    }

    public void hideSpellInfo()
    {
      spellInfo.SetActive(false);
    }

    public void quit()
    {
      Application.Quit();
    }
}
