using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LemmingMovement : MonoBehaviour
{
    public Vector3 LowerLocation, UpperLocation;
    public LemmingMovement OtherLemming;
    public GameObject TeeterTotter;
    public Animator FlowerAnimator;
    public PlayerStats Player;

    public float FallSpeed, RotationAmount, TeeterRotationAmount;
    public float StartTeeterYPos;

    private Animator anim;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    void Update()
    {
        //if player presses screen in the game area and input is allowed and lemming is currently at top and lemming is doing idle animation
        if (Input.GetMouseButtonDown(0) && Input.mousePosition.y < 1750 && Player.TouchEnabled
            && Mathf.Abs(transform.position.y - UpperLocation.y) < 0.05f &&
            anim.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Idle"))
        {
            //Lemming starts falling
            StartCoroutine(Fall());
        }
    }

    public IEnumerator Fall()
    {
        //start lemming jumping animation
        anim.SetBool("isJumping", true);
        yield return new WaitForSeconds(0.1f);

        //fall until lemming hits point where teeter totter should move
        while (this.transform.position.y >= StartTeeterYPos)
        {
            float moveStep = FallSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, LowerLocation, moveStep);
            yield return new WaitForFixedUpdate();
        }

        //start other lemming going up
        StartCoroutine(OtherLemming.RespondFall());

        //start lemming finish jumping and then idle animation
        anim.SetBool("isJumping", false);

        this.GetComponent<PlayAudio>().Play(6);
        //fall with teeter totter moving the rest of the way
        while (this.transform.position.y > LowerLocation.y)
        {
            //teeter totter rotate
            TeeterTotter.transform.Rotate(0, 0, TeeterTotter.transform.rotation.x + TeeterRotationAmount);

            //lemming fall
            float moveStep = FallSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, LowerLocation, moveStep);
            yield return new WaitForFixedUpdate();
        }

        //set rotation of lemming so it appears to be on the teeter totter
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, RotationAmount));
    }

    public IEnumerator RespondFall()
    {
        //set flower animation to idle
        FlowerAnimator.SetBool("isLanding", false);

        //start lemming jumping animation
        anim.SetBool("isJumping", true);

        //lemming fall up until on the flower
        while (this.transform.position.y < UpperLocation.y)
        {
            //lemming fall up
            float moveStep = FallSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, UpperLocation, moveStep);
            yield return new WaitForFixedUpdate();
        }

        //start lemming finish jumping and then idle animation
        anim.SetBool("isJumping", false);

        //set rotation of lemming so it appears to be on the flower
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        //set flower animation to lemming is landing
        FlowerAnimator.SetBool("isLanding", true);
        this.GetComponent<PlayAudio>().Play(5);
    }
}
