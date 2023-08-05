using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject prefab1, prefab2, bunker1, bunker2;

    public List<GameObject> Ghoul = new List<GameObject>();
    public List<GameObject> Alien = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        makeClone(bunker1);
        makeClone(bunker2);
    }

    void makeClone(GameObject Bunker)
    {
        
        for (int i = 0; i < 25; i++)
        {
            var clone = Instantiate(prefab1, Bunker.transform.position, Quaternion.identity);
            var cloneAlien = Instantiate(prefab2, Bunker.transform.position, Quaternion.identity);

            Vector2 AIone = clone.transform.position;
            Vector2 AItwo = cloneAlien.transform.position;

            AIone.x = Random.Range(-20, 20) + AIone.x;
            AIone.y = Random.Range(-20, 20) + AIone.y;
            AItwo.x = Random.Range(-20, 20) + AItwo.x;
            AItwo.y = Random.Range(-20, 20) + AItwo.y;

            Vector2 bunkerPos = Bunker.transform.position;

            Vector2 clampedPosition = new Vector2(Mathf.Clamp(AIone.x, bunkerPos.x - 20f, bunkerPos.x + 50f),
                                               Mathf.Clamp(AIone.y, bunkerPos.y - 30f, bunkerPos.y + 50f));

            Vector2 clampedPositionTwo = new Vector2(Mathf.Clamp(AItwo.x, bunkerPos.x - 30f , bunkerPos.x + 35f),
                                               Mathf.Clamp(AItwo.y, bunkerPos.y - 30f, bunkerPos.y + 40f));

            clone.transform.position = clampedPosition;
            cloneAlien.transform.position= clampedPositionTwo;

            Ghoul.Add(clone);
            Alien.Add(cloneAlien);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Ghoul.Count < 10 || Alien.Count < 10)
        {
            makeClone(bunker1);
            makeClone(bunker2);
        }
    }
}
