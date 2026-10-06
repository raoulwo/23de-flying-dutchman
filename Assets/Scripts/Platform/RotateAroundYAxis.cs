using UnityEngine;

namespace Platform
{
    public class RotateAroundYAxis : MonoBehaviour
    {
        [SerializeField] private float speed = 10.0f;
    
        private void Update()
        {
            transform.Rotate(Vector3.up, speed * Time.deltaTime); 
        }
    }
}
