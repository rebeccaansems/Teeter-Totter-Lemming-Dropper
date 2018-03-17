using System.Collections;
using UnityEngine;

public class BirdMovement : MonoBehaviour {

    public float Speed, EndX;
    //is left or right
    public int DirectionMultiplier;

    private bool hasBeenSetup = false;
    private Vector3 endLocation;

    //Set the information about collectable after spawning
    public void Setup(int color)
    {
        //determines the location where donut will be deleted
        endLocation = new Vector3(EndX * DirectionMultiplier, this.transform.position.y, 0);
        this.GetComponent<SpriteRenderer>().flipX = DirectionMultiplier == 1;
        //set the sprite
        this.GetComponent<Animator>().SetInteger("color", color);
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

    public void KillBird()
    {
        Speed = 0;
        this.GetComponent<Animator>().SetBool("isDying", true);
        Destroy(this.transform.parent.gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 0.1f);
    }
}
