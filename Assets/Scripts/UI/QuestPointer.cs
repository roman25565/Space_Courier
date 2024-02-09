using UnityEngine;
using UnityEngine.UI;
public class QuestPointer : MonoBehaviour {

    [SerializeField] private Sprite arrowSprite;
    [SerializeField] private Sprite crossSprite;

    private Vector3 _targetPosition;
    private RectTransform _pointerRectTransform;
    private Image _pointerImage;
    
    private const float BorderSize = 100f;

    private void Awake() {
        _pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
        _pointerImage = transform.Find("Pointer").GetComponent<Image>();
    }

    private void Update()
    {
        var targetPositionScreenPoint = Camera.main.WorldToScreenPoint(_targetPosition);
        var isOffScreen = targetPositionScreenPoint.x <= BorderSize || targetPositionScreenPoint.x >= Screen.width - BorderSize || targetPositionScreenPoint.y <= BorderSize || targetPositionScreenPoint.y >= Screen.height - BorderSize;
        if (isOffScreen)
        {
            RotatePointerTowardsTargetPosition();
            
            _pointerImage.sprite = arrowSprite;
            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;

            cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, BorderSize, Screen.width - BorderSize);
            cappedTargetScreenPosition.y = Mathf.Clamp(cappedTargetScreenPosition.y, BorderSize, Screen.height - BorderSize);

            _pointerRectTransform.position = cappedTargetScreenPosition;
            _pointerRectTransform.localPosition = new Vector3(_pointerRectTransform.localPosition.x, _pointerRectTransform.localPosition.y, 0f);
        }
        else
        {
            _pointerImage.sprite = crossSprite;
            // Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(targetPositionScreenPoint);
            _pointerRectTransform.position = targetPositionScreenPoint;
            _pointerRectTransform.localPosition = new Vector3(_pointerRectTransform.localPosition.x,
                _pointerRectTransform.localPosition.y, 0f);

            _pointerRectTransform.localEulerAngles = Vector3.zero;
        }
    }

    private void RotatePointerTowardsTargetPosition()
    {
        var toPosition = _targetPosition;
        var fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        var dir = (toPosition - fromPosition).normalized;
        var angle = GetAngleFromVectorFloat(dir);
        _pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }

    private float GetAngleFromVectorFloat(Vector3 dir) {
         dir = dir.normalized;
         float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
         if (n < 0) n += 360;

         return n;
     }

    public void SetTarget(Vector3 targetPosition) {
        _targetPosition = targetPosition;
    }
}
