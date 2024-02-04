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

    [SerializeField] private float beatScale;

    [SerializeField] private TweenSettings expandTween, contractTween;
    
    void Awake()
    {
        SingletonSetInstance(this, true);
    }

    IEnumerator Start()
    {
        while (true)
        {
            yield return Tween.Scale(transform, Vector3.one * beatScale, expandTween).ToYieldInstruction();
            OnHeartBeat.Invoke();
            yield return Tween.Scale(transform, Vector3.one, contractTween).ToYieldInstruction();
        }
    }
}
