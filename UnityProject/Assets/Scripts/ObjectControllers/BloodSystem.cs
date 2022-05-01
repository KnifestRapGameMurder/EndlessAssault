using UnityEngine;

public class BloodSystem : MonoBehaviour
{
    [SerializeField] private GameObject bloodParticles;
    [SerializeField] private string bloodSound;


    public void ShowBlood(Vector3 position)
    {
        Instantiate(bloodParticles, position, Quaternion.identity);
        AudioManager.Instance.PlaySound(bloodSound);
    }
}