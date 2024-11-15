using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target; // Hedef pozisyon (örneğin oyuncu)
    [SerializeField] private Vector3 offset; // Kamera ile oyuncu arasındaki mesafe
    [SerializeField] private float followSpeed = 5f; // Kameranın oyuncuyu takip etme hızı

    private void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogError("CameraFollow: Takip edilecek hedef atanmadı!");
            return;
        }

        // Hedef pozisyona doğru smooth hareket
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
    }
}
