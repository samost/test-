using System;
using Bomb;
using UnityEngine;

namespace Configs
{
    [Serializable]
    public class BombConfig
    {
        public BombType bombType;
        public int damage;
        public Color color;
        public float radius;
    }
}