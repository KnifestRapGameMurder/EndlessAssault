using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AidKit : MonoBehaviour
{
    [SerializeField] private string playerTag;
    [SerializeField] private float healAmount;
    [SerializeField] private string healSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == playerTag)
        {
            collision.GetComponent<HealthControl>().Heal(healAmount);
            AudioManager.Instance.PlaySound(healSound);
            Destroy(gameObject);
        }
    }
}
