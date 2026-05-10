using UnityEngine;
using UnityEngine.InputSystem;

public class ShowOnMobile : MonoBehaviour
{
    public InputActionAsset InputActions;
    InputAction tapOn;
    InputAction tapOff;
    [SerializeField] private GameObject controlVisuals;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controlVisuals.SetActive(Application.isMobilePlatform);
        tapOn = InputActions.FindAction("TapOn");
        tapOff = InputActions.FindAction("TapOff");
    }
    private void Update()
    {
        if (tapOn.WasPerformedThisFrame())
        {
            controlVisuals.SetActive(true);
        }
        if (tapOff.WasPerformedThisFrame())
        {
            controlVisuals.SetActive(false);
        }
    }
}
