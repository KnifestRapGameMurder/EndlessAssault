using UnityEngine;

public class Barricade : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) { if (collision.tag.Contains("Bullet")) Destroy(collision.gameObject);}
}
