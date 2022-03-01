using UnityEngine;
using Random = UnityEngine.Random;

namespace Configs
{
    [CreateAssetMenu(fileName = "BombsData", menuName = "ScriptableObjects/BombsData", order = 1)]
    public class BombsData : ScriptableObject
    {
        [SerializeField]
        private BombConfig[] _bombsConfigs = null;

        public BombConfig GetRandomConfig()
        {
            int rand = Random.Range(0, _bombsConfigs.Length);

            return _bombsConfigs[rand];
        }
    }
}