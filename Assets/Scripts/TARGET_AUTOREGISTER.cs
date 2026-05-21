using UnityEngine;

public class TARGET_AUTOREGISTER : MonoBehaviour
{
    private MULTIPLAYER_TARGET_GROUP_MANGER manager;

    private void Start()
    {
        manager = FindAnyObjectByType<MULTIPLAYER_TARGET_GROUP_MANGER>();

        if(manager != null)
        {
            manager.RegisterTarget(transform);
        }
    }

    private void OnDestroy()
    {
        if(manager != null)
        {
            manager.UnregisterTarget(transform);
        }
    }
}
