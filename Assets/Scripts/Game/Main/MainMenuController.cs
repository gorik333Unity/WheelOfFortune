using UnityEngine;
using UnityEngine.UI;

namespace Game.Main
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField]
        private Button _spinButton;

        private void Awake()
        {
            _spinButton.onClick.RemoveListener(SpinButton_OnClick);
            _spinButton.onClick.AddListener(SpinButton_OnClick);
        }

        private void SpinButton_OnClick()
        {

        }
    }
}
