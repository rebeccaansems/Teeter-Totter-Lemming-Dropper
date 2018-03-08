using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableMovement : MonoBehaviour
{
    public float Speed, EndX;
    //is left or right
    public int DirectionMultiplier, Score, CollectType;
    public Sprite[] CollectSprites;

    private bool hasBeenSetup = false;
    private Vector3 endLocation;

    //Set the information about collectable after spawning
    public void Setup(int collectType)
    {
        //determines the location where donut will be deleted
        endLocation = new Vector3(EndX * DirectionMultiplier, this.transform.position.y, 0);
        //set the sprite
        this.GetComponent<SpriteRenderer>().sprite = CollectSprites[collectType];
        //set the type
        CollectType = collectType;
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
