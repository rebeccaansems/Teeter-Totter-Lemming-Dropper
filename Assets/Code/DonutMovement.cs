using UnityEngine;

public class DonutMovement : MonoBehaviour
{
    public float Speed;
    //is left or right
    public int DirectionMultiplier, DonutScore;
    public Sprite[] DonutSprites;

    private bool hasBeenSetup = false;
    private float endX;
    private Vector3 endLocation;

    //Set the information about donut after spawning
    public void Setup(int donutSprite, int score)
    {
        endX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x + 2;
        //determines the location where donut will be deleted
        endLocation = new Vector3(endX * DirectionMultiplier, this.transform.position.y, 0);
        //set the sprite
        this.GetComponent<SpriteRenderer>().sprite = DonutSprites[donutSprite];
        //set the score per donut
        DonutScore = score;
        //setup has been complete
        hasBeenSetup = true;
    }

    void Update()
    {
        //has setup been completed
        if (hasBeenSetup)
        {
            //move donut (left or right dependent on spawning location)
            float step = Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endLocation, step);

            //if close enough to end zone = delete
            if (Mathf.Abs(this.transform.position.x - endLocation.x) < 0.1f)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
