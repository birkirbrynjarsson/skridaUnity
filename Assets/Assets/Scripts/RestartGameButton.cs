using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGameButton : MonoBehaviour
{
    public void restartGame()
    {
        GameObject.Find("PlayerGameController").GetComponent<PlayerControllerScript>().restartGame();
    }
}
