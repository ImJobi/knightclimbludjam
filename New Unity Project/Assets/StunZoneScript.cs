using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunZoneScript : MonoBehaviour
{
    public PlayerScript player1;
    public AudioClip zap;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = zap;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player1.stun = 2;
            player1.unstuntime = Time.time + 1.5f;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GetComponent<AudioSource>().Play();
            player1.stun = 1;
        }
    }
}
