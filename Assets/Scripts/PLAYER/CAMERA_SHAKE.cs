using UnityEngine;
using Cinemachine; // ou "using Unity.Cinemachine;" se estiver no Cinemachine 3.x

public class CAMERA_SHAKE : MonoBehaviour
{
    public static CAMERA_SHAKE Instance { get; private set; }

    [Header("Default Preset")]
    [SerializeField] private float defaultStrength = 0.5f;
    [SerializeField] private float defaultDuration = 0.3f;

    [SerializeField]
    private CinemachineImpulseSource impulseSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shake(float duration, float strength)
    {
        impulseSource.m_ImpulseDefinition.m_ImpulseDuration = duration;
        impulseSource.GenerateImpulse(strength);
    }

    public void ShakeDefault() => Shake(defaultDuration, defaultStrength);

    // Presets prontos
    public void ShakeLight() => Shake(0.2f, 0.15f);
    public void ShakeMediumLight() => Shake(0.3f, 0.2f);
    public void ShakeMedium() => Shake(0.3f, 0.25f);
    public void ShakeHeavy() => Shake(0.5f, 1.2f);

    public void ShakeWithEnum(shakeMode _shakeMode)
    {
        switch(_shakeMode)
        {
            case shakeMode.Light:
                ShakeLight();
                break;

            case shakeMode.LightMedium:
                ShakeMediumLight();
                break;

            case shakeMode.Medium:
                ShakeMedium();
                break;

            case shakeMode.Heavy:
                ShakeHeavy();
                break;
        }
    }
}

public enum shakeMode { Light, LightMedium, Medium, Heavy}