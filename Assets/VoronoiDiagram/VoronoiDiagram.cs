using UnityEngine;

namespace NOC
{
    public class VoronoiDiagram : MonoBehaviour
    {
        [SerializeField] private Vector2Int voronoiSize = new Vector2Int(512, 512);
        [SerializeField] private int regionAmount = 50;

        private void Start()
        {
            var spriteSR = GetComponent<SpriteRenderer>();
            spriteSR.sprite = Sprite.Create(GetTexture(), new Rect(0, 0, voronoiSize.x, voronoiSize.y), Vector2.one * 0.5f);
        }

        private Texture2D GetTexture()
        {
            Vector2Int[] centroids = new Vector2Int[regionAmount];
            Color[] regions = new Color[regionAmount];
            for (int i = 0; i < regionAmount; ++i)
            {
                centroids[i] = new Vector2Int(Random.Range(0, voronoiSize.x), Random.Range(0, voronoiSize.y));
                regions[i] = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1);
            }

            Color[] pixelColors = new Color[voronoiSize.x * voronoiSize.y];
            for (int y = 0; y < voronoiSize.y; ++y)
            {
                for (int x = 0; x < voronoiSize.x; ++x)
                {
                    int index = y * voronoiSize.y + x;
                    pixelColors[index] = regions[GetClosestCentroidIndex(new Vector2Int(x, y), centroids)];
                }
            }

            return GetTextureFromColorArray(pixelColors);
        }

        private int GetClosestCentroidIndex(Vector2Int pixelPos, Vector2Int[] centroid)
        {
            float smallestDst = float.MaxValue;
            int result = 0;
            for (int i = 0; i < centroid.Length; ++i)
            {
                if (Vector2.Distance(pixelPos, centroid[i]) < smallestDst)
                {
                    smallestDst = Vector2.Distance(pixelPos, centroid[i]);
                    result = i;
                }
            }
            return result;
        }

        private Texture2D GetTextureFromColorArray(Color[] pixelColors)
        {
            Texture2D result = new Texture2D(voronoiSize.x, voronoiSize.y);
            result.filterMode = FilterMode.Point;
            result.SetPixels(pixelColors);
            result.Apply();
            return result;
        }
    }
}
