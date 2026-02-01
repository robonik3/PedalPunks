using UnityEngine;

[CreateAssetMenu(fileName = "BikeType", menuName = "Scriptable Objects/BikeType")]
public class BikeType : ScriptableObject
{
    public GameObject prefab;
    public AnimatorOverrideController visual;
}
