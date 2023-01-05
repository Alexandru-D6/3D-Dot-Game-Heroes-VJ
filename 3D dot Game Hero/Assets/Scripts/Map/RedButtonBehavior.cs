using Assets.Scripts.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButtonBehavior : MonoBehaviour
{
    [SerializeField] private PuzzleManager manager;
    [SerializeField] private Animator animator;
    [SerializeField] private Collider collider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.Boomerang.ToString())
        {
            animator.SetTrigger("Pressed");
            collider.enabled= false;
            manager.aButtonHaveBeenPressed();
        }
    }

    public void ToIdle()
    {
        animator.SetTrigger("toIdle");
        collider.enabled= true;
    }
}
