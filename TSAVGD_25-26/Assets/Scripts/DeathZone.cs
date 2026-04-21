using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private bool jumpable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerScript player)) 
        {
            if(jumpable) {if(player.height == 0) player.Die(); }
            else
            {
                player.Die();
            }
        }
    }
}
