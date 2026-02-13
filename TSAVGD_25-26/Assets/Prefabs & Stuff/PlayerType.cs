using UnityEngine;

[CreateAssetMenu(fileName = "PlayerType", menuName = "Scriptable Objects/PlayerType")]
public class PlayerType : ScriptableObject
{
    public AnimatorOverrideController visual;
    public AnimatorOverrideController visualT;
    public Sprite DisplaySprite;
    public float turningSpeed;
    public float forwardSpeed;
    public float gravity;
    public string characterName;
}
