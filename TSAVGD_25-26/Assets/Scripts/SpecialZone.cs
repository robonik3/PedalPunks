using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public class SpecialZone : MonoBehaviour
{
    public bool happened;
    public UnityEvent OnEnter;
    void Update()
    {
        if (!happened)
        {
            if (transform.position.x < 0)
            {
                happened = true;
                OnEnter.Invoke();
            }
        }
        else
        {
            if (transform.position.x > 0)
            {
                happened = false;
            }
        }


    }
        
   
}
