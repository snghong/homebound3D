using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class nextLevel : MonoBehaviour
{
    [Tooltip("In seconds")] [SerializeField] float levelLoadDelay = 2f;
    private void LoadNextLevel()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        int finalSceneIndex = SceneManager.sceneCountInBuildSettings;

        if (currentSceneIndex == 6)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(nextSceneIndex);
    }

    void Update()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
            Invoke("LoadNextLevel", levelLoadDelay);
        
    }
}
