using UnityEngine;

public class SlideState : IPlayerState
{
    private float originalYPosition; // Oyuncunun başlangıç Y pozisyonu
    private float slideYPosition = -0.678f; // Slide sırasında ayarlanacak Y pozisyonu
    private float originalAngle; // Oyuncunun başlangıç açısı
    private float slideAngle = 90f; // Slide sırasında ayarlanacak açı
    private Vector3 originalCenterOfMass; // Rigidbody'nin orijinal merkez noktası
    private Vector3 slideCenterOfMass = new Vector3(0, 2.09f, 0); // Slide sırasında merkez

    public void EnterState(PlayerStateMachine player)
    {
        player.Animator.SetTrigger("Slide");
        Debug.Log("PlayerStateMachine: Entered SlideState");

        // Oyuncunun orijinal Y pozisyonunu kaydet
        originalYPosition = player.transform.position.y;

        // Oyuncunun orijinal açısını kaydet
        originalAngle = player.transform.eulerAngles.y;

        // Rigidbody'nin orijinal merkezini kaydet
        originalCenterOfMass = player.Rigidbody.centerOfMass;

        // Oyuncunun Y pozisyonunu slide pozisyonuna ayarla
        Vector3 newPosition = player.transform.position;
        newPosition.y = slideYPosition;
        player.transform.position = newPosition;

        // Oyuncunun açısını slide açısına ayarla
        Vector3 newRotation = player.transform.eulerAngles;
        newRotation.y = slideAngle;
        player.transform.eulerAngles = newRotation;

        // Rigidbody'nin merkezini slide pozisyonuna ayarla
        player.Rigidbody.centerOfMass = slideCenterOfMass;
    }

    public void UpdateState(PlayerStateMachine player)
    {
        var stateInfo = player.Animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Slide") && stateInfo.normalizedTime >= 1.0f)
        {
            Debug.Log("SlideState: Animation finished. Restoring original settings.");

            // Orijinal Y pozisyonuna dön
            Vector3 newPosition = player.transform.position;
            newPosition.y = originalYPosition;
            player.transform.position = newPosition;

            // Orijinal açıya dön
            Vector3 newRotation = player.transform.eulerAngles;
            newRotation.y = originalAngle;
            player.transform.eulerAngles = newRotation;

            // Rigidbody'nin orijinal merkezine dön
            player.Rigidbody.centerOfMass = originalCenterOfMass;

            // RunningState'e geri dön
            player.ChangeState(new RunningState());
        }
    }

    public void OnCollisionEnter(PlayerStateMachine player, Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            player.ChangeState(new HitToHeadState());
        }
    }
}
