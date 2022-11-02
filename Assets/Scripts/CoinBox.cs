using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CoinBox : HittableFromBelow
{
    [SerializeField] int _totalCoins = 3;
    
    private int _coinsRemaining;

    //Can Use if coins remaining is greater than 0
    protected override bool CanUse => _coinsRemaining > 0;


    // Start is called before the first frame update
    void Start()
    {
        //Coins remaining = total coins
        _coinsRemaining = _totalCoins;
    }

    protected override void Use()
    {
        //decrease coins remaining by 1
        _coinsRemaining--;
        //add 1 coin to players coins collected
        Coin.CoinsCollected++;
    }
}
