using System;
using UnityEngine;

public class HittableFromBelow : MonoBehaviour
{
    [SerializeField] protected Sprite _usedSprite;
    private Animator _animator;

    //can use is set to True, from the start
    protected virtual bool CanUse => true;

    private void Awake()
    {
        //Get Animator Component
        _animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //if item can't be used anymore
        if (CanUse == false)
            //Do Nothing
            return;
        //Get collision with Player
        var player = collision.collider.GetComponent<Player>();
        //If there is no player when collided
        if (player == null)
            //Do Nothing
            return;
        //Play animation
        PlayAnimation();
        //Use object
        Use();
        //if item can't be used anymore
        if (CanUse == false)
        {
            //set Sprite to Used sprite
            GetComponent<SpriteRenderer>().sprite = _usedSprite;
        }
    }

    private void PlayAnimation()
    {
        //if there is an animator
        if(_animator != null)
        {
            //set trigger peramiter in animator to Use.
            _animator.SetTrigger("Use");
        }
    }

    protected virtual void Use()
    {
        Debug.Log($"Used {gameObject.name}");
    }
}
