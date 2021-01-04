using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class MovableWallActivable : Activable
{

    [SerializeField] private Vector3 _endPosition;

    [Header("OPEN")]
    [SerializeField] private Ease _openEase = Ease.InOutSine;
    [SerializeField] private float _openDoorDuration = 0f;

    [Header("CLOSE")]
    [SerializeField] private Ease _closeEase = Ease.InOutSine;
    [SerializeField] private float _closeDoorDuration = 0f;

    [Header("SHAKE")]
    [SerializeField] private Transform _objectToShake = null;
    [SerializeField] private float _shakeDoorDuration = 0f;
    [SerializeField] private float _shakeDoorStrength = 0f;

    private Tween _openTween = null;
    private Tween _closeTween = null;
    private Tween _shakeTween = null;

    public bool deactivatedNeeded=true;
    public Activator activatorDeactivate;


    private Vector3 _startPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

        if (_objectToShake == null) _objectToShake = transform;
        _startPosition = transform.position;
        _endPosition += transform.position;

        SetupTweens();
        AssociateActions();
    }

    private void SetupTweens()
    {
        UpdateOpenTween(_openDoorDuration, true);
        UpdateCloseTween(_closeDoorDuration);
        UpdateShakeTween();
    }

    private void UpdateOpenTween(float duration, bool settingUp = false)
    {
        if (_openTween != null) _openTween.Kill();
        _openTween = transform.DOMove(_endPosition, duration).Pause().SetEase(_openEase).SetAutoKill(false);
        _openTween.OnComplete(() => {
            DoShake();
        });
    }

    private void UpdateCloseTween(float duration)
    {
        if (_closeTween != null) _closeTween.Kill();
        _closeTween = transform.DOMove(_startPosition, duration);
        _closeTween.SetEase(_closeEase);
        _closeTween.OnComplete(() => DoShake());
        _closeTween.SetAutoKill(false);
        _closeTween.Pause();
    }

    private void UpdateShakeTween()
    {
        if (_shakeTween != null) _shakeTween.Kill();
        _shakeTween = _objectToShake.DOShakePosition(_shakeDoorDuration, _shakeDoorStrength);
        _shakeTween.SetAutoKill(false);
        _shakeTween.Pause();
    }

    public override void Activate()
    {
        if (!canActivate) return;
        if (_isActivated) return;
        float _timeElapsed = _openDoorDuration;

        if (_shakeTween.IsPlaying()) _shakeTween.Pause();

        if (_closeTween.IsPlaying())
        {
            _timeElapsed = _closeTween.ElapsedPercentage() * _openDoorDuration;
            _closeTween.Pause();
        }

        _isActivated = !_isActivated;

        UpdateOpenTween(_timeElapsed);
        _openTween.Restart();
    }

    public override void Deactivate()
    {

        if(!deactivatedNeeded && activatorDeactivate._activated)
        {
            Debug.Log("g");
        }

        else
        {
            if (!canActivate) return;
            if (!_isActivated) return;
            float _timeElapsed = _closeDoorDuration;
            if (_shakeTween.IsPlaying()) _shakeTween.Pause();
            if (_openTween.IsPlaying())
            {
                _timeElapsed = _openTween.ElapsedPercentage() * _closeDoorDuration;
                _openTween.Pause();
            }

            _isActivated = !_isActivated;

            UpdateCloseTween(_timeElapsed);
            _closeTween.Restart();
        }

        

    }
    private TweenCallback DoShake()
    {
        UpdateShakeTween();
        _shakeTween.Restart();
      
        return null;
    }
}
