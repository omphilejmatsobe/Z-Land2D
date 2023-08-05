
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class MotionControl : MonoBehaviour
{
    [SerializeField] string parameterName;
    [SerializeField] Camera gameCam;
    [SerializeField] GameObject parent, Knife, Arrow, Fire;
    [SerializeField] float shotSpeed;
    [SerializeField] private AudioSource buttonAudio;
    [SerializeField] Image ImgOne, ImgTwo, ImgThree;
    [SerializeField] Image CheckTwo, CheckThree;
    GameObject weapon;
    Vector2 mouseWorldPosition, mousePosition;
    Vector2 playerMove;
    Vector3 parentTransform, vel;
    Animator animation;
    int state, click;

    private List<GameObject> shots = new List<GameObject>();

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
        weapon = Knife;
        animation = GetComponent<Animator>();
    }


    private void destroyBullet()
    {
        for (int i = 0; i < shots.Count; i++)
        {
            StartCoroutine(WaitForFunction(i));
        }
    }

    IEnumerator WaitForFunction(int i)
    {
        yield return new WaitForSeconds(3);
        Destroy(shots[i]);
    }



    private void UpdateState()
    {

        playerMove.Normalize();

        if (playerMove.x > 0 || click  == 1)
        {
            animation.SetInteger(parameterName, (int)CharStates.walkEast);
            state = (int)CharStates.dirNum + (int)CharStates.walkEast;
        }
        else if (playerMove.x < 0 || click == 2)
        {
            animation.SetInteger(parameterName, (int)CharStates.walkWest);
            state = (int)CharStates.dirNum + (int)CharStates.walkWest;
        }
        else if (playerMove.y > 0 || click == 3)
        {
            animation.SetInteger(parameterName, (int)CharStates.walkNorth);
            state = (int)CharStates.dirNum + (int)CharStates.walkNorth;
        }
        else if (playerMove.y < 0 || click == 4)
        {
            animation.SetInteger(parameterName, (int)CharStates.walkSouth);
            state = (int)CharStates.dirNum + (int)CharStates.walkSouth;
        }
        else if (playerMove.y == 0 && playerMove.x == 0)
        {
            animation.SetInteger(parameterName, state);
        }
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        parentTransform = parent.transform.position;
        parentTransform.z = 0;

        if (Input.GetKeyDown(KeyCode.B) && CheckThree.GetComponent<Image>().enabled == true )
        {
            weapon = Arrow;
            ImgOne.GetComponent<Image>().enabled = true;
            ImgTwo.GetComponent<Image>().enabled = false;
            ImgThree.GetComponent<Image>().enabled = false;
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            weapon = Knife;
            ImgThree.GetComponent<Image>().enabled = true;
            ImgOne.GetComponent<Image>().enabled = false;
            ImgTwo.GetComponent<Image>().enabled = false;
        }
        if (Input.GetKeyUp(KeyCode.G) && CheckTwo.GetComponent<Image>().enabled == true)
        {
            weapon = Fire;
            ImgTwo.GetComponent<Image>().enabled = true;
            ImgThree.GetComponent<Image>().enabled = false;
            ImgOne.GetComponent<Image>().enabled = false;
        }

            if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            buttonAudio.Play();

            if (mouseWorldPosition.x - parentTransform.x >= 0)
            {
                if (mouseWorldPosition.y - parentTransform.y >= 0)
                {
                    click = 3;
                }
                else if (mouseWorldPosition.y - parentTransform.y < 0)
                {
                    click = 1;
                }
            }
            else if (mouseWorldPosition.x - parentTransform.x < 0)
            {
                if (mouseWorldPosition.y - parentTransform.y >= 0)
                {
                    click = 2;
                }
                else if (mouseWorldPosition.x - parentTransform.x < 0)
                {
                    click = 4;
                }
            }


            vel.x = mouseWorldPosition.x - parentTransform.x;
            vel.y = mouseWorldPosition.y - parentTransform.y;
            vel.z = 0f;

            GameObject clone = Instantiate(weapon, parentTransform, Quaternion.identity);

            var rot = Mathf.Atan(vel.y/vel.x);


            rot = (rot * (180 / Mathf.PI)) - 45;

            

            if (vel.y >= 0 && vel.x <= 0)
            {
                rot = 180 + rot;
            }
            else if (vel.y <= 0 && vel.x <= 0)
            {
                rot = 180 + rot;
            }
            else if (vel.y <= 0 && vel.x >= 0)
            {
                rot = 360 + rot;
            }


            clone.transform.rotation = Quaternion.Euler(0, 0, rot);


            clone.GetComponent<Rigidbody2D>().velocity = Vector3.Normalize(vel) * shotSpeed;
            shots.Add(clone);
            destroyBullet();
            UpdateState();
        }
        else
            click = 0;

        

        
        
        
        UpdateState();
    }

    private void FixedUpdate()
    {
        playerMove.x = Input.GetAxis("Horizontal");
        playerMove.y = Input.GetAxis("Vertical");
        playerMove.Normalize();
    }
}