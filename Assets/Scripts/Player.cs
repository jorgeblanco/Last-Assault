using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")][SerializeField] private float shipSpeed = 30f;
    [Header("Screen Position")]
    [SerializeField] private float maxX = 5f;
    [SerializeField] private float maxY = 3f;
    [SerializeField] private float yawFactor = 6f;
    [SerializeField] private float pitchFactor = -4f;
    [Header("Control Throw")]
    [SerializeField] private float controlPitchFactor = -40f;
    [SerializeField] private float controlRollFactor = -60f;
    [Header("Death FX")]
    [SerializeField] private GameObject explosion;

    private float _xOffset;
    private float _yOffset;
    private float _horizontalThrow;
    private float _verticalThrow;
    private bool _controlsDisabled;
    private SceneLoader _sceneLoader;

    private void Start()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
    }

    void Update()
    {
        ProcessInput();
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessInput()
    {
        if(_controlsDisabled) {return;}
        
        _horizontalThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        _verticalThrow = CrossPlatformInputManager.GetAxis("Vertical");
    }

    private void ProcessTranslation()
    {
        var localPosition = transform.localPosition;
        _xOffset = localPosition.x + shipSpeed * _horizontalThrow * Time.deltaTime;
        _yOffset = localPosition.y + shipSpeed * _verticalThrow * Time.deltaTime;
        _xOffset = Mathf.Clamp(_xOffset, -maxX, maxX);
        _yOffset = Mathf.Clamp(_yOffset, -maxY, maxY);
        localPosition = new Vector3(_xOffset, _yOffset, localPosition.z);
        transform.localPosition = localPosition;
    }
    
    private void ProcessRotation()
    {
        transform.localRotation = Quaternion.Euler(
            _yOffset * pitchFactor + _verticalThrow * controlPitchFactor,
            _xOffset * yawFactor,
            _horizontalThrow * controlRollFactor
            );
    }

    public void OnPlayerDeath()
    {
        _controlsDisabled = true;
        explosion.SetActive(true);
        Debug.Log("Controls Disabled");
        if (_sceneLoader != null)
        {
            _sceneLoader.ReloadLevel();
        }
    }
}
