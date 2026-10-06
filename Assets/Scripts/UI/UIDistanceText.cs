using System.Globalization;
using Platform;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UIDistanceText : MonoBehaviour
    {
        [SerializeField] private PlatformController platformController;

        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            var distance = platformController.PlayerDistance.magnitude;
            _text.text = distance.ToString("F1", CultureInfo.InvariantCulture) + " m";
        }
    }
}