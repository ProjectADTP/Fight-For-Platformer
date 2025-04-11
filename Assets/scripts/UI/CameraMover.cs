using UnityEngine;
using Cinemachine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private Player _target;
    
    private float _fixedY;

    private void Awake()
    {
        _fixedY = _virtualCamera.transform.position.y;
    }

    private void LateUpdate()
    {
        if (_virtualCamera != null && _target != null)
        {
            Vector3 cameraPosition = _virtualCamera.transform.position;
            cameraPosition.y = _fixedY;
            cameraPosition.x = _target.transform.position.x;

            _virtualCamera.transform.position = cameraPosition;
        }
    }
}
