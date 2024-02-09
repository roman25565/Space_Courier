using System;
using UnityEngine;

public class ColliderEnter : MonoBehaviour
{
    private const string Planet = "planet";
    private const string Asteroid = "Asteroid";

    private GameObject lastHit;
    
    private void OnControllerColliderHit(ControllerColliderHit hitInfo)
    {
        if (hitInfo.gameObject == lastHit)
            return;

        lastHit = hitInfo.gameObject;
        switch (hitInfo.gameObject.tag)
        {
            case Planet:
                EventBus.OnPlanetHit.Invoke(hitInfo.gameObject);
                break;
            case Asteroid:
                EventBus.OnAsteroidHit.Invoke();
                break;
        }
    }
}