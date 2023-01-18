using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterSpellController : MonoBehaviour
{
    public PlayerController player, player2;
    PlayerController friendly, enemy;
    ManaController mana;

    public int healAmount = 20;

    public void doDamage(int amount, bool caster)
    {
      if(caster)
      {
        friendly = player;
        enemy = player2;
      }
      else
      {
        friendly = player2;
        enemy = player;
      }

      enemy.takeDamage(amount);
    }

    public void castSpell(Spells spell, bool caster)
    {
      if(caster)
      {
        friendly = player;
        enemy = player2;
      }
      else
      {
        friendly = player2;
        enemy = player;
      }

      mana = friendly.GetComponent<ManaController>();

      switch(spell)
      {
        case Spells.Heal:
        {
          Heal();
          break;
        }
        case Spells.Earthquake:
        {
          Earthquake();
          break;
        }
        case Spells.Fireball:
        {
          Fireball();
          break;
        }
        case Spells.Speed:
        {
          Speed();
          break;
        }
        case Spells.Extend:
        {
          Extend();
          break;
        }
        //new spells here
        default:
        {
          break;
        }
      }
    }

    void Heal()
    {
      if(mana.water >= 5)
      {
        mana.DecrementMana(ManaType.Water, 5);
        friendly.takeDamage(healAmount * -1);
        friendly.healSound.Play();
        Debug.Log("Heal");
      }
      else
      {
        Debug.Log("Not Enough Mana");
      }
    }

    void Earthquake()
    {
      if(mana.earth >= 5)
      {
        mana.DecrementMana(ManaType.Earth, 5);
        enemy.GetComponent<PlayerController>().earthquakeSpell();
        friendly.earthquakeSound.Play();
        Debug.Log("Earthquake");
      }
      else
      {
        Debug.Log("Not Enough Mana");
      }
    }

    void Fireball()
    {
      if(mana.fire >= 5)
      {
        mana.DecrementMana(ManaType.Fire, 5);
        friendly.GetComponent<PlayerController>().fireballSpell();
        friendly.fireballSound.Play();
        Debug.Log("Fireball");
      }
      else
      {
        Debug.Log("Not Enough Mana");
      }
    }

    void Speed()
    {
      if(mana.mental >= 5)
      {
        mana.DecrementMana(ManaType.Mental, 5);
        enemy.ball.speedSpell();
        friendly.speedSound.Play();
        Debug.Log("Speed");
      }
      else
      {
        Debug.Log("Not Enough Mana");
      }
    }

    void Extend()
    {
      if(mana.physical >= 5 && !player.extended)
      {
        mana.DecrementMana(ManaType.Physical, 5);
        friendly.GetComponent<PlayerController>().extendSpell();
        friendly.speedSound.Play();
        Debug.Log("Extend");
      }
      else
      {
        Debug.Log("Not Enough Mana");
      }
    }
}
