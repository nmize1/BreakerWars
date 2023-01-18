using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSlotController : MonoBehaviour
{
    public GameObject[] slot;

    public Spells[] assigned;

    //slot order fireball, earthquake, heal, extend, speed

    public void updateSlots()
    {
      for(int i = 0; i < assigned.Length; i++)
      {
        Spells spell = assigned[i];
        switch(spell)
        {
          case Spells.Heal:
          {
            slot[i].GetComponent<Slots>().heal.SetActive(true);
            slot[i].GetComponent<Slots>().earthquake.SetActive(false);
            slot[i].GetComponent<Slots>().fireball.SetActive(false);
            slot[i].GetComponent<Slots>().speed.SetActive(false);
            slot[i].GetComponent<Slots>().extend.SetActive(false);
            break;
          }
          case Spells.Earthquake:
          {
            slot[i].GetComponent<Slots>().heal.SetActive(false);
            slot[i].GetComponent<Slots>().earthquake.SetActive(true);
            slot[i].GetComponent<Slots>().fireball.SetActive(false);
            slot[i].GetComponent<Slots>().speed.SetActive(false);
            slot[i].GetComponent<Slots>().extend.SetActive(false);
            break;
          }
          case Spells.Fireball:
          {
            slot[i].GetComponent<Slots>().heal.SetActive(false);
            slot[i].GetComponent<Slots>().earthquake.SetActive(false);
            slot[i].GetComponent<Slots>().fireball.SetActive(true);
            slot[i].GetComponent<Slots>().speed.SetActive(false);
            slot[i].GetComponent<Slots>().extend.SetActive(false);
            break;
          }
          case Spells.Speed:
          {
            slot[i].GetComponent<Slots>().heal.SetActive(false);
            slot[i].GetComponent<Slots>().earthquake.SetActive(false);
            slot[i].GetComponent<Slots>().fireball.SetActive(false);
            slot[i].GetComponent<Slots>().speed.SetActive(true);
            slot[i].GetComponent<Slots>().extend.SetActive(false);
            break;
          }
          case Spells.Extend:
          {
            slot[i].GetComponent<Slots>().heal.SetActive(false);
            slot[i].GetComponent<Slots>().earthquake.SetActive(false);
            slot[i].GetComponent<Slots>().fireball.SetActive(false);
            slot[i].GetComponent<Slots>().speed.SetActive(false);
            slot[i].GetComponent<Slots>().extend.SetActive(true);
            break;
          }
          //new spells here
          default:
          {
            break;
          }
        }
      }
    }
}
