using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    Animator mAnimator;

    const string pressLeft = "press_left";
    const string pressRight = "press_right";
    const string pressCrouch = "press_crouch";
    const string holdLeft = "holding_left";
    const string holdRight = "holding_right";
    const string holdSprint = "holding_sprint";

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            mAnimator.SetBool(holdLeft, true);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            mAnimator.SetBool(holdRight, true);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            mAnimator.SetBool(holdSprint, true);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            mAnimator.SetTrigger(pressCrouch);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            mAnimator.SetTrigger(pressLeft);
            mAnimator.SetBool(holdLeft, false);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            mAnimator.SetTrigger(pressRight);
            mAnimator.SetBool(holdRight, false);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            mAnimator.SetBool(holdSprint, false);
        }
    }
}
