using UnityEngine;

namespace Utilities
{
    public class CursorScript : MonoBehaviour
    {
        [SerializeField] private bool visible;
        [SerializeField] private bool locked;

        private void Start()
        {
            Cursor.visible = visible;
            Cursor.lockState = locked ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}