using Platform;
using UnityEngine;

namespace UI
{
    public class UIDistanceArrow : MonoBehaviour
    {
        [SerializeField] private PlatformController platformController;

        private void Update()
        {
            var direction = platformController.PlayerDistance;

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(-direction);
            }
        }
    }
}
