using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallController : MonoBehaviour
{
    public float speed = 1.0f;

    public float resetHeight = -10.0f;
    public int missDamage = 10;

    public Vector2 startPos;
    public PlayerController player;

    public GameObject fireSprite;

    bool needReset = false;

    public void ready()
    {
      Rigidbody2D rb = GetComponent<Rigidbody2D>();
      rb.velocity = new Vector2(1, -1) * speed;
      startPos = transform.position;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
      Rigidbody2D rb = GetComponent<Rigidbody2D>();
      if(col.gameObject.tag == "Paddle")
      {
        float dir = getHitDirection(transform.position, col.transform.position, col.collider.bounds.size.x);

        Vector2 hitDir = new Vector2(dir, 1).normalized;

        rb.velocity = hitDir * speed;
        player.bounceSound.Play();
      }
      else
      {
        Debug.Log("Using new math");
        float x = rb.velocity.x;
        float y = rb.velocity.y;
        float m = Mathf.Sqrt(x*x + y*y);
        // vx and vy are the normalized velocity (magnitude of 1)
        float vx = x / m;
        float vy = y / m;
        // t is the cosine of the angle between v and n
        float nx = col.GetContact(0).normal.x;
        float ny = col.GetContact(0).normal.y;

        float t = vx * nx + vy * ny;
        if(t == 0)
        {
          t -= .05f;
        }
        Debug.Log(t);
        if(t < 0)
        {
          Vector2 tmp = rb.velocity;
          rb.velocity -= new Vector2(2 * nx * m * t, 2 * ny * m * t);
          Debug.Log("Old: " + tmp + " New: " + rb.velocity);
        }
      }
    }

    float getHitDirection(Vector2 ballPos, Vector2 paddlePos, float paddleWidth)
    {
      float relativePos = ballPos.x - paddlePos.x;

      return relativePos / paddleWidth;
    }

    public void speedSpell()
    {
      speed *= 2;
      StartCoroutine("resetSpeed");
    }

    IEnumerator resetSpeed()
    {
      yield return new WaitForSeconds(player.spellSeconds);
      speed /= 2;
    }

    public void fireball(bool active)
    {
        fireSprite.SetActive(active);
    }

    void Reset()
    {
      GetComponent<Rigidbody2D>().velocity = new Vector2(1, -1) * speed;
      needReset = false;
    }

    void Update()
    {
      if(transform.position.y < resetHeight)
      {
        player.health -= missDamage;
        needReset = true;
      }
      if(needReset)
      {
        //display reset text
        transform.position = startPos;

        GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        if(player.player)
        {
          if(Input.GetButtonDown("Reset"))
          {
              Reset();
          }
        }
        else
        {
          if(Input.GetButtonDown("P2Reset"))
          {
            Reset();
          }
        }
      }
    }
}
