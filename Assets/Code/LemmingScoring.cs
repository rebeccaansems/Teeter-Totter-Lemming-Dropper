using UnityEngine;

public class LemmingScoring : MonoBehaviour
{
    public PlayerStats Player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if object collected was a donut
        if (collision.transform.tag == "Score")
        {
            //run donut eaten code and destroy object
            //INSERT CODE HERE
        }
        //if object collected was a collectable
        else if (collision.transform.tag == "Collect")
        {
            //run collectable eaten code and destroy object
            Player.AddCollectableEaten(collision.GetComponent<CollectableMovement>().Score, collision.GetComponent<CollectableMovement>().CollectType);
            Destroy(collision.transform.parent.gameObject);
        }
    }
}
