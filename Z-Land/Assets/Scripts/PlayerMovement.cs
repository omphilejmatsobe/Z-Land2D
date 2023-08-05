
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed, getSpeed, speedFactor;
    [SerializeField] GameObject coverObj;

    Vector2 playerMove;
    Rigidbody2D rb;
    float y;
    Vector3 Jump;
    int x;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        getSpeed = speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            coverObj.SetActive(true);
        }

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            coverObj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpFunc();
            transform.position = Jump;
        }
        
    }

    void JumpFunc()
    {
        Vector3 reF;

        reF = transform.position;

        Jump = transform.position;
        y = Jump.y;
        Jump.y = y + 3f;

        x = 0;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump = Vector3.MoveTowards(reF, Jump, 5f * Time.deltaTime);

            
        }

        Jump.y = y;
        StartCoroutine(WaitForFall(Jump));
    }

    IEnumerator WaitForFall(Vector3 jump)
    {
        yield return new WaitForSeconds(3);
        jump = Vector3.MoveTowards(transform.position, jump, 5f * Time.deltaTime);
    }


    private void FixedUpdate()
    {
        playerMove.x = Input.GetAxis("Horizontal");
        playerMove.y = Input.GetAxis("Vertical");
        playerMove.Normalize();

        rb.velocity = playerMove * speed;

        if (Input.GetKey(KeyCode.LeftShift))
            speed = speedFactor * getSpeed;
        else
            speed = getSpeed;
    }
}