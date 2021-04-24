using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneControllerScript : MonoBehaviour
{
    public AsyncOperation operation;
    private void Start()
    {
        StartCoroutine(LoadAsynchronously());
        StartCoroutine(WaitAndLoad());
    }
    IEnumerator LoadAsynchronously()
    {
        operation = SceneManager.LoadSceneAsync(1);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            yield return null;
        }
    }
    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("qq");
        operation.allowSceneActivation = true;
    }

}
