using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    [SerializeField] string _sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //player = a collision involving the player
        var player = collision.GetComponent<Player>();

        //If there is no player interaction
        if (player == null)
            //Do nothing
            return;

        //play Flag wave
        var animator = GetComponent<Animator>();
        animator.SetTrigger("Raise");

        StartCoroutine(LoadAfterDelay());
        //Load new level
        
    }

    IEnumerator LoadAfterDelay()
    {
        //Unlock next level
        PlayerPrefs.SetInt(_sceneName + "Unlocked", 1);
        //Wait for  2 seconds
        yield return new WaitForSeconds(2f);
        //Load next scene
        SceneManager.LoadScene(_sceneName);
    }
}
