using System;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject EndGame;

        private void Start()
        {
            EventBus.OnAsteroidHit.AddListener(OnAsteroidHit);
        }

        private void OnAsteroidHit()
        {
            EndGame.SetActive(true);
        }
    }
}
