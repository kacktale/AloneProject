using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerEnum
{
    idle,forward,back,left,right,forwardLeft,forwardRight,backLeft,backRight,jump
}

public class AnimSwitch : MonoBehaviour
{
    public Animator anim;
    protected void AnimBoolSwitch(string boolName, bool final)
    {
        anim.SetBool(boolName, final);
    }
}