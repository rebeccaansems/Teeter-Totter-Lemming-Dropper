using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutMovement : MonoBehaviour
{

    public float Speed, EndX;
    //is left or right
    public int DirectionMultiplier;
    public Sprite[] DonutSprites;

    private bool hasBeenSetup = false;
    private int score;
    private Vector3 endLocation;

    //Set the information about donut after spawning
    public void Setup(int donutSprite, int score)
    {
        //determines the location where donut will be deleted
        endLocation = new Vector3(EndX * DirectionMultiplier, this.transform.position.y, 0);
        //set the sprite
        this.GetComponent<SpriteRenderer>().sprite = DonutSprites[donutSprite];
        //set the score per donut
        this.score = score;
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
                Destroy(this.gameObject);
            }
        }
    }
}
