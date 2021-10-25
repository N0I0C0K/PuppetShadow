using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPage : MonoBehaviour
{
    // Start is called before the first frame update
    public string startSceneName = "Zero";
    public void startGame()
    {
        MyGameManager.instance.changeScene(startSceneName);
    }
    public void endGame()
    {
        Application.Quit(0);
    }
}
