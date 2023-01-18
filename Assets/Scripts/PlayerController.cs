using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool player = true;
    public float speed = 1.0f;

    public int health = 100;
    public TMP_Text hpText;

    public float blockSpeed = 0.05f;
    public float spawnTimer = 30.0f;

    public float spellSeconds = 5.0f;
    public bool extended = false;
    public float earthquakeChance = .4f;

    public ManaController mana;
    public SpellController spells;

    public BallController ball;
    public BlockSpawner blocks;

    public List<GameObject> currentBlocks = new List<GameObject>();

    public AudioSource breakSound;
    public AudioSource painSound;
    public AudioSource bounceSound;

    public AudioSource extendSound;
    public AudioSource healSound;
    public AudioSource fireballSound;
    public AudioSource earthquakeSound;
    public AudioSource speedSound;

    // Start is called before the first frame update
    void Start()
    {
      mana = GetComponent<ManaController>();
      spells = GetComponent<SpellController>();
    }

    void Update()
    {
      if(player)
      {
        if(Input.GetButtonDown("Spell 1"))
        {
          spells.castSpell(0);
        }
        if(Input.GetButtonDown("Spell 2"))
        {
          spells.castSpell(1);
        }
        if(Input.GetButtonDown("Spell 3"))
        {
          spells.castSpell(2);
        }
      }
      else
      {
        if(Input.GetButtonDown("Spell 4"))
        {
          spells.castSpell(0);
        }
        if(Input.GetButtonDown("Spell 5"))
        {
          spells.castSpell(1);
        }
        if(Input.GetButtonDown("Spell 6"))
        {
          spells.castSpell(2);
        }
      }

      hpText.SetText("{0}", health);
    }

    void FixedUpdate()
    {
        if(player)
        {
          GetComponent<Rigidbody2D>().velocity = Vector2.right * Input.GetAxis("Horizontal") * speed;
        }
        else
        {
          GetComponent<Rigidbody2D>().velocity = Vector2.right * Input.GetAxis("NumHorizontal") * speed;
        }
    }

    public void ready()
    {
      blocks.spawnBlocks();
      spells.ready();
      ball.ready();
      StartCoroutine("spawnBlocks");
    }

    public void takeDamage(int amount)
    {
      health -= amount;
    }

    IEnumerator spawnBlocks()
    {
      yield return new WaitForSeconds(spawnTimer);
      blocks.spawnBlocks();
      if(spawnTimer > 5.0f)
      {
        spawnTimer -= blockSpeed;
      }
      else
      {
        spawnTimer = 5.0f;
      }

      if(blockSpeed < 1.0f)
      {
        blockSpeed += (blockSpeed / 2);
      }
      else
      {
        blockSpeed = 1.0f;
      }

      StartCoroutine("spawnBlocks");
    }

    public void fireballSpell()
    {
      ball.fireball(true);
      blocks.fireball = true;
      foreach(GameObject b in currentBlocks)
      {
        b.GetComponent<BoxCollider2D>().isTrigger = true;
      }
      StartCoroutine("resetFireball");
    }

    IEnumerator resetFireball()
    {
      yield return new WaitForSeconds(spellSeconds);
      ball.fireball(false);
      blocks.fireball = false;
      foreach(GameObject b in currentBlocks)
      {
        if(b != null)
        {
          b.GetComponent<BoxCollider2D>().isTrigger = false;
        }
      }
    }

    public void extendSpell()
    {
      extended = true;
      transform.localScale *= new Vector2(2, 1);
      StartCoroutine("resetExtend");
    }

    IEnumerator resetExtend()
    {
      yield return new WaitForSeconds(spellSeconds);
      transform.localScale /= new Vector2(2, 1);
      extended = false;
    }

    public void earthquakeSpell()
    {
      foreach(GameObject b in currentBlocks)
      {
        if(Random.Range(0.0f, 1.0f) > earthquakeChance)
        {
          b.GetComponent<Block>().earthquake();
        }
      }
    }
}
