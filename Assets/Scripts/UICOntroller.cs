using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICOntroller : MonoBehaviour
{
    public void Replay(){
        GamePlayController.SharedInstance.RestartGame();
    }

    public void Exit(){
        Application.Quit();
    }
}
