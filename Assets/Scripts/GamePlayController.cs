using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController _instance;
    public static GamePlayController SharedInstance { get { return _instance; } }
    public Text dragText;
    public GameObject player;
    public bool isPlaying = false;

    public Text scoreTxt;
    public int score = 0;

    public bool scene3Loaded = false;
    private float lastTilePosition = 0;
private void Awake()
{
     if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

}
private void Start()
{
    for (int i = 0; i < 10; i++)
    {
        SpawnEnemy(new Vector3(0, -0.625f, 5.5f+i*8));
    }
}

    private void FixedUpdate()
    {
        if(isPlaying){
            if(lastTilePosition - player.transform.position.z < 60)
            SpawnEnemy(new Vector3(0, -0.625f, lastTilePosition+ 8));
        }
    }
    public void GameStarted(){
        dragText.enabled = false;
        isPlaying = true;
    }

    public void StopGame(){
        isPlaying = false;
        score = 0;
        scoreTxt.text = score.ToString();
        StartCoroutine(LoadScene3());
    }


    public void RestartGame(){
        dragText.enabled = true;
        StartCoroutine(UnLoadScene3());
        ObjectPooler.SharedInstance.RemoveAllPooledObject();
         for (int i = 0; i < 10; i++)
         {
            SpawnEnemy(new Vector3(0, -0.625f, 5.5f+i*8));
         }
        GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerController>().RestartGame();
    }

    public void SpawnEnemy(Vector3 position){
        float random = UnityEngine.Random.value;
        position = new Vector3(random * 6 , position.y, position.z);
        GameObject gameObject = ObjectPooler.SharedInstance.GetPooledObject("cube");
        if (gameObject != null)
        {
            lastTilePosition = position.z;
            gameObject.transform.position = position;
            gameObject.SetActive(true);
        }
    }

    public void AddScore(int value){
        score += value;
        scoreTxt.text = score.ToString();
    } 
    
    IEnumerator LoadScene3()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        while (!operation.isDone)
        {
            scene3Loaded = true;
            yield return null;
        }
    }
    
    IEnumerator UnLoadScene3()
    {
        AsyncOperation operation = SceneManager.UnloadSceneAsync(2);
        while (!operation.isDone)
        {
            scene3Loaded = false;
            yield return null;
        }

    }
}
