using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomchangeup : MonoBehaviour
{
    //float entrance=0;
    [SerializeField] Vector3 room1;
    [SerializeField] Vector3 room2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    /*void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            entrance = col.gameObject.GetComponent<Rigidbody2D>().velocity.y;
            print("Entrance: ");
        }
    }*/

    void OnTriggerExit2D(Collider2D col)
    {
        //print(col.gameObject.tag);
        if(col.gameObject.tag=="Player")
        {
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            if(rb.velocity.y > 0)// && entrance > 0)
            {
                Camera.main.transform.position = room2;
            }
            else if(rb.velocity.y < 0)// && entrance < 0)// && up==1)
            {
                Camera.main.transform.position = room1;
            }
        }
    }
}
