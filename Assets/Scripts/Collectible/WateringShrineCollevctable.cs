using System;
using UnityEngine;



public class WateringShrineCollevctable : Collectible
{
    protected override void OnCollect()
    {
        GameSession.Instance.Player.AddWateringCan(-1);
        PlayCollectVFX();
        PlayCollectSFX();
    }
}

