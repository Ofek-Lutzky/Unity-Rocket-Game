using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandeler : MonoBehaviour
{
    [SerializeField] AudioClip obstacleCrushSound;
    [SerializeField] AudioClip finishSound;
    [SerializeField] ParticleSystem finishParcticle;
    [SerializeField] ParticleSystem crashParcticle;

    AudioSource audioSource; 
    float waitTimeCrush = 1f;
    float waitTimeNextLevel = 2.5f;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision other) {
        if (isTransitioning)
        {
            return;
        }

        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("this Friendly");
                break;
            case "Finish":
                LoadNextLevelInvoke();
                break;
            case "Fuel":
                Debug.Log("You Picked up fuel");
                break;
            default:
                StartCrash();
                break;
        }
    }

    void StartCrash()
    {   
        crashParcticle.Play();
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(obstacleCrushSound);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel",waitTimeCrush);
    }
    void ReloadLevel()
    {   
        isTransitioning = true;
        audioSource.Stop();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevelInvoke()
    {   
        finishParcticle.Play();
        audioSource.PlayOneShot(finishSound);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel",waitTimeNextLevel);
    }
    void LoadNextLevel()
    {   
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentSceneIndex+1;
        if (nextScene == SceneManager.sceneCountInBuildSettings){
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }
    

    
}
