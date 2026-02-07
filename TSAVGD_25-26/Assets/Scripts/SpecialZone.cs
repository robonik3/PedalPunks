using UnityEngine;
using UnityEngine.Events;
public class SpecialZone : MonoBehaviour
{
    public UnityEvent OnEnter;
    private void OnTriggerEnter2D(Collider2D other)
    {
        OnEnter.Invoke();
    }
}
