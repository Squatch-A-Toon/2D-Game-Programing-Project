using UnityEngine;

public class UILockable : MonoBehaviour
{
    

    private void OnEnable()
    {
        //Gets Script for StartLevelButton
        var startButton = GetComponent<UIStartLevelButton>();
        //The key value = the level name
        string key = startButton.LevelName + "Unlocked";
        //sets default key value to 0 / False
        int unlocked = PlayerPrefs.GetInt(key, 0);
        //if Unlocked = false
        if (unlocked == 0)
        {
            //Level button is not active
            gameObject.SetActive(false);
        }
    }

    [ContextMenu("ClearUnlocked")]
    void ClearLevelUnlocked()
    {
        var startButton = GetComponent<UIStartLevelButton>();
        string key = startButton.LevelName + "Unlocked";
        //Deletes the information Unlocking levels/ Used for development purposes
        PlayerPrefs.DeleteKey(key);
    }
}
