using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.z - GameObject.FindGameObjectWithTag("Player").transform.position.z < -10)
        {
            gameObject.SetActive(false);
        }
        
    }
   private void OnTriggerEnter(Collider other) {
        GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerController>().TriggerEnter();
        GamePlayController.SharedInstance.AddScore(1);
    }

    private void OnTriggerExit(Collider other) {
        GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerController>().TriggerExit();
    }
}
