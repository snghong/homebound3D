using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ok as long as this is the only script that loads scenes

public class CollisionHandler : MonoBehaviour
{
    AudioSource audioSource;
    [Tooltip("In seconds")] [SerializeField] float levelLoadDelay = 1f;
    [Tooltip("FX prefab on player")] [SerializeField] GameObject deathFX;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] AudioClip Damaged;
    [SerializeField] AudioClip Success;
    
    public int hits = 3;
    GameObject Fader;
    

    private void Awake()
    {
        Fader = GameObject.Find("Fader");
        Fader.GetComponent<FaderScript>();
        audioSource = GetComponent<AudioSource>();

    }


    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            deathFX.SetActive(false);
            StartSuccessSequence();
        }


        else
        {
            if (hits > 1)
            {
                StartCoroutine(ProcessHit());
                processHits();
                //same as hits = hits - 1;
               
            }

            else
            {
                hits--;
                StartDeathSequence();
                deathFX.SetActive(true);
                Invoke("ReloadScene", levelLoadDelay);
            }

           
        }



    }
    void Update()
    {
        OnGUI();
    
    }
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 100), hits.ToString());
    }


    private IEnumerator ProcessHit()
    {
        FaderScript faderScript = Fader.GetComponent<FaderScript>();
        yield return faderScript.FadeOut(1f);
        yield return faderScript.FadeIn(0.5f);
        yield break;
    }

    private void processHits()
    {
        hits--;
        audioSource.PlayOneShot(Damaged);
    }


    private void StartDeathSequence()
    {
        
        SendMessage("OnPlayerDeath");

    }

    private void ReloadScene() // string referenced
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        SceneManager.LoadScene(currentSceneIndex);


    }
    private void StartSuccessSequence()
    {
        audioSource.PlayOneShot(Success);
        successParticles.Play();
        deathFX.SetActive(false);
        Invoke("LoadNextLevel", 0.999f); //parametise time
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);

    }

}
