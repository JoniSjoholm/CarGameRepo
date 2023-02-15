using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneTrigger2 : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            SceneManager.LoadScene(0);
        }
    }
}

