using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private PlayerMovement playerMovement; // PlayerMovement referansı
    private float forwardSpeed; // Sürekli ileri hareket hızı

    private void Awake()
    {
        // PlayerMovement bileşenine referans al
        playerMovement = FindObjectOfType<PlayerMovement>();

        if (playerMovement != null)
        {
            forwardSpeed = playerMovement.ForwardSpeed; // ForwardSpeed'i al
        }
        else
        {
            Debug.LogError("PlayerMovement bulunamadı!");
        }
    }

    void Update()
    {
        // Sürekli ileri hareket
        Vector3 forwardMovement = new Vector3(0, 0, forwardSpeed * Time.deltaTime);
        this.transform.position += forwardMovement;
    }
}
