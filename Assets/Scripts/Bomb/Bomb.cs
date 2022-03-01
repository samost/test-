using System;
using System.Collections.Generic;
using Character;
using Configs;
using Room;
using Unity.VisualScripting;
using UnityEngine;

namespace Bomb
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField]
        private BombsData _bombsData = null;

        private BombConfig _config;
        
        public void Init()
        {
            _config = _bombsData.GetRandomConfig();
            
            GetComponent<MeshRenderer>().material.color = _config.color;
        }

        

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<Ground>() != null)
            {
                Explode();
            }
        }

        private void Explode()
        {
            Collider[] overlapColliders = Physics.OverlapSphere(transform.position, _config.radius);

            int aroundWallCounter = 0;

            List<CharacterBase> characterBases = new List<CharacterBase>();
                
            for (int i = 0; i < overlapColliders.Length; i++)
            {
                if (overlapColliders[i].GetComponent<BaseTerrain>() != null)
                {
                    aroundWallCounter++;
                }

                CharacterBase characterBase = overlapColliders[i].GetComponent<CharacterBase>();

                if (characterBase!= null)
                {
                    characterBases.Add(characterBase);
                }
            }

            foreach (var characterBase in characterBases)
            {
                int damageValue = _config.damage * aroundWallCounter;
                
                characterBase.TakeDamage(damageValue);
            }
            
            Destroy(gameObject, 1);
        }
    }
}