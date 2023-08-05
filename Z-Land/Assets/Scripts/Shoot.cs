using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] GameObject shotPrefab;
    [SerializeField] GameObject player;
    [SerializeField] float speed;
    GameObject bound;

    public List<GameObject> shotsFired = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bound = GameObject.FindGameObjectWithTag("Soil");
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Shots();
        }
    }
    private void Shots()
    {

        GameObject clone = Instantiate(shotPrefab, transform.position, Quaternion.identity);

        Vector3 pos;

        pos.x = player.transform.position.x - transform.position.x;
        pos.y = player.transform.position.y - transform.position.y;

        pos.z = 0;

        clone.GetComponent<Rigidbody2D>().velocity = Vector3.Normalize(pos) * speed;
        shotsFired.Add(clone);

        for (int i = 0; i < shotsFired.Count; i++)
        {
            StartCoroutine(DestroyShot(i));
        }

    }

    IEnumerator ShotFire(int i)
    {
        yield return new WaitForSeconds(3);
        GameObject clone = Instantiate(shotPrefab, transform.position, Quaternion.identity);
        Destroy(shotsFired[i]);
    }

    IEnumerator DestroyShot(int i)
    {
        yield return new WaitForSeconds(5);
        GameObject clone = Instantiate(shotPrefab, transform.position, Quaternion.identity);
        Destroy(shotsFired[i]);
    }
}
