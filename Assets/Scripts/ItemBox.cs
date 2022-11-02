using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemBox : HittableFromBelow
{
    [SerializeField] GameObject _itemPrefab;
    [SerializeField] GameObject _item;
    [SerializeField] Vector2 _itemLaunchVelocity;

    bool _used;

    void Start()
    {
        //If there is an item
        if(_item != null)
            //Set it to false
            _item.SetActive(false);
    }

    //can use if used = false and we have an item assigned
    protected override bool CanUse => _used == false;
    protected override void Use()
    {
        _item = Instantiate(_itemPrefab, transform.position + Vector3.up, Quaternion.identity, transform);
        //check if we have no item assigned
        if(_item == null)
            //Do Nothing
            return;
        //run base Use method from HittableFromBelow
        base.Use();
        //Set Used to true
        _used = true;

        //Set item to active in scene
        _item.SetActive(true);
        //Set used to true
        _used = true;
        //Get rigidbody componint from item
        var itemRigidBody = _item.GetComponent<Rigidbody2D>();
        //if there is a rigidbody
        if (itemRigidBody != null)
        {
            //Set it's velocity yo the itemLaunchVelocity
            itemRigidBody.velocity = _itemLaunchVelocity;
        }
    }     
}
