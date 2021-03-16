using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class MovingObject : Activable
{
    [SerializeField] private Vector3 _endPosition;
    [SerializeField] private float _timeStatic = 0f;

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



    private Vector3 _startPosition = Vector3.zero;

    public GameObject particles;

    public bool isActivable = false;
    private IEnumerator _coroutine = null;

    public bool hasToReturn = false;

    // Start is called before the first frame update
    void Start()
    {
        if (isActivable &&!hasToReturn)
        {
            if (_objectToShake == null) _objectToShake = transform;
            _startPosition = transform.position;
            _endPosition += transform.position;

            SetupTweensActivable();
            AssociateActions();

            if (particles != null)
            {
                particles.SetActive(false);
            }
        }
        else if (!isActivable && hasToReturn)
        {
            _startPosition = transform.position;
            _endPosition += transform.position;

            SetupTweens();
            _coroutine = PlayAnimations();
            StartCoroutine(_coroutine);

        }
        else if( isActivable && hasToReturn)
        {
            _startPosition = transform.position;
            _endPosition += transform.position;
            SetupTweensActivable();
            AssociateActions();
           
        }

       

    }
    private IEnumerator PlayAnimations()
    {
        _openTween.Restart();
        yield return _openTween.WaitForCompletion();
        yield return new WaitForSeconds(_timeStatic);
        _closeTween.Restart();
        yield return _closeTween.WaitForCompletion();

        yield return new WaitForSeconds(_timeStatic);
        _coroutine = PlayAnimations();
        StopAllCoroutines();
        StartCoroutine(_coroutine);
    }


    private void SetupTweensActivable()
    {
        UpdateOpenTween(_openDoorDuration, true);
        UpdateCloseTween(_closeDoorDuration);
        UpdateShakeTween();
    }

    private void SetupTweens()
    {
        _openTween = transform.DOMove(_endPosition, _openDoorDuration);
        _openTween.SetEase(_openEase);
        _openTween.SetAutoKill(false);
        _openTween.Pause();

        _closeTween = transform.DOMove(_startPosition, _closeDoorDuration);
        _closeTween.SetEase(_closeEase);
        _closeTween.SetAutoKill(false);
        _closeTween.Pause();
    }

    private void UpdateOpenTween(float duration, bool settingUp = false)
    {
        if (_openTween != null) _openTween.Kill();
        _openTween = transform.DOMove(_endPosition, duration).Pause().SetEase(_openEase).SetAutoKill(false);
        _openTween.OnComplete(() => {
            DoShake();
            if (particles != null)
            {
                particles.SetActive(false);
            }
            if(hasToReturn && _isActivated)
            {
                StartCoroutine(OpenDoor(3f));
                
            }
        });
        
    }

    private void UpdateCloseTween(float duration)
    {
        if (_closeTween != null) _closeTween.Kill();
        _closeTween = transform.DOMove(_startPosition, duration);
        _closeTween.SetEase(_closeEase);
        _closeTween.OnComplete(() => {
            DoShake();
            if (particles != null)
            {
                particles.SetActive(false);
            }
            if (hasToReturn && _isActivated)
            {
                StartCoroutine(CloseDoor(3f));
               
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
        if(!hasToReturn)
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
        if (hasToReturn)
        {
            StopAllCoroutines();
            float _timeElapsed = _openDoorDuration;
            if (_closeTween.IsPlaying())
            {
                _timeElapsed = _closeTween.ElapsedPercentage() * _openDoorDuration;
                _closeTween.Pause();
            }

            _isActivated = !_isActivated;

            UpdateOpenTween(_closeDoorDuration);
            _openTween.Restart();
            //StopAllCoroutines();
            //_coroutine = PlayAnimations();
            //StartCoroutine(_coroutine);
        }

    }

    public override void Deactivate()
    {
        if (!hasToReturn)
        {
            if (particles != null)
            {
                particles.SetActive(true);
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
        

        if(hasToReturn)
        {
            StopAllCoroutines();
            _isActivated = !_isActivated;

            UpdateCloseTween(_closeDoorDuration);
            _closeTween.Restart();
            //StopAllCoroutines();
            //UpdateCloseTween(_closeDoorDuration);
            //_closeTween.Restart();

        }



    }
    private TweenCallback DoShake()
    {
        UpdateShakeTween();
        _shakeTween.Restart();

        return null;
    }

    IEnumerator OpenDoor(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        UpdateCloseTween(_closeDoorDuration);
        _closeTween.Restart();
        _openTween.SetAutoKill(false);
        _openTween.Pause();
    }

    IEnumerator CloseDoor(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        UpdateOpenTween(_openDoorDuration);
        _openTween.Restart();
        _closeTween.SetAutoKill(false);
        _closeTween.Pause();
    }




}
