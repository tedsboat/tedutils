using System;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Agricosmic.Utilities
{
    [Serializable]
    public class AgriRange
    {
        public AgriRange(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
        
        public float min;
        public float max;

        public int RandomInt() => Random.Range(Mathf.FloorToInt(min), Mathf.FloorToInt(max) + 1);
        public float RandomFloat() => Random.Range(min, max);
    }
}