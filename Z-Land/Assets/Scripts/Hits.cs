using System.Collections;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;


public class Hits : MonoBehaviour
{
    //[SerializeField] Image HealthImage;
    [SerializeField] TMP_Text HitText;
    [SerializeField] int x;
    int health;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        x = 0;

        if (collision.gameObject.tag == "Knife")
        {
            x = 20;
            HitText.text = "-" + x.ToString();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Arrow")
        {
            x = 50;
            Destroy(collision.gameObject);
            HitText.text = "-" + x.ToString(); 
        }
        if (collision.gameObject.tag == "Fire")
        {
            x = 100;
            Destroy(collision.gameObject);
            HitText.text = "-" + x.ToString();
        }
        
        health -= x;
    }

    private void Bullet()
    {
       
        StartCoroutine(text());
        HitText.text = "";
    }

    IEnumerator text()
    {
        yield return new WaitForSeconds(2);

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
         HitText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    
}
