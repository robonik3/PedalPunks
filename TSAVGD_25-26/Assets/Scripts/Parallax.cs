using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float mult;

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.parent.position * mult;
    }
    public void SetMult(float m) { mult = m; }
}
