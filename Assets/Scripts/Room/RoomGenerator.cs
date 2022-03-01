using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


namespace Room
{
    public class RoomGenerator : MonoBehaviour
    {
        public static event Action<int[,]> TerrainGenerated = null;
        
        private const int MaxChance = 10;
            
        [SerializeField]
        private BaseTerrain _baseTerrainPrefab = null;
        [SerializeField]
        private Vector2 _roomSize = Vector2.zero;

        [SerializeField]
        [Range(0, MaxChance)] 
        private int _generateParam = 0;

        private int[,] _basemMatrix;
        
        private List<BaseTerrain> _baseTerrains = null;

        private void Start()
        {
            GenerateMatrix();
            GenerateTerrain();
            GenerateNavMesh();
        }

        private void GenerateTerrain()
        {
            _baseTerrains = new List<BaseTerrain>();
            
            for (int i = 0; i < _roomSize.x; i++)
            {
                for (int j = 0; j < _roomSize.y; j++)
                {
                    BaseTerrain baseTerrain = null;
                    
                    if (_basemMatrix[i,j] == 1)
                    {
                        baseTerrain = Instantiate(_baseTerrainPrefab, new Vector3(i, 1, j), Quaternion.identity);
                    }
                    else if(_basemMatrix[i,j] == 2)
                    {
                        baseTerrain = Instantiate(_baseTerrainPrefab, new Vector3(i, 1, j), Quaternion.identity);
                        baseTerrain.transform.localScale = new Vector3(1, 3,1);
                    }
                    
                    _baseTerrains.Add(baseTerrain);
                }
            }
            
            TerrainGenerated?.Invoke(_basemMatrix);
        }

        private void GenerateMatrix()
        {
            _basemMatrix = new int[(int) _roomSize.x, (int) _roomSize.y];

            for (int i = 0; i < _roomSize.x; i++)
            {
                for (int j = 0; j < _roomSize.y; j++)
                {
                    if (IsBorderValue(i, j))
                    {
                        _basemMatrix[i, j] = 2;
                    }
                    else
                    {
                        //Random generate
                        int randomValue = Random.Range(0, MaxChance);

                        if (randomValue > _generateParam)
                        {
                            _basemMatrix[i, j] = 1;
                        }
                    }
                }
            }
        }

        private bool IsBorderValue(int i, int j)
        {
            return i == 0 || j == 0 || i == _roomSize.x - 1 || j == _roomSize.y - 1;
        }

        private void GenerateNavMesh()
        {
            var navMesh = GetComponent<NavMeshSurface>();
            navMesh.BuildNavMesh();
        }
    }
}
