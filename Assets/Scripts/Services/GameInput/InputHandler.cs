using UnityEngine;

namespace Services.GameInput
{
    public class InputHandler : MonoBehaviour, IInputHandler
    {
        private readonly string _horizontal = "Horizontal";

        public bool SwipeRight()
        {
            if (SimpleInput.GetAxisRaw(_horizontal) > 0.0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SwipeLeft()
        {
            if (SimpleInput.GetAxisRaw(_horizontal) < 0.0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}