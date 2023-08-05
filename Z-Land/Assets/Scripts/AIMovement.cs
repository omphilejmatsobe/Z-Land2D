using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    [SerializeField] string parameterName;
    [SerializeField] GameObject shots, enemy;
    [SerializeField] Bound bounds;
    [SerializeField] float speed, shotSpeed;
    Rigidbody2D rb;
    GameObject bound, player;
    Vector2 initialPos, AI;
    Vector2 playerMove;
    int state, counter, Timer;
    Animator animation;
    Vector2 mag;


    enum CharStates
    {
        walkEast = 1,
        walkSouth = 2,
        walkWest = 3,
        walkNorth = 4,
        dirNum = 4
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bound = GameObject.FindGameObjectWithTag("Soil");
        bounds = bound.GetComponent<Bound>();
        rb = GetComponent<Rigidbody2D>();
        AI = this.gameObject.transform.position;
        initialPos = transform.position;
    }

    private void OnTriggerExit(Collider other)
    {
        //MoveTowards,initialPos;
    }
    private void FixedUpdate()
    {
        if (bounds.boundState == true)
        {
            Debug.Log(true);

            AI = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

            rb.MovePosition(AI);

        }
        /*if (bounds.boundState == false)
        {
            this.gameObject.transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }*/

        mag.x = transform.position.x - player.transform.position.x;
        mag.y = transform.position.y - player.transform.position.y;

    }
 

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void UpdateState()
    {
        playerMove.Normalize();

        if (playerMove.x > 0)
        {
            animation.SetInteger(parameterName, (int)CharStates.walkEast);
            state = (int)CharStates.dirNum + (int)CharStates.walkEast;
        }
        else if (playerMove.x < 0)
        {
            animation.SetInteger(parameterName, (int)CharStates.walkWest);
            state = (int)CharStates.dirNum + (int)CharStates.walkWest;
        }
        else if (playerMove.y > 0)
        {
            animation.SetInteger(parameterName, (int)CharStates.walkNorth);
            state = (int)CharStates.dirNum + (int)CharStates.walkNorth;
        }
        else if (playerMove.y < 0)
        {
            animation.SetInteger(parameterName, (int)CharStates.walkSouth);
            state = (int)CharStates.dirNum + (int)CharStates.walkSouth;
        }
        else if (playerMove.y == 0 && playerMove.x == 0)
        {
            animation.SetInteger(parameterName, state);
        }
    }

    
}