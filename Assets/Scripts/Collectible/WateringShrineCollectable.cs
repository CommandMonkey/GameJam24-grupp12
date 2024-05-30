using System;
using UnityEngine;



public class WateringShrineCollectable : Collectible
{
    protected override void OnCollect()
    {
        GameSession.Instance.Player.AddWateringCan(-1);
        PlayCollectVFX();
        PlayCollectSFX();
    }
}

