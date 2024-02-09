using UnityEngine;
using UnityEngine.Events;

public class EventBus : MonoBehaviour
{
    public static UnityEvent OnAsteroidHit = new ();
    public static UnityEvent<GameObject> OnPlanetHit = new ();
    public static UnityEvent<int> OnDocHit = new ();
    public static UnityEvent<GameObject,bool> UiDirection = new ();
}
