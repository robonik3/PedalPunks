using Unity.VisualScripting;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float mult;
    [SerializeField] private float manual;

    // Update is called once per frame
    void Update()
    {
        if(manual==0)transform.position = transform.parent.position * mult;
        else { transform.position += manual * Time.deltaTime * Vector3.left; }
    }
    public void SetMult(float m) { mult = m; }
}
