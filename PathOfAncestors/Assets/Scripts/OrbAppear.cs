using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OrbAppear : MonoBehaviour
{
    public float velocity = 1;
    float y;
    [SerializeField] private GameObject _endPosition;


    [Header("FALL")]
    [SerializeField] private Ease _fallEase = Ease.InBounce;
    [SerializeField] private float _fallDuration = 0f;

    private Tween _fallTween = null;

    

    void Start()
    {
        StartFallenTween(_fallDuration);
        
    }

    private void StartFallenTween(float duration)
    {
        if (_fallTween != null) _fallTween.Kill();

        _fallTween = transform.DOMove(_endPosition.transform.position, duration);
        _fallTween.SetEase(_fallEase);
    }

    void Update()
    {
        y += velocity;
        transform.rotation = Quaternion.Euler(0, y, 0);
    }

   
}
