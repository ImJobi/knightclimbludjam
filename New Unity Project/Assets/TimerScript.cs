using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimerScript : MonoBehaviour
{
    public bool stopwatchActive = false;
    float currentTime;
    public Text currentTimeText;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(stopwatchActive==true)
        {
            currentTime = currentTime + Time.deltaTime;
            System.TimeSpan time = System.TimeSpan.FromSeconds(currentTime);
            i++;
            if (i == 6)
            {
                currentTimeText.text = time.ToString(@"hh\:mm\:ss\:ff");
                i = 0;
            }
        }
        else
        {
            if (i != 0)
            {
                System.TimeSpan time = System.TimeSpan.FromSeconds(currentTime);
                currentTimeText.text = time.ToString(@"hh\:mm\:ss\:ff");
                i = 0;
            }
        }
    }
}
