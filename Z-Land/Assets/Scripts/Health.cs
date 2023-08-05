using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] Image healthImg, protectionImg;
    [SerializeField] Image ImgOne, ImgTwo;
    float health, protection;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        protection = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Banana")
        {
            health+= 10f;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Bow")
        {
            Destroy(collision.gameObject);
            ImgOne.GetComponent<Image>().enabled = true;
            
        }

        if (collision.gameObject.tag == "Gun")
        {
            Destroy(collision.gameObject);
            ImgTwo.GetComponent<Image>().enabled = true;
  
        }

        if (collision.gameObject.tag == "EnemyShot")
        {
            if (protection <= 0)
            {
                health = health - 5f;
            }
            else if (protection > 0)
            {
                protection = protection - 20f;
            }

            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Potion")
        {
            protection = protection + 20f;
            Destroy(collision.gameObject);
        }

    }

   
    // Update is called once per frame
    void Update()
    {
        healthImg.fillAmount = health / 100f;
        protectionImg.fillAmount = protection / 100f;

        if (health <= 0)
        {
            SceneManager.LoadScene(3);
        }

        if (health > 100)
        {
            health = 100f;
        }
        if (protection > 100)
        {
            protection = 100f;
        }
        if (protection <= 0)
        {
            protection = 0;
        }
    }
}
