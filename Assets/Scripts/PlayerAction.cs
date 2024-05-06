using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    
    public int MoveSpeed = 5;
    public GameManager gameManager;

    Rigidbody2D playerRigidbody;
    Animator animator;

    float h;
    float v;
    bool hDown;
    bool vDown;
    bool hUp;
    bool vUp;

    bool isHorizonMove;
    Vector3 directionVector;
    GameObject scanObject;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // get player input
        gameManager.GetPlayerInput(out h, out v, out hDown, out vDown, out hUp,out vUp);

        // check horizontal move
        if (hDown)
            isHorizonMove = true;
        else if (vDown)
            isHorizonMove = false;
        else if (hUp || vUp)
            isHorizonMove = h != 0;

        // Animation
        //Debug.Log("h : " + h + "/ v : " + v + " ~~ (int)h : " + (int)h + "/ (int)v : " + (int)v);
        if (animator.GetInteger("hAxisRaw") != h)
        {
            animator.SetInteger("hAxisRaw", (int)h);
            animator.SetBool("isChange", true);
        }
        else if (animator.GetInteger("vAxisRaw") != v)
        {
            animator.SetInteger("vAxisRaw", (int)v);
            animator.SetBool("isChange", true);
        }
        else
        {
            animator.SetBool("isChange", false);
        }

        // direction
        if (vDown && v == 1)
        {
            directionVector = Vector3.up;
        }
        else if (vDown && v == -1)
        {
            directionVector = Vector3.down;
        }
        else if (hDown && h == 1)
        {
            directionVector = Vector3.right;
        }
        else if (hDown && h == -1)
        {
            directionVector = Vector3.left;
        }

        // scan object
        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            gameManager.DialogAction(scanObject);
        }
    }

    private void FixedUpdate()
    {
        // move
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2 (0, v);
        playerRigidbody.velocity = moveVec * MoveSpeed;

        // Ray
        Debug.DrawRay(playerRigidbody.position, directionVector * 0.7f, Color.green);
        RaycastHit2D raycastHit = Physics2D.Raycast(
            playerRigidbody.position, 
            directionVector, 0.7f, 
            LayerMask.GetMask("Object")
        );

        if (raycastHit.collider != null)
        {
            scanObject = raycastHit.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
    }
}
