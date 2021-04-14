using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{


    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] ParticleSystem successParticles;
    
    void OnTriggerEnter(Collider collision)
    {


        switch (collision.gameObject.tag)
        {

            case "Finish":
                StartSuccessSequence();
                break;
            default:

                break;

        }

    }
    private void StartSuccessSequence()
    {

   
        successParticles.Play();
        Invoke("LoadNextLevel", levelLoadDelay); //parametise time
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
