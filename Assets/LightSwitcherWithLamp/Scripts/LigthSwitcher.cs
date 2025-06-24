using UnityEngine;

public class LightSwitcher : MonoBehaviour
{
    [Header("Light Switch Components")]
    public Light sceneLight; // Reference to the light to toggle
    public GameObject pointer; // Object that activates the switch
    // public MonoBehaviour switchAnimation; // Optional animation script

    private bool isLightOn; // Internal state of the light
    private SwitchPushAnimation switchAnimation;

    private System.Reflection.MethodInfo animateMethod = null;

    void Start()
    {
        CheckLight();
        CheckPointer();
        setAnimation();
    }

    void CheckLight()
    {
        if (sceneLight == null)
        {
            Debug.LogError("Scene Light is not assigned in the inspector.");
        }

        isLightOn = sceneLight.enabled;
    }

    void CheckPointer()
    {
        if (pointer == null)
        {
            Debug.LogError("Pointer GameObject is not assigned in the inspector.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == pointer)
        {
            ToggleLight();
        }
    }

    void setAnimation()
    {
        switchAnimation = GetComponent<SwitchPushAnimation>();

        if (switchAnimation == null)
        {
            Debug.LogWarning("Switch animation is not assigned.");
            return;
        }

        animateMethod = switchAnimation.GetType().GetMethod("Animate");

        if (animateMethod == null)
        {
            Debug.LogWarning("Assigned animation does not contain an 'Animate' method.");
        }
    }

    void ToggleLight()
    {
        if (sceneLight == null) return;

        isLightOn = !isLightOn;
        sceneLight.enabled = isLightOn;

        TriggerAnimation();
    }

    void TriggerAnimation()
    {
        if (animateMethod != null)
        {
            // animateMethod.Invoke(switchAnimation, null);
            switchAnimation.Animate();
        }
    }
}
