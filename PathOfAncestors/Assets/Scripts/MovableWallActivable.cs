﻿using System.Collections;
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

    public GameObject particles;

    //movable objects that movement is needed
    public bool isDinamic = false;
    public Transform startPos;
    public Transform endPos;

    private GameObject player;
    private SpiritsPassiveAbilities passive;
    private bool openCompleted, closeCompleted;
    private Color ambienceLightColor;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        passive = player.GetComponent<SpiritsPassiveAbilities>();
        openCompleted = false;
        closeCompleted = false;
        ambienceLightColor = RenderSettings.ambientLight;

        if(!isDinamic)
        {
            if (_objectToShake == null) _objectToShake = transform;
            _startPosition = transform.position;
            _endPosition += transform.position;

            SetupTweens();
            AssociateActions();

            if (particles != null)
            {
                particles.SetActive(false);
            }
        }
        else
        {
            AssociateActions();
        }
       
    }

    private void Update()
    {
        if (isDinamic)
        {
            if (_isActivated)
            {
                float step = 3 * Time.deltaTime; // calculate distance to move
                transform.position = Vector3.MoveTowards(transform.position, endPos.position, step);
            }
            else
            {
                float step = 3 * Time.deltaTime; // calculate distance to move
                transform.position = Vector3.MoveTowards(transform.position, startPos.position, step);
            }

            if (transform.position == endPos.position || transform.position == startPos.position)
            {
                if (particles != null)
                {
                    particles.SetActive(false);

                }
            }
        }
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

            RenderSettings.ambientLight = new Color(ambienceLightColor.r, ambienceLightColor.g, ambienceLightColor.b, 1);
            RenderSettings.reflectionIntensity = 1;

            DoShake();
            if (particles != null)
            {
                particles.SetActive(false);
            }
        });
    }

    private void UpdateCloseTween(float duration)
    {
        if (_closeTween != null) _closeTween.Kill();
        _closeTween = transform.DOMove(_startPosition, duration);
        _closeTween.SetEase(_closeEase);
        _closeTween.OnComplete(() => {
            if (passive.inDarkArea)
            {
                RenderSettings.ambientLight = new Color(0,0,0,1);
                RenderSettings.reflectionIntensity = 0;
            }

            DoShake();
            if (particles != null)
            {
                particles.SetActive(false);
            }
        });
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
        openCompleted = false;
        if(!isDinamic)
        {
            if (particles != null)
            {
                particles.SetActive(true);
            }
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
        else
        {
            if (particles != null)
            {
                particles.SetActive(true);
            }
            _isActivated = !_isActivated;
        }
       
    }

    public override void Deactivate()
    {
        closeCompleted = false;
        if(!isDinamic)
        {
            if (particles != null)
            {
                particles.SetActive(true);
            }
            if (!deactivatedNeeded && activatorDeactivate._activated)
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

        else
        {
            if (particles != null)
            {
                particles.SetActive(true);
            }
            _isActivated = !_isActivated;
        }




    }
    private TweenCallback DoShake()
    {
        UpdateShakeTween();
        _shakeTween.Restart();
      
        return null;
    }
}
