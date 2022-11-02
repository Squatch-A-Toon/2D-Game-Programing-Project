using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.Events;
using System;

public class Collector : MonoBehaviour
{
    [SerializeField] List<Collectible> _collectibles;
    [SerializeField] UnityEvent _onCollectionComplete;
   
    TMP_Text _remainingText;

    int _countCollected;

    // Start is called before the first frame update
    void Start()
    {
        _remainingText = GetComponentInChildren<TMP_Text>();

        foreach (var collectible in _collectibles)
        {
            collectible.OnPickedUp += Collectible_OnPickedUp; ;
        }
        int countRemaining = _collectibles.Count - _countCollected;

        _remainingText?.SetText(countRemaining.ToString());
    }

    
    // Update is called once per frame
    public void Collectible_OnPickedUp()
    {
        _countCollected++;
        int countRemaining = _collectibles.Count - _countCollected;

        _remainingText?.SetText(countRemaining.ToString());

        if (countRemaining > 0)
            return;

        Debug.Log("Got All Gems");
        _onCollectionComplete.Invoke();
    }

    private void OnValidate()
    {
        _collectibles = _collectibles.Distinct().ToList();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        foreach (var collectible in _collectibles)
        {
            Gizmos.DrawLine(transform.position, collectible.transform.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        foreach (var collectible in _collectibles)
        {
            Gizmos.DrawLine(transform.position, collectible.transform.position);
        }
    }
}
