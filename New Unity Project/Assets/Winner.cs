using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winner : MonoBehaviour
{
    public TimerScript timer;
    public PlayerScript player;
    [SerializeField] GameObject winScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            timer.stopwatchActive = false;
            player.playing = false;
            Instantiate(winScreen);
        }
    }
}
