using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingMovement : MonoBehaviour
{
    public Vector3 LowerLocation, UpperLocation;
    public LemmingMovement OtherLemming;
    public GameObject TeeterTotter;
    public float FallSpeed, RotationAmount, TeeterRotationAmount;
    public float StartTeeterYPos;

    private Animator anim;
    private bool isFirstTime = true;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    void Update()
    {
        //if player clicks or presses screen and lemming is currently at top
        if (Input.GetMouseButtonDown(0) && Vector3.Distance(transform.position, UpperLocation) < 0.05f)
        {
            StartCoroutine(Fall());
        }
    }

    public IEnumerator Fall()
    {
        anim.SetBool("isJumping", true);
        yield return new WaitForSeconds(0.1f);

        while (this.transform.position.y > StartTeeterYPos)
        {
            float moveStep = FallSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, LowerLocation, moveStep);
            yield return new WaitForFixedUpdate();
        }

        StartCoroutine(OtherLemming.RespondFall());

        anim.SetBool("isJumping", false);

        while (this.transform.position.y > LowerLocation.y)
        {
            TeeterTotter.transform.Rotate(0, 0, TeeterTotter.transform.rotation.x + TeeterRotationAmount);

            float moveStep = FallSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, LowerLocation, moveStep);
            yield return new WaitForFixedUpdate();
        }

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, RotationAmount));
    }

    public IEnumerator RespondFall()
    {
        anim.SetBool("isJumping", true);

        while (this.transform.position.y < UpperLocation.y)
        {
            float moveStep = FallSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, UpperLocation, moveStep);
            yield return new WaitForFixedUpdate();
        }

        anim.SetBool("isJumping", false);

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
}
