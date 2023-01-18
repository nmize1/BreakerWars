using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManaController : MonoBehaviour
{
    public int water = 0;
    public int earth = 0;
    public int fire = 0;
    public int mental = 0;
    public int physical = 0;

    SpellController spell;

    public TMP_Text wText;
    public TMP_Text eText;
    public TMP_Text fText;
    public TMP_Text mText;
    public TMP_Text pText;

    void Start()
    {
      spell = GetComponent<SpellController>();
    }

    void Update()
    {
      wText.SetText("{0}", water);
      eText.SetText("{0}", earth);
      fText.SetText("{0}", fire);
      mText.SetText("{0}", mental);
      pText.SetText("{0}", physical);
    }

    public void IncrementMana(ManaType mana)
    {
      switch(mana)
      {
        case ManaType.Water:
        {
          water++;
          break;
        }
        case ManaType.Earth:
        {
          earth++;
          break;
        }
        case ManaType.Fire:
        {
          fire++;
          break;
        }
        case ManaType.Mental:
        {
          mental++;
          break;
        }
        case ManaType.Physical:
        {
          physical++;
          break;
        }
        case ManaType.Damage:
        {
          spell.doDamage(1);
          break;
        }
        case ManaType.None:
        {
          break;
        }
      }
    }

    public void DecrementMana(ManaType mana, int amount)
    {
      switch(mana)
      {
        case ManaType.Water:
        {
          water = water - amount;
          break;
        }
        case ManaType.Earth:
        {
          earth = earth - amount;
          break;
        }
        case ManaType.Fire:
        {
          fire = fire - amount;
          break;
        }
        case ManaType.Mental:
        {
          mental = mental - amount;
          break;
        }
        case ManaType.Physical:
        {
          physical = physical - amount;
          break;
        }
        case ManaType.None:
        {
          break;
        }
      }
    }
  }
