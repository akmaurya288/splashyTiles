using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float? pressPosX;
    public float movementSpeed = 10.0f;
    private Animator animator;
    private bool triggered = false;

    private bool gameStarted = false;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    
    void Update()
    {
        if(!gameStarted && !GamePlayController.SharedInstance.scene3Loaded){
            if(Input.GetMouseButtonDown(0)){
                gameStarted = true;
                GamePlayController.SharedInstance.GameStarted();
                animator.SetBool("jump", true);
                #if UNITY_EDITOR
                    pressPosX = Input.mousePosition.x;
                #endif
                
                #if UNITY_ANDROID
                    pressPosX = Input.touches[0].position.x;
                #endif
            }
        }

        if(gameStarted){

            // Forword Movement

            // transform.Translate(transform.position.x, transform.position.y, transform.position.z + 8);
            transform.Translate(transform.forward * movementSpeed * Time.smoothDeltaTime);

            // Side Movement

        if(Input.GetMouseButtonDown(0)){
                #if UNITY_EDITOR
                    pressPosX = Input.mousePosition.x;
                #endif
                
                #if UNITY_ANDROID
                    pressPosX = Input.touches[0].position.x;
                #endif
        }
        else if(Input.GetMouseButtonUp(0)){
                    pressPosX = null;
            }

        if(pressPosX !=null){
            float currentX = Input.mousePosition.x;
            float difference = currentX - pressPosX.Value;
            Move((difference * 0.2f) + transform.position.x);
        }

        if(transform.GetChild(0).transform.position.y < 0.5 && !triggered){
            GameStop();
        }
        }
    }

    public void RestartGame(){
        animator.SetBool("fall", false);
        animator.SetBool("jump", false);
        animator.SetTrigger("restart");
        animator.bodyPosition =  new Vector3(0,0,0);
        transform.position = new Vector3(0,5,0);
        transform.GetChild(0).transform.position = new Vector3(0,0,0);

    }
    private void GameStop(){
        Debug.Log("fall");
        gameStarted=false;
        animator.SetBool("fall", true);
        GamePlayController.SharedInstance.StopGame();
    }

    private void Move(float x)
    {
        transform.position = new Vector3(x*0.05f, transform.position.y, transform.position.z);
    }

    public void TriggerEnter(){
        triggered = true;
    }
    public void TriggerExit(){
        triggered = false;
    }
}
