using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject[] blocks = new GameObject[5];
    public GameObject newBlock;
    public bool fireball = false;

    public void spawnBlocks()
    {
      foreach(Transform child in transform)
      {
        newBlock = Instantiate(blocks[Random.Range(0, blocks.Length)], child.transform.position, Quaternion.identity);
        newBlock.GetComponent<Block>().player = player.GetComponent<PlayerController>();
        newBlock.GetComponent<Block>().mana = player.GetComponent<ManaController>();
        if(fireball)
        {
          newBlock.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        player.GetComponent<PlayerController>().currentBlocks.Add(newBlock);
      }
    }
}
