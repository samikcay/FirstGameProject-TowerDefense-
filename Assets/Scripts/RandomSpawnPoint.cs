using UnityEngine;
using UnityEngine.UIElements;

public class RandomSpawnPoint : MonoBehaviour
{
    public Transform outerSquare; // D�� kare (Transform + �l�ek + d�n��)
    public Transform innerSquare; // �� kare (Transform + �l�ek + d�n��)

    // D�� karenin i�inde, i� karenin d���nda bir nokta d�nd�r�r
    public Vector3 GetRandomPointBetweenSquares()
    {
        while (true)
        {
            // D�� karenin local uzay�nda [-0.5, 0.5] aral���nda nokta se�
            Vector3 localPoint = new Vector3(
                Random.Range(-0.5f, 0.5f),
                0f,
                Random.Range(-0.5f, 0.5f)
            );

            // Local noktay� d�� karenin world pozisyonuna �evir (�l�ek+d�n�� dahil)
            Vector3 worldPoint = outerSquare.TransformPoint(
                Vector3.Scale(localPoint, outerSquare.localScale)
            );

            // �� karenin local uzay�nda i�te mi kontrol et
            Vector3 innerLocal = innerSquare.InverseTransformPoint(worldPoint);
            bool insideInner = innerLocal.x >= -0.5f && innerLocal.x <= 0.5f &&
                               innerLocal.z >= -0.5f && innerLocal.z <= 0.5f;

            if (!insideInner)
                return worldPoint;
        }
    }

    // Scene g�r�n�m�nde i� ve d�� kareyi �izer
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

    // Verilen Transform'u merkez+�l�ek+d�n�� ile kare olarak �izer (X-Z d�zleminde)
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

        // Local k��eleri world�e �evir (d�n�� + pozisyon)
        for (int i = 0; i < 4; i++)
            cornersLocal[i] = t.TransformPoint(cornersLocal[i]);

        for (int i = 0; i < 4; i++)
            Gizmos.DrawLine(cornersLocal[i], cornersLocal[(i + 1) % 4]);
    }
}
