using UnityEngine;
using EasyUI.PickerWheelUI;
using UnityEngine.UI;

public class Demo : MonoBehaviour
{
    [SerializeField] private Button uiSpinButton;
    [SerializeField] private Text uiSpinButtonText;

    [SerializeField] private PickerWheel pickerWheel;


    private void Start()
    {
        uiSpinButton.onClick.AddListener(() =>
        {

            uiSpinButton.interactable = false;
            uiSpinButtonText.text = "Spinning";

            pickerWheel.OnSpinEnd(wheelPiece =>
            {
                Debug.Log("Index end: " + wheelPiece.Index);

                uiSpinButton.interactable = true;
                uiSpinButtonText.text = "Spin";
            });

            pickerWheel.Spin();

        });

    }

}
