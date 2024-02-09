using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MoveController : MonoBehaviour
{
    private Vector3 _targetPosition;
    private Camera _mainCamera;
    
    [SerializeField]private float rotationSpeed = 1.0f;
    [SerializeField]private float moveSpeed = 2.0f;
    
    [SerializeField]private FixedJoystick fixedJoystick;
    private Vector3 _targetDirection;

    private DroneStatus _droneStatus;
    private const float DistanceToStop = 0.1f;
    [SerializeField] private CharacterController characterController;

    private enum DroneStatus
    {
        JoystickInput,
        TouchInput,
        Stop
    }
    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        CheckTouchInput();
        CheckJoystickInput();

        if (_droneStatus == DroneStatus.Stop) return;
        _targetDirection.y = 0;

        RotateAndMove();
    }

    private void CheckTouchInput()
    {
        foreach(Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                SetTargetPosition(touch.position);
            }
        }
        if(_droneStatus == DroneStatus.TouchInput)
        {
            if (Vector3.Distance(_targetPosition, transform.position) < DistanceToStop)
                _droneStatus = DroneStatus.Stop;
        }
        
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(1))
        {
            SetTargetPosition(Input.mousePosition);
        }
#endif
    }
    
    private void CheckJoystickInput()
    {
        if (fixedJoystick.Direction != Vector2.zero)
        {
            _targetDirection = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
            _droneStatus = DroneStatus.JoystickInput;
        }else if (_droneStatus == DroneStatus.JoystickInput)
        {
            _droneStatus = DroneStatus.Stop;
        }
    }
    
    private void RotateAndMove()
    {
        var singleStep = rotationSpeed * Time.deltaTime;
        var newDirection = Vector3.RotateTowards(transform.forward, _targetDirection, singleStep, 1f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        characterController.Move(_targetDirection.normalized * (moveSpeed * Time.deltaTime));
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    private void SetTargetPosition(Vector3 inputCamWorld)
    {
        Physics.Raycast(
            _mainCamera.ScreenPointToRay(inputCamWorld),
            out var ray,
            1000f);
        _targetPosition = ray.point;
        _targetDirection = _targetPosition - transform.position;
        _droneStatus = DroneStatus.TouchInput;
    }
}
