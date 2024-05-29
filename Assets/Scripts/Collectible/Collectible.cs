using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum Type    
    {
        walkInto,
        keyInterract
    }
    public Type interactType;
    [SerializeField] Animator collectablePromptText;
    [SerializeField] GameObject collectVFX;
    [SerializeField] GameObject collectSFX;





    public void SetCollectTextActive(bool active)
    {
        collectablePromptText?.SetBool("isInRange", active);
    }

    public bool Collect()
    {
        OnCollect();
        Destroy(gameObject);
        return true;
    }

    protected virtual void OnCollect()
    {
        Debug.LogWarning("Collectible collected, But No Logic has been added");
    }

    protected virtual void PlayCollectVFX()
    {
        Destroy(Instantiate(collectVFX), 3f);
    }
    protected virtual void PlayCollectSFX()
    {
        Destroy(Instantiate(collectSFX), 3f);
    }
}

