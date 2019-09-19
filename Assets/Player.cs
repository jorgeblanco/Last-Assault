using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In ms^-1")][SerializeField] private float shipSpeed = 30f;
    [SerializeField] private float maxX = 5f;
    [SerializeField] private float maxY = 3f;
    [SerializeField] private float yawFactor = 6f;
    [SerializeField] private float pitchFactor = -4f;
    [SerializeField] private float controlPitchFactor = -40f;
    [SerializeField] private float controlRollFactor = -60f;

    private float _xOffset;
    private float _yOffset;
    private float _horizontalThrow;
    private float _verticalThrow;

    void Start()
    {
    }

    void Update()
    {
        ProcessInput();
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessInput()
    {
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
}
