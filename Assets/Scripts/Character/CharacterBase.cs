using System;
using System.Collections;
using RoomManagers;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


namespace Character
{
    public class CharacterBase : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _hpValueLabel = null;
        [SerializeField]
        private int _healthPoint = 0;
        [SerializeField]
        private NavMeshAgent _navMeshAgent = null;

        [SerializeField]
        private MeshRenderer _meshRenderer;

        
        public void Initialize()
        {
            _hpValueLabel.text = _healthPoint.ToString();
        }
        
        public void TakeDamage(int damageValue)
        {
            _healthPoint -= damageValue;

            if (_healthPoint <= 0)
            {
                Death();
            }
            else
            {
                _hpValueLabel.text = _healthPoint.ToString();
                StartCoroutine(DamageIndicationRoutine());
            }
        }

        private void Death()
        {
            Destroy(gameObject);
        }

        private IEnumerator DamageIndicationRoutine()
        {
            var characterColor = _meshRenderer.material.color;
            _meshRenderer.material.color = Color.red;
            
            yield return new WaitForSeconds(0.3f);

            _meshRenderer.material.color = characterColor;
        }

        private void Move()
        {
            if (_navMeshAgent.velocity == Vector3.zero)
            {
                _navMeshAgent.SetDestination(RoomManager.Instance.GetFreePosition());
            }
        }

        private void Update()
        {
            Move();
        }
    }
}