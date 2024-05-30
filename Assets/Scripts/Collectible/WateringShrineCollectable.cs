using System;
using UnityEngine;



public class WateringShrineCollectable : Collectible
{
    protected override void OnCollect()
    {
        if (GameSession.Instance.Player.AddWateringCan(-1));
        PlayCollectVFX();
        PlayCollectSFX();
    }
}

