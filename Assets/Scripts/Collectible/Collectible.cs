using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public virtual void OnCollect()
    {
        Debug.Log("Collectible collected");
    }
    public bool Collect()
    {
        OnCollect();
        Destroy(gameObject);
        return true;
    }
}
