using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float damage;
    [SerializeField] private string shotSound;

    private void Start()
    {
        AudioManager.Instance.PlaySound(shotSound);
    }

    private void Update() { if ((lifeTime -= Time.deltaTime) <= 0) Destroy(gameObject); }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var health = collision.GetComponent<HealthControl>();
        if (health)
        {
            health.TakeDamage(damage, collision.ClosestPoint(transform.position));
            Destroy(gameObject);
        }
    }
}