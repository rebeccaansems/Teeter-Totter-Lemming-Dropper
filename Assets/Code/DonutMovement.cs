using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutMovement : MonoBehaviour
{

    public float Speed, EndX;
    public int DirectionMultiplier;
    public Sprite[] DonutSprites;

    private bool hasBeenSetup = false;
    private int score;
    private Vector3 endLocation;

    public void Setup(int donutSprite, int score)
    {
        endLocation = new Vector3(EndX * DirectionMultiplier, this.transform.position.y, 0);
        this.GetComponent<SpriteRenderer>().sprite = DonutSprites[donutSprite];
        this.score = score;
        hasBeenSetup = true;
    }

    void Update()
    {
        if (hasBeenSetup)
        {
            float step = Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endLocation, step);

            if (Mathf.Abs(this.transform.position.x - endLocation.x) < 0.1f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
