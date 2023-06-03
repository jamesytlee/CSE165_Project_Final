using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCreator : MonoBehaviour
{
    public void OnCreateSceneButtonClick()
    {
        // Create a new scene using SceneManager
        SceneManager.LoadScene("NewSceneName");
    }
}
