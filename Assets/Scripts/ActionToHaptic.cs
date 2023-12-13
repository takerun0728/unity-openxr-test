using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.OpenXR.Input;
using static UnityVR.LibraryForVRTextbook;

namespace UnityVR
{
    public class ActionToHaptic : ActionToControl
    {
        [SerializeField] InputActionReference hapticActionReference;

        InputAction hapticAction;

        void Start()
        {
            if(hapticActionReference is null || (hapticAction = hapticActionReference.action) is null)
            {
                isReady = false;
                errorMessage += "#hapticActionReference";
            }

            if(!isReady)
            {
                displayMessage.text = $"{GetSourceFileName()}\r\nError: {errorMessage}";
            }
            else
            {
                hapticAction.Enable();
            }
        }

        protected override void OnActionPerformed(InputAction.CallbackContext ctx) => UpdateValue(ctx);
        protected override void OnActionCanceled(InputAction.CallbackContext ctx) => UpdateValue(ctx);

        void UpdateValue(InputAction.CallbackContext ctx)
        {
            var device = ctx.action?.activeControl?.device;
            if (device is null) return;

            var message = "Haptic: ";
            if (ctx.ReadValueAsButton())
            {
                var intensity = 1f;
                var duration = 0.5f;
                OpenXRInput.SendHapticImpulse(hapticAction, intensity, duration, device);
                message += $"call={ctx.action.name}, haptic={hapticAction.name}, device={device.name}";
            }
            displayMessage.text = message;
        }
    }
}
