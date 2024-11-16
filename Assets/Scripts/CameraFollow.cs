using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target; // Takip edilecek hedef
    [SerializeField] private Vector3 offset; // Kamera ile hedef arasındaki mesafe
    [SerializeField] private float followSpeed = 5f; // Kameranın takip hızı
    [SerializeField] private float zoomOutDistance = 5f; // Çarpışma sırasında uzaklaşma mesafesi
    [SerializeField] private float zoomSpeed = 2f; // Kamera zoom hızını kontrol eder

    private Vector3 currentOffset; // Dinamik olarak değiştirilebilir offset
    private bool isZoomedOut = false;

    private void Awake()
    {
        currentOffset = offset; // Başlangıçta normal offset
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogError("CameraFollow: Takip edilecek hedef atanmadı!");
            return;
        }

        Vector3 desiredPosition = target.position + currentOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
    }

    public void ZoomOut()
    {
        if (!isZoomedOut)
        {
            Debug.Log("CameraFollow: Kamera uzaklaşıyor.");
            currentOffset = offset + new Vector3(0, 0, -zoomOutDistance); // Kamera daha geriye gider
            isZoomedOut = true;
        }
    }

    public void ResetCamera()
    {
        if (isZoomedOut)
        {
            Debug.Log("CameraFollow: Kamera eski pozisyonuna dönüyor.");
            currentOffset = offset; // Kamera başlangıç konumuna döner
            isZoomedOut = false;
        }
    }
}
