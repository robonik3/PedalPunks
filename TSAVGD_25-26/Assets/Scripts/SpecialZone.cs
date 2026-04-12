using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class SpecialZone : MonoBehaviour
{
    public bool happened;
    public UnityEvent OnEnter;
    [SerializeField] private float manualTime;

    private void Start()
    {
        if (manualTime != 0) { StartCoroutine(Wait()); }
    }
    void Update()
    {
        if (manualTime==0)
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
        IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(manualTime);
        OnEnter.Invoke();
    }
   
}
