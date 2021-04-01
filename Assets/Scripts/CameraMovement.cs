using DG.Tweening;
using Levels;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
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
        var sandwichPosition = _sandwich.transform.position;

        var targetPosition = new Vector3(sandwichPosition.x, 1.75f, sandwichPosition.z + 3f);
        var targetRotation = new Vector3(30f, 180f, 0f);
        
        transform.DORotate(targetRotation, 1f).SetEase(Ease.OutBack);
        transform.DOMove(targetPosition, 1f).SetEase(Ease.OutBack);
        
    }

    private void SetUpCameraForNewLevel()
    {
        if(_sandwich != null)
            DOTween.KillAll();
        
        transform.position = _cameraPosition;
        transform.rotation = _cameraRotation;
    }
}