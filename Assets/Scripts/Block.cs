using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public ManaType manaType = ManaType.Water;
    public ManaType manaType2 = ManaType.None;
    public ManaController mana;
    public PlayerController player;

    public int missDamage = 5;
    public float blockSpeed;
    public int dropSeconds = 5;

    public float quakeSpeed = 1.0f;
    public float quakeAmount = 1.0f;
    public float quakeTime = 3.0f;
    public float quakeDropMod = 3.0f;
    public bool quake = false;

    void Start()
    {
        blockSpeed = 0;
        InvokeRepeating("dropBlocks", 0, dropSeconds);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag != "Bottom")
        {
          mana.IncrementMana(manaType);
          mana.IncrementMana(manaType2);
          player.breakSound.Play();
        }
        else
        {
          player.takeDamage(missDamage);
          player.painSound.Play();
        }
        player.currentBlocks.Remove(gameObject);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag != "Bottom")
        {
          mana.IncrementMana(manaType);
          mana.IncrementMana(manaType2);
          player.breakSound.Play();
        }
        else
        {
          player.takeDamage(missDamage);
          player.painSound.Play();
        }
        player.currentBlocks.Remove(gameObject);
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
      GetComponent<Rigidbody2D>().velocity = Vector2.down * blockSpeed;
    }

    void dropBlocks()
    {
      if(!quake)
      {
        blockSpeed = player.blockSpeed;
        StartCoroutine("stopDrop");
      }
    }

    IEnumerator stopDrop()
    {
      yield return new WaitForSeconds(2);
      blockSpeed = 0;
    }

    public void earthquake()
    {
      quake = true;
      StartCoroutine("quakeShake");
      StartCoroutine("quakeDrop");
    }

    IEnumerator quakeShake()
    {
      while(quake)
      {
        float xOffset = Mathf.PerlinNoise(Time.time * quakeSpeed, 0);
        Vector3 offset = new Vector3(xOffset, 0, 0);
        transform.position += offset * quakeAmount;
        yield return new WaitForSeconds(.1f);
        xOffset = Mathf.PerlinNoise(Time.time * quakeSpeed, 0);
        offset = new Vector3(xOffset, 0, 0);
        transform.position -= offset * quakeAmount;
        yield return new WaitForSeconds(.1f);
      }
    }

    IEnumerator quakeDrop()
    {
      yield return new WaitForSeconds(quakeTime);
      blockSpeed = player.blockSpeed * quakeDropMod;
      StopAllCoroutines();
    }
}
