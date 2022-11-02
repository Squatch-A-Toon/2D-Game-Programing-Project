using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Sprite _midOpen;
    [SerializeField] Sprite _topOpen;

    [SerializeField] SpriteRenderer _midRendererClosed;
    [SerializeField] SpriteRenderer _toprendererClosed;
    [SerializeField] int _requiredCoins= 3;
    [SerializeField] Door _exit;
    [SerializeField] Canvas _canvas;

    bool _open;

    [ContextMenu("OpenDoor")]
    public void OpenDoor()
    {
        if(_canvas!= null)
            _canvas.enabled = false;
        _open = true;
        _midRendererClosed.sprite = _midOpen;
        _toprendererClosed.sprite = _topOpen;
    }

    void Update()
    {
        if (_open == false && Coin.CoinsCollected >= _requiredCoins)
            OpenDoor();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_open == false)
            return;

        var player = collision.GetComponent<Player>();
        if(player != null && _exit != null)
        {
            player.TeleportTo(_exit.transform.position);
        }

    }
}
