using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    public Spells[] slots = new Spells[3];
    public MasterSpellController msc;
    public PlayerController player;
    public SpellSlotController slotController;

    public bool[] cooldown = new bool[3];
    public Slider[] slotAnimation = new Slider[3];
    public float cooldownTime = 10.0f;
    public float[] cdPercent = new float[3];

    void Start()
    {
      player = GetComponent<PlayerController>();
      for(int i = 0; i < 3; i++)
      {
        cooldown[i] = false;
      }
    }

    public void ready()
    {
      slotController.assigned = slots;
      slotController.updateSlots();
    }

    public void castSpell(int slotNum)
    {
      if(!cooldown[slotNum])
      {
        msc.castSpell(slots[slotNum], player.player);
        StartCoroutine("cd", slotNum);
      }
    }

    public void doDamage(int amount)
    {
      msc.doDamage(amount, player.player);
    }

    IEnumerator cd(int slotNum)
    {
      cooldown[slotNum] = true;
      cdPercent[slotNum] = cooldownTime;
      while(cdPercent[slotNum] > 0)
      {
        yield return new WaitForSeconds(.1f);
        cdPercent[slotNum] = cdPercent[slotNum] - 1.0f;
        slotAnimation[slotNum].value = cdPercent[slotNum] / cooldownTime;
      }
      cooldown[slotNum] = false;
    }
}
