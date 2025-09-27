using UnityEngine;
using UnityEngine.UIElements;

public class RandomSpawnPoint : MonoBehaviour
{
    public Transform outerSquare; // Dýþ kare (Transform + ölçek + dönüþ)
    public Transform innerSquare; // Ýç kare (Transform + ölçek + dönüþ)

    // Dýþ karenin içinde, iç karenin dýþýnda bir nokta döndürür
    public Vector3 GetRandomPointBetweenSquares()
    {
        while (true)
        {
            // Dýþ karenin local uzayýnda [-0.5, 0.5] aralýðýnda nokta seç
            Vector3 localPoint = new Vector3(
                Random.Range(-0.5f, 0.5f),
                0f,
                Random.Range(-0.5f, 0.5f)
            );

            // Local noktayý dýþ karenin world pozisyonuna çevir (ölçek+dönüþ dahil)
            Vector3 worldPoint = outerSquare.TransformPoint(
                Vector3.Scale(localPoint, outerSquare.localScale)
            );

            // Ýç karenin local uzayýnda içte mi kontrol et
            Vector3 innerLocal = innerSquare.InverseTransformPoint(worldPoint);
            bool insideInner = innerLocal.x >= -0.5f && innerLocal.x <= 0.5f &&
                               innerLocal.z >= -0.5f && innerLocal.z <= 0.5f;

            if (!insideInner)
                return worldPoint;
        }
    }

    // Scene görünümünde iç ve dýþ kareyi çizer
    private void OnDrawGizmos()
    {
        if (outerSquare != null)
        {
            Gizmos.color = Color.blue;
            DrawWireSquare(outerSquare);
        }

        if (innerSquare != null)
        {
            Gizmos.color = Color.blue;
            DrawWireSquare(innerSquare);
        }
    }

    // Verilen Transform'u merkez+ölçek+dönüþ ile kare olarak çizer (X-Z düzleminde)
    private void DrawWireSquare(Transform t)
    {
        float hx = t.localScale.x * 0.5f;
        float hz = t.localScale.z * 0.5f;

        Vector3[] cornersLocal =
        {
            new Vector3(-hx, 0f, -hz),
            new Vector3(-hx, 0f,  hz),
            new Vector3( hx, 0f,  hz),
            new Vector3( hx, 0f, -hz)
        };

        // Local köþeleri world’e çevir (dönüþ + pozisyon)
        for (int i = 0; i < 4; i++)
            cornersLocal[i] = t.TransformPoint(cornersLocal[i]);

        for (int i = 0; i < 4; i++)
            Gizmos.DrawLine(cornersLocal[i], cornersLocal[(i + 1) % 4]);
    }
}
