using UnityEngine;
using DG.Tweening;

public class CAMERA_SHAKE : MonoBehaviour
{
    public static CAMERA_SHAKE Instance { get; private set; }

    [Header("Default Preset")]
    [SerializeField] private float defaultStrength = 0.5f;
    [SerializeField] private int defaultVibrato = 10;
    [SerializeField] private float defaultDuration = 0.3f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Shake(float duration, float strength, int vibrato = 10)
    {
        DOTween.Kill(transform);
        transform.DOShakePosition(duration, strength, vibrato).SetEase(Ease.OutQuad);
    }

    public void ShakeDefault() => Shake(defaultDuration, defaultStrength, defaultVibrato);

    // Presets prontos
    public void ShakeLight() => Shake(0.2f, 0.2f, 8);
    public void ShakeMedium() => Shake(0.3f, 0.5f, 10);
    public void ShakeHeavy() => Shake(0.5f, 1.2f, 15);
}