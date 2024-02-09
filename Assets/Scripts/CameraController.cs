using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]private Transform target;
    private void Update()
    {
        Vector3 position = target.position;
        transform.position = new Vector3(position.x,30, position.z);

        transform.LookAt(target);
    }
}
