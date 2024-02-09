using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class Quest : MonoBehaviour
    {
        [SerializeField] private QuestPointer _windowQuestPointer;
        
        [SerializeField] private GameObject arrow;
        [SerializeField] private TextMeshProUGUI quest;

        private GameObject _target;

        private const string ToParcel = "move to the planet to load the parcel";
        private const string ToUnParcel = "move to the planet to load the parcel";
        private void Awake()
        {
            EventBus.UiDirection.AddListener(SetDirection);
        }
        
        // private void Update()
        // {
        //     throw new NotImplementedException();
        // }
        
        private void SetDirection(GameObject target, bool isMovingToLoading)
        {
            _target = target;
            quest.text = isMovingToLoading ? ToParcel : ToUnParcel;
            _windowQuestPointer.SetTarget(_target.transform.position);
        }
    }
}