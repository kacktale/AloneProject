using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AnimSwitch
{
    public CharacterController characterController;
    public PlayerEnum playerState = PlayerEnum.idle;
    public float speed;
    public float jumpForce;
    public float Gravity = -9.81f;
    private Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        CheckInputs(h, v);
        Move(h, v);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) AnimBoolSwitch("InputSprint", true); speed *= 1.5f;
        if (Input.GetKeyUp(KeyCode.LeftShift)) AnimBoolSwitch("InputSprint", false); speed /= 1.5f;

        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (characterController.isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * Gravity);
        }

        velocity.y += Gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    void CheckInputs(float h, float v)
    {
        if (v > 0 && h > 0) playerState = PlayerEnum.forwardLeft;
        else if (v > 0 && h < 0) playerState = PlayerEnum.forwardRight;
        else if (v > 0 && h == 0) playerState = PlayerEnum.forward;
        else if (v < 0 && h > 0) playerState = PlayerEnum.backLeft;
        else if (v < 0 && h < 0) playerState = PlayerEnum.backRight;
        else if (v < 0 && h == 0) playerState = PlayerEnum.back;
        else if (v == 0 && h > 0) playerState = PlayerEnum.left;
        else if(v == 0 && h< 0) playerState = PlayerEnum.right;
        else playerState = PlayerEnum.idle;

        CheckEnum();
    }

    void CheckEnum()
    {
        switch (playerState)
        {
            case PlayerEnum.idle:
                AnimBoolSwitch("InputFoward",false);
                AnimBoolSwitch("InputBackward", false);
                AnimBoolSwitch("InputLeft", false);
                AnimBoolSwitch("InputRight", false);
            break;
            case PlayerEnum.forward:
                AnimBoolSwitch("InputFoward", true);
                AnimBoolSwitch("InputBackward", false);
                AnimBoolSwitch("InputLeft", false);
                AnimBoolSwitch("InputRight", false);
            break;
            case PlayerEnum.forwardLeft:
                AnimBoolSwitch("InputFoward", true);
                AnimBoolSwitch("InputBackward", false);
                AnimBoolSwitch("InputLeft", true);
                AnimBoolSwitch("InputRight", false);
                break;
            case PlayerEnum.forwardRight:
                AnimBoolSwitch("InputFoward", true);
                AnimBoolSwitch("InputBackward", false);
                AnimBoolSwitch("InputLeft", false);
                AnimBoolSwitch("InputRight", true);
                break;
            case PlayerEnum.back:
                AnimBoolSwitch("InputFoward", false);
                AnimBoolSwitch("InputBackward", true);
                AnimBoolSwitch("InputLeft", false);
                AnimBoolSwitch("InputRight", false);
                break;
            case PlayerEnum.backLeft:
                AnimBoolSwitch("InputFoward", false);
                AnimBoolSwitch("InputBackward", true);
                AnimBoolSwitch("InputLeft", true);
                AnimBoolSwitch("InputRight", false);
                break;
            case PlayerEnum.backRight:
                AnimBoolSwitch("InputFoward", false);
                AnimBoolSwitch("InputBackward", true);
                AnimBoolSwitch("InputLeft", false);
                AnimBoolSwitch("InputRight", true);
                break;
            case PlayerEnum.left:
                AnimBoolSwitch("InputFoward", false);
                AnimBoolSwitch("InputBackward", false);
                AnimBoolSwitch("InputLeft", true);
                AnimBoolSwitch("InputRight", false);
                break;
            case PlayerEnum.right:
                AnimBoolSwitch("InputFoward", false);
                AnimBoolSwitch("InputBackward", false);
                AnimBoolSwitch("InputLeft", false);
                AnimBoolSwitch("InputRight", true);
                break;
        }
    }

    void Move(float h, float v)
    {
        Vector3 pos = new Vector3(h,0, v);
        pos = transform.TransformDirection(pos);
        characterController.Move(pos * speed * Time.deltaTime);
    }
}
