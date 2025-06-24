using UnityEngine;

public class SwitchPushAnimation : MonoBehaviour
{
    [Header("Z-Axis Toggle Animation")]
    public float moveSpeed = 15f;          // Speed of the movement
    public AudioClip pushSound;

    private float offZPosition;           // Original Z position (unpressed)
    private float onZPosition;            // Pressed Z position (calculated)
    private bool isPushed = false;       // Animation state
    private AudioSource audioSource;

    void Start()
    {
        offZPosition = transform.localPosition.z;
        onZPosition = offZPosition * 0.375f;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null && pushSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void Animate()
    {
        isPushed = !isPushed;

        if (pushSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(pushSound);
        }

        StopAllCoroutines();
        StartCoroutine(MoveSwitchCoroutine());
    }

    private System.Collections.IEnumerator MoveSwitchCoroutine()
    {
        Vector3 startPos = transform.localPosition;
        Vector3 targetPos = new Vector3(startPos.x, startPos.y, isPushed ? onZPosition : offZPosition);

        while (Vector3.Distance(transform.localPosition, targetPos) > 0.01f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * moveSpeed);
            yield return null;
        }

        transform.localPosition = targetPos; // Snap to final position
    }
}
