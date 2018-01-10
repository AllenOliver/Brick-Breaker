using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickToLoadAsync : MonoBehaviour
{

    public Slider loadingBar;
    public GameObject loadingImage;


    private AsyncOperation async;


    public void ClickAsync(string levelName)
    {
        loadingImage.SetActive(true);
        StartCoroutine(LoadLevelWithBar(levelName));
    }


    IEnumerator LoadLevelWithBar(string levelName)
    {
        async = SceneManager.LoadSceneAsync(levelName);
        while (!async.isDone)
        {
            loadingBar.value = async.progress;
            yield return null;
        }
    }
}