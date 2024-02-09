using UnityEngine;

public class Сourier : MonoBehaviour
{
    [SerializeField]private GameObject cargo;
    private bool _hasCargo = false;
    private GameObject _neededPlanet;
    
    private const string Planet = "planet";
    
    private void Start()
    {
        _neededPlanet = GetNearestPlanet();
        EventBus.OnPlanetHit.AddListener(OnPlanetHit);
        EventBus.UiDirection.Invoke(_neededPlanet,_hasCargo);
    }

    private void OnPlanetHit(GameObject planet)
    {
        if(planet != _neededPlanet)
            return;
        
        ChangeState(_hasCargo);
        
        _neededPlanet = _hasCargo ? GetRandomPlanet() : GetNearestPlanet();
        
        EventBus.UiDirection.Invoke(_neededPlanet,_hasCargo);
    }

    private void ChangeState(bool newState)
    {
        _hasCargo = !newState;
        cargo.SetActive(!newState);
    }

    private GameObject GetRandomPlanet()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 50);
        
        var random = new System.Random();
        var randomIndex = random.Next(0, colliders.Length);
        if (colliders[randomIndex].gameObject.CompareTag(Planet) && colliders[randomIndex].gameObject != _neededPlanet)
        {
            return colliders[randomIndex].gameObject;
        }
        return GetRandomPlanet();
    }

    private GameObject GetNearestPlanet()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 100);

        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag(Planet))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);

                if (distance < closestDistance && distance > 10f)
                {
                    closestDistance = distance;
                    closestObject = collider.gameObject;
                }
            }
        }
        return closestObject;
    }
}
