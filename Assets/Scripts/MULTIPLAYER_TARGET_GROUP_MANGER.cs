using Cinemachine;
using Mono.Cecil;
using UnityEngine;



public class MULTIPLAYER_TARGET_GROUP_MANGER : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup targetGroup;

    [SerializeField] private float defaultWeight = 1f;

    [SerializeField] private float defaultRadius = 2f;


    private void Awake()
    {
        if (targetGroup == null)
        {
            targetGroup = FindAnyObjectByType<CinemachineTargetGroup>();
        }
    }


    public void RegisterTarget(Transform target)
    {
        if(target == null || targetGroup == null)
        {
            return;
        }

        targetGroup.AddMember(target, defaultWeight, defaultRadius);
    }

    public void UnregisterTarget(Transform target)
    {
        if (target == null || targetGroup == null)
        {
            return;
        }

        targetGroup.RemoveMember(target);
    }

}
