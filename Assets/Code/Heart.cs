using System.Collections;
using System.Collections.Generic;
using crass;
using PrimeTween;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Heart : Singleton<Heart>
{
    public UnityEvent OnHeartBeat;
    [SerializeField] private float beatInterval;

    [SerializeField] private Animator animator;
    
    void Awake()
    {
        SingletonSetInstance(this, true);
    }

    IEnumerator Start()
    {
        var wait = new WaitForSeconds(beatInterval);
        while (true)
        {
            yield return wait;
            OnHeartBeat.Invoke();
            animator.SetTrigger("Beat");
        }
    }
}
