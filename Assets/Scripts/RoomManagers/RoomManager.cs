using System;
using System.Collections.Generic;
using Character;
using Room;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RoomManagers
{
    public class RoomManager : MonoBehaviour
    {
        [SerializeField] 
        private CharacterBase _characterBasePrefab = null;
        
        public static RoomManager Instance = null;
        
        private int[,] _matrix;
        private List<Vector2> _freeMatrixPos;

        void Start()
        {
            Init();
        }

        public Vector3 GetFreePosition()
        {
            int random = Random.Range(0, _freeMatrixPos.Count);
            Vector3 position = new Vector3(_freeMatrixPos[random].x, 1.5f, _freeMatrixPos[random].y);

            return position;
        }

        private void RoomGeneratorOnTerrainGenerated(int[,] terrainMatrix)
        {
            _matrix = terrainMatrix;

            InitializeFreePosition();

            SpawnUnits();
        }

        private void SpawnUnits()
        {
            for (int i = 0; i < 3; i++)
            {
                var unit = Instantiate(_characterBasePrefab, GetFreePosition(), Quaternion.identity);
                unit.Initialize();
            }
        }

        private void InitializeFreePosition()
        {
            _freeMatrixPos = new List<Vector2>();

            int rows = _matrix.GetUpperBound(0) + 1;
            int cols = _matrix.Length / rows;
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (_matrix[i, j] == 0)
                    {
                        _freeMatrixPos.Add(new Vector2(i, j));
                    }
                }
            }
        }

        private void OnEnable()
        {
            RoomGenerator.TerrainGenerated += RoomGeneratorOnTerrainGenerated;
        }

        private void OnDisable()
        {
            RoomGenerator.TerrainGenerated -= RoomGeneratorOnTerrainGenerated;
        }

        private void Init()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance == this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}