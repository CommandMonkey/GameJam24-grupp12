using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Vine : MonoBehaviour
{
    public Animator animator;
    [SerializeField]AnimationClip clip;
    [SerializeField] bool grow;
    public bool Grow { get { return grow; } set { grow = value; } }
    int growthLevel;
    [SerializeField]float[] Length;
    private void Start()
    {
        //Length = clip.length;
        print(clip.length);
        print(clip.length/4);
        animator.speed = 0;

    }

    private void Update()
    {
        if (grow)
        {
            grow = false;
            StartCoroutine(Grows(growthLevel));
            growthLevel++;
        }
    }
   
    IEnumerator Grows(int i)
    {
        animator.speed = 1;
        yield return new WaitForSeconds(Length[i]);
        animator.speed = 0;
    }
}