using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour
{

    public GameObject loadingImage;

    public void LoadScene(string SceneName)
    {
        loadingImage.SetActive(true);
        SceneManager.LoadScene(SceneName);
    }
}