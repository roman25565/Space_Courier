// using UnityEngine;
// using UnityEngine.UI;
// public class QuestPointer : MonoBehaviour {
//
//     [SerializeField] private Camera uiCamera;
//     [SerializeField] private Sprite arrowSprite;
//     [SerializeField] private Sprite crossSprite;
//
//     private Vector3 _targetPosition;
//     private RectTransform _pointerRectTransform;
//     private Image _pointerImage;
//
//     private void Awake() {
//         _pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
//         _pointerImage = transform.Find("Pointer").GetComponent<Image>();
//     }
//
//     private void Update() {
//         float borderSize = 100f;
//         Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(_targetPosition);
//         bool isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;
//
//         if (isOffScreen) {
//             RotatePointerTowardsTargetPosition();
//
//             _pointerImage.sprite = arrowSprite;
//             Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
//             if (cappedTargetScreenPosition.x <= borderSize) cappedTargetScreenPosition.x = borderSize;
//             if (cappedTargetScreenPosition.x >= Screen.width - borderSize) cappedTargetScreenPosition.x = Screen.width - borderSize;
//             if (cappedTargetScreenPosition.y <= borderSize) cappedTargetScreenPosition.y = borderSize;
//             if (cappedTargetScreenPosition.y >= Screen.height - borderSize) cappedTargetScreenPosition.y = Screen.height - borderSize;
//
//             Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
//             _pointerRectTransform.position = pointerWorldPosition;
//             _pointerRectTransform.localPosition = new Vector3(_pointerRectTransform.localPosition.x, _pointerRectTransform.localPosition.y, 0f);
//         } else {
//             _pointerImage.sprite = crossSprite;
//             Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(targetPositionScreenPoint);
//             _pointerRectTransform.position = pointerWorldPosition;
//             _pointerRectTransform.localPosition = new Vector3(_pointerRectTransform.localPosition.x, _pointerRectTransform.localPosition.y, 0f);
//
//             _pointerRectTransform.localEulerAngles = Vector3.zero;
//         }
//     }
//
//     private void RotatePointerTowardsTargetPosition() {
//         Vector3 toPosition = _targetPosition;
//         Vector3 fromPosition = Camera.main.transform.position;
//         fromPosition.z = 0f;
//         Vector3 dir = (toPosition - fromPosition).normalized;
//         float angle = GetAngleFromVectorFloat(dir);
//         _pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
//     }
//
//     private float GetAngleFromVectorFloat(Vector3 dir) {
//         dir = dir.normalized;
//         float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
//         if (n < 0) n += 360;
//
//         return n;
//     }
//     public void Hide() {
//         gameObject.SetActive(false);
//     }
//
//     public void SetTarget(Vector3 targetPosition) {
//         _targetPosition = targetPosition;
//     }
// }
