using System;
using System.Collections;
using Room;
using UnityEngine;
using RoomManagers;

namespace Bomb
{
    public class BombManager : MonoBehaviour
    {
        [SerializeField] 
        private Bomb _bombPrefab = null;

        [SerializeField]
        private float _spawnBombDelay = 0;

        [SerializeField] 
        private int _spawnBombHeight = 0;

        private void OnEnable()
        {
            RoomGenerator.TerrainGenerated += RoomGeneratorOnTerrainGenerated;
        }

        private void OnDisable()
        {
            RoomGenerator.TerrainGenerated -= RoomGeneratorOnTerrainGenerated;
        }

        private void RoomGeneratorOnTerrainGenerated(int[,] obj)
        {
            StartCoroutine(SpawnBombRoutine());
        }

        private IEnumerator SpawnBombRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnBombDelay);
                Vector3 position = RoomManager.Instance.GetFreePosition();
                position += Vector3.up * _spawnBombHeight;
                
                var bomb = Instantiate(_bombPrefab, position, Quaternion.identity);
                bomb.Init();
            }
        }
    }
}