using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    [SerializeField] GameObject intro;
    public TimerScript timer;
    public PlayerScript player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void restartLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void playnow()
    {
        timer.stopwatchActive = true;
        player.playing = true;
        Destroy(intro, .25f);
    }


}
