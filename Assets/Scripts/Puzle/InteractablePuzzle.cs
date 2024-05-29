using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePuzzle : MonoBehaviour
{
    [SerializeField]protected bool isActive; 
    public bool CheckIfActiv { get { return isActive; }   }
}
