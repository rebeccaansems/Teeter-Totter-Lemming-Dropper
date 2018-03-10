using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutPredictor : MonoBehaviour
{
    public GameObject Donut;
    public Sprite[] DonutSprites;
    public int DirectionMultiplier;

    //Set the information about donut after spawning
    public void Setup(int donutSprite)
    {
        //set the sprite
        this.GetComponent<SpriteRenderer>().sprite = DonutSprites[donutSprite];
    }

    private void Update()
    {
        if (Mathf.Abs(Donut.transform.position.x - this.transform.position.x) < 1)
        {
            Destroy(this.gameObject);
        }
    }
}
