using Economy;
using Helpers;
using SaveModule;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.LevelRun.TwoDimensional
{
    public class TwoDimensionalLevelRunner : LevelRunner
    {
        [SerializeField]
        private RectTransform _content;

        [SerializeField]
        private TMP_InputField _wheelPiecesInputField;

        [SerializeField]
        private TMP_InputField _spinDurationInputField;

        [SerializeField]
        private Button _applyChangesButton;

        [SerializeField]
        private Button _spinButton;

        [SerializeField]
        private TMP_Text _spinButtonText;

        [SerializeField]
        private TMP_Text _lastWinText;

        [SerializeField]
        private string _wheelSpinningText;

        [SerializeField]
        private string _wheelIdleText;

        [SerializeField]
        private string _lastWinInfo;

        [SerializeReference, SubclassSelector]
        private TwoDimensionalGameController _twoDimensionalGameController;

        public override void Initialize()
        {
            InitializeButtons();
            InitializeGameController();
            InitializeInputFields();
            RefreshLastWinScore();
            _content.gameObject.SetActive(false);
        }

        public override void Activate()
        {
            _twoDimensionalGameController.Activate();
            _content.gameObject.SetActive(true);
        }

        public override void Deactivate()
        {
            _twoDimensionalGameController.Deactivate();
            _content.gameObject.SetActive(false);
        }

        private void RefreshLastWinScore()
        {
            var reward = SaveSystem.LoadLastWinScore();

            var result = $"{_lastWinInfo} {Extensions.GetBriefTextOfScore(reward)}";
            _lastWinText.text = result;
        }

        private void InitializeInputFields()
        {
            _wheelPiecesInputField.contentType = TMP_InputField.ContentType.IntegerNumber;
            _spinDurationInputField.contentType = TMP_InputField.ContentType.DecimalNumber;
        }

        private void InitializeButtons()
        {
            _spinButton.onClick.RemoveListener(SpinButton_OnClick);
            _spinButton.onClick.AddListener(SpinButton_OnClick);

            _applyChangesButton.onClick.RemoveListener(ApplyChangesButton_OnClick);
            _applyChangesButton.onClick.AddListener(ApplyChangesButton_OnClick);

        }

        private void InitializeGameController()
        {
            _twoDimensionalGameController.Initialize();
            _twoDimensionalGameController.Deactivate();

            _twoDimensionalGameController.OnSpinStart -= TwoDimensionalGameController_OnSpinStart;
            _twoDimensionalGameController.OnSpinStart += TwoDimensionalGameController_OnSpinStart;

            _twoDimensionalGameController.OnSpinEnd -= TwoDimensionalGameController_OnSpinEnd;
            _twoDimensionalGameController.OnSpinEnd += TwoDimensionalGameController_OnSpinEnd;
        }

        private void ApplyChangesButton_OnClick()
        {
            var piecesCountText = _wheelPiecesInputField.text;
            int.TryParse(piecesCountText, out int piecesCount);

            if (piecesCount > 0f)
            {
                _twoDimensionalGameController.SetPiecesCount(piecesCount);
            }

            var spinDurationText = _spinDurationInputField.text;
            float.TryParse(spinDurationText, out float spinDuration);

            if (spinDuration > 0f)
            {
                _twoDimensionalGameController.SetSpinDuration(spinDuration);
            }

            if (piecesCount > 0f || spinDuration > 0f)
            {
                _twoDimensionalGameController.ApplyChanges();
            }
        }

        private void SpinButton_OnClick()
        {
            _twoDimensionalGameController.Spin();
        }

        private void TwoDimensionalGameController_OnSpinStart(TwoDimensionalWheelPieceData obj)
        {
            _spinButton.interactable = false;
            _spinButtonText.text = _wheelSpinningText;
        }

        private void TwoDimensionalGameController_OnSpinEnd(TwoDimensionalWheelPieceData obj)
        {
            _spinButton.interactable = true;
            _spinButtonText.text = _wheelIdleText;

            var reward = obj.Reward;
            SimpleEconomy.AddScore(reward);

            var result = $"{_lastWinInfo} {Extensions.GetBriefTextOfScore(reward)}";
            _lastWinText.text = result;
            SaveSystem.SaveLastWinScore(reward);
        }
    }
}
