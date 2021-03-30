using System;
using System.Collections;
using DG.Tweening;
using Levels;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Vector3 objectPosition;
    public Vector3 objectRotation;
    
    private Vector3 _cameraPosition;
    private Quaternion _cameraRotation;
    private GameObject _sandwich;

    private void Start()
    {
        LevelManager.Instance.OnNewLevelStart += SetUpCameraForNewLevel;
        _cameraPosition = transform.position;
        _cameraRotation = transform.rotation;
    }

    public void CameraLookToObject(GameObject obj)
    {
        _sandwich = obj;
        obj.transform.DORotate(objectRotation, 1f).SetEase(Ease.OutBack);
        obj.transform.DOMove(objectPosition, 1f).SetEase(Ease.OutBack);
    }

    private void SetUpCameraForNewLevel()
    {
        if(_sandwich != null)
            DOTween.Kill(_sandwich.transform);
        
        transform.position = _cameraPosition;
        transform.rotation = _cameraRotation;
    }
}