using UnityEngine;

namespace NOC
{
    public static class VectorHelper
    {
        public static Vector2 GetRandom2DDir()
        {
            return new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        }
    }
}
