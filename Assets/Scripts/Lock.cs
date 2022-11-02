using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lock : MonoBehaviour
{
    [SerializeField] UnityEvent _onUnlocked;

    public void Unlock()
    {
        Debug.Log("unlocked");
        _onUnlocked.Invoke();
    }
    
}
