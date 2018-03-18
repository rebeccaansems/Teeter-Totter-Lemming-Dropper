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
            Player.AddDonutEaten(collision.GetComponent<SpriteRenderer>().sprite, collision.GetComponent<DonutMovement>().DonutScore, this.transform.position.x < 0);
            Destroy(collision.transform.parent.gameObject);
            this.GetComponent<PlayAudio>().PlayRandom(0,5);
        }
        //if object collected was a collectable
        else if (collision.transform.tag == "Collect")
        {
            //run collectable eaten code and destroy object
            Player.AddCollectableEaten(collision.GetComponent<SpriteRenderer>().sprite, collision.GetComponent<CollectableMovement>().Score, collision.GetComponent<CollectableMovement>().CollectType);
            Destroy(collision.transform.parent.gameObject);
            this.GetComponent<PlayAudio>().Play(7);
        }
        //if object collected was a bird
        else if (collision.transform.tag == "Bird")
        {
            //knock out lemming and remove colliders
            collision.transform.GetComponent<BirdMovement>().KillBird();
            StartCoroutine(this.GetComponent<LemmingMovement>().KnockOut());
            this.GetComponent<PlayAudio>().PlayRandom(8, 10);
        }
    }
}
