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
    private Vector3 _startPositionAux = Vector3.zero;

    public GameObject particles;

    //movable objects that movement is needed
    public bool isDinamic = false;
    public Transform startPos;
    public Transform endPos;

    //stop
    public bool hasToStop = false;
    public bool isStopped = false;
    public Transform stopPos;

    //references to the fmod friction sound instance
    private FMOD.Studio.EventInstance doorSoundInstance;
    private FMOD.Studio.EventInstance platformSoundInstance;
    private FMOD.Studio.EventInstance dirtColumnSoundInstance;

    // Start is called before the first frame update
    void Start()
    {
        switch (gameObject.tag)
        {
            case "Metal":
                platformSoundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Mecanismos/activatePlatform");
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(platformSoundInstance, gameObject.transform, gameObject.GetComponent<Rigidbody>());
                break;
            case "EarthPlatform":
                //dirtColumnSoundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Mecanismos/createDirtColumn");
                //FMODUnity.RuntimeManager.AttachInstanceToGameObject(dirtColumnSoundInstance, gameObject.transform, gameObject.GetComponent<Rigidbody>());
                break;
            case "PartDoor":
                doorSoundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Puerta 1/openStoneDoor");
                //doorSoundInstance.setVolume();
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(doorSoundInstance, gameObject.transform, gameObject.GetComponent<Rigidbody>());
                break;
        }
        
        if (!isDinamic)
        {
            if (_objectToShake == null) _objectToShake = transform;
            _startPosition = transform.position;
            _endPosition += transform.position;
            _startPositionAux = _startPosition;

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

        if(hasToStop)
        {
            _startPosition = stopPos.position;
            
        }
        
        else
        {
            _startPosition = _startPositionAux;
        }

        if(isStopped)
        {
            if(!hasToStop)
            {
                _startPosition = _startPositionAux;

                float _timeElapsed = _closeDoorDuration;
                if (_shakeTween.IsPlaying()) _shakeTween.Pause();
                if (_openTween.IsPlaying())
                {
                    _timeElapsed = _openTween.ElapsedPercentage() * _closeDoorDuration;
                    _openTween.Pause();
                }


                UpdateCloseTween(_timeElapsed);
                _closeTween.Restart();
            }
        }

        //Debug.Log(_openDoorDuration);
        
    }

    private void SetupTweens()
    {
        UpdateOpenTween(_openDoorDuration, true);
        UpdateCloseTween(_closeDoorDuration);
        UpdateShakeTween();
    }

    private void UpdateOpenTween(float duration, bool settingUp = false)
    {
        //play friction sound when going up depending on gameobject tag
        switch (gameObject.tag)
        {
            case "Metal":
                platformSoundInstance.start();
                break;
            case "EarthPlatform":
                //dirtColumnSoundInstance.start();
                break;
            case "PartDoor":
                doorSoundInstance.start();
                break;
        }
        
        if (_openTween != null) _openTween.Kill();
        _openTween = transform.DOMove(_endPosition, duration).Pause().SetEase(_openEase).SetAutoKill(false);
        _openTween.OnComplete(() => {
            //play sound depending on the gameobject tag
            switch (gameObject.tag)
            {
                case "Metal":
                    //stop platform sound when the platform has reached the top
                    platformSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    break;
                case "EarthPlatform":
                    //stop platform sound when the dirt platform is completed
                    //dirtColumnSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    break;
                case "PartDoor":
                    //stop friction sound when the door has reached the ceiling
                    doorSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    //play sound when the door collides with the ceiling
                    FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Puerta 1/stoneWallHitUp",gameObject);
                    break;
            }
            DoShake();
            if (particles != null)
            {
                particles.SetActive(false);
            }
        });

        if(isStopped)
        {
            isStopped = false;
        }
    }

    private void UpdateCloseTween(float duration)
    {
        //play friction sound when going down depending on gameobject tag
        switch (gameObject.tag)
        {
            case "Metal":
                platformSoundInstance.start();
                break;
            case "EarthPlatform":
                //dirtColumnSoundInstance.start();
                break;
            case "PartDoor":
                doorSoundInstance.start();
                break;
        }
        if (_closeTween != null) _closeTween.Kill();
        _closeTween = transform.DOMove(_startPosition, duration);
        _closeTween.SetEase(_closeEase);
        _closeTween.OnComplete(() => {
            //play sound depending on the gameobject tag
            switch (gameObject.tag)
            {
                case "Metal":
                    //stop platform sound when the platform has reached the bottom
                    platformSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    break;
                case "EarthPlatform":
                    //stop platform sound when the dirt platform is gone
                    //dirtColumnSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    break;
                case "PartDoor":
                    //stop friction sound when the door has reached the floor
                    doorSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    //play sound when the door collides with the floor
                    FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Puerta 1/stoneWallHitDown",gameObject);
                    break;
            }
            DoShake();
            if (particles != null)
            {
                particles.SetActive(false);
            }
        });
        _closeTween.SetAutoKill(false);
        _closeTween.Pause();

        if(hasToStop)
        {
            isStopped = true;
        }
        
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Stop")
        {
            hasToStop = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Stop")
        {
            hasToStop = false;
        }
    }
}
