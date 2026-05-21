using UnityEngine;
using UnityEngine.InputSystem;

public class REBIND_HANDLER : MonoBehaviour
{
    public InputActionReference attackAction;
    public InputActionReference moveAction;

    public void StartRebinding()
    {
        // 1. Disable the action before rebinding
        attackAction.action.Disable();

        var rebindOperation = attackAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse") // Prevents accidental mouse clicks from rebinding
            .OnMatchWaitForAnother(0.1f)     // Slight delay to avoid double-triggers
            .OnComplete(operation =>
            {
                Debug.Log($"Rebound to: {operation.selectedControl.name}");
                Cleanup(operation);
            })
            .OnCancel(operation => Cleanup(operation))
            .Start();
    }

    private void Cleanup(InputActionRebindingExtensions.RebindingOperation operation)
    {
        operation.Dispose(); // Memory management is critical here
        attackAction.action.Enable(); // 2. Re-enable the action
    }
}