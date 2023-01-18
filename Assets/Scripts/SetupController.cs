using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class SetupController : MonoBehaviour
{
    public Spells[] slots = new Spells[3];
    public GameObject[] blocks = new GameObject[5];

    public GameObject[] availableSimpleBlocks;
    public GameObject[] availableComplexBlocks;
    public TMP_Dropdown[] blockChoices;
    public TMP_Dropdown[] spellChoices;
    public Button readyBtn;

    public PlayerController player;

    public void Ready()
    {
      int iter = 0;
      foreach(TMP_Dropdown choice in blockChoices)
      {
        if(iter <= 2)
        {
          blocks[iter] = availableSimpleBlocks[choice.value];
        }
        else
        {
          blocks[iter] = availableComplexBlocks[choice.value];
        }
        choice.interactable = false;
        iter++;
      }

      iter = 0;
      foreach(TMP_Dropdown choice in spellChoices)
      {
        slots[iter] = (Spells)choice.value;
        choice.interactable = false;
        iter++;
      }

      player.blocks.blocks = blocks;
      player.spells.slots = slots;
      readyBtn.interactable = false;
      GameStart.numReady++;
    }
}
