using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingScoring : MonoBehaviour
{
    public PlayerStats Player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Score")
        {
            Player.AddDonutEaten(collision.GetComponent<DonutMovement>().DonutScore, this.transform.position.x < 0);
            Destroy(collision.transform.parent.gameObject);
        }
        else if (collision.transform.tag == "Collect")
        {
            Player.AddCollectableEaten(collision.GetComponent<CollectableMovement>().Score, collision.GetComponent<CollectableMovement>().CollectType);
            Destroy(collision.gameObject);
        }
    }
}
