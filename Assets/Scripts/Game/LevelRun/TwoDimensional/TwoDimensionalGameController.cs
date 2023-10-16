using DG.Tweening;
using Game.Mechanics;
using System.Collections.Generic;
using UnityEngine;

namespace Game.LevelRun.TwoDimensional
{
    [System.Serializable]
    public class TwoDimensionalGameController
    {
        [SerializeField]
        private RectTransform _content;

        [SerializeField]
        private RectTransform _lineParent;

        [SerializeField]
        private RectTransform _wheelParent;

        [SerializeField]
        private RectTransform _wheelContentToRotate;

        [SerializeField]
        private TwoDimensionalLine _linePrefab;

        [SerializeField]
        private TwoDimensionalWheelPiece _wheelPiecePrefab;

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private AudioClip _audioClip;

        [SerializeReference, SubclassSelector]
        private INumbersGenerator _numbersGenerator;

        private TwoDimensionalWheelPiece _startPrefab;

        private float _halfPieceAngleWithPaddings;
        private float _halfPieceAngle;
        private float _pieceAngle;
        private float _accumulatedWeight;
        private float _spinDuration;

        private int _pieceCount;

        private readonly Vector2 _wheelPieceMinSize = new Vector2(40.5f, 73f);
        private readonly Vector2 _wheelPieceMaxSize = new Vector2(144f, 213f);
        private readonly int _maxWheelPieces = 24;
        private readonly int _minWheelPieces = 2;
        private readonly System.Random _random = new System.Random();
        private readonly List<TwoDimensionalLine> _line = new List<TwoDimensionalLine>();
        private readonly List<int> _nonZeroChancesIndixes = new List<int>();
        private readonly List<TwoDimensionalWheelPiece> _wheelPiece = new List<TwoDimensionalWheelPiece>();
        private readonly List<TwoDimensionalWheelPieceData> _wheelPieceData = new List<TwoDimensionalWheelPieceData>();

        public event System.Action<TwoDimensionalWheelPieceData> OnSpinStart;
        public event System.Action<TwoDimensionalWheelPieceData> OnSpinEnd;

        public void Initialize()
        {
            _pieceCount = 16;
            _spinDuration = 1f;

            GenerateBoard();
        }

        public void Activate()
        {
            _content.gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            _content.gameObject.SetActive(false);
        }

        public void Spin()
        {
            ProcessSpin();
        }

        public void SetSpinDuration(float spinDuration)
        {
            if (spinDuration < 0f)
            {
                spinDuration = 0f;
            }

            _spinDuration = spinDuration;
        }

        public void SetPiecesCount(int pieceCount)
        {
            if (pieceCount > _maxWheelPieces)
            {
                pieceCount = _maxWheelPieces;
                Debug.Log($"Max piece count: {_maxWheelPieces}");
            }
            if (pieceCount < _minWheelPieces)
            {
                pieceCount = _minWheelPieces;
                Debug.Log($"Max piece count: {_minWheelPieces}");
            }

            _pieceCount = pieceCount;
        }

        public void ApplyChanges()
        {
            GenerateBoard();
        }

        private void GenerateBoard()
        {
            ClearPrevious(); // 1

            GenerateWheelPieceData(); // 2

            SetUpAngles(); // 3
            GenerateWheelContent(); // 4
            CalculateWeightsAndIndixes(); // 5
        }

        private void ClearPrevious()
        {
            foreach (var item in _wheelPiece)
            {
                Object.Destroy(item.gameObject);
            }

            foreach (var item in _line)
            {
                Object.Destroy(item.gameObject);
            }

            _line.Clear();
            _wheelPiece.Clear();
            _nonZeroChancesIndixes.Clear();
            _wheelPieceData.Clear();
        }

        private void GenerateWheelPieceData()
        {
            var rewards = _numbersGenerator.GenerateNumbers(_pieceCount);
            for (int i = 0; i < _pieceCount; i++)
            {
                var reward = rewards[i];
                var dropChance = Random.value;

                var data = new TwoDimensionalWheelPieceData(reward, dropChance);
                _wheelPieceData.Add(data);
            }
        }

        private void SetUpAngles()
        {
            _pieceAngle = 360f / _pieceCount;
            _halfPieceAngle = _pieceAngle / 2f;
            _halfPieceAngleWithPaddings = _halfPieceAngle - (_halfPieceAngle / 4f);
        }

        private void GenerateWheelContent()
        {
            _startPrefab = InstantiatePiece(_wheelPiecePrefab, _wheelParent);

            var gridRect = _startPrefab.VerticalLayoutGroup.GetComponent<RectTransform>();
            var width = Mathf.Lerp(_wheelPieceMinSize.x, _wheelPieceMaxSize.x, 1f - Mathf.InverseLerp(_minWheelPieces, _maxWheelPieces, _pieceCount));
            var height = Mathf.Lerp(_wheelPieceMinSize.y, _wheelPieceMaxSize.y, 1f - Mathf.InverseLerp(_minWheelPieces, _maxWheelPieces, _pieceCount));
            gridRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            gridRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

            for (int i = 0; i < _wheelPieceData.Count; i++)
            {
                DrawWheelPiece(i);
                DrawLine(i);
            }

            Object.Destroy(_startPrefab.gameObject);
        }

        private void DrawLine(int index)
        {
            var line = Object.Instantiate(_linePrefab, _lineParent.position, Quaternion.identity, _lineParent);
            line.transform.RotateAround(_wheelParent.position, Vector3.back, (_pieceAngle * index) + _halfPieceAngle);

            _line.Add(line);
        }

        private void DrawWheelPiece(int index)
        {
            var piece = _wheelPieceData[index];
            var wheelPiece = InstantiatePiece(_startPrefab, _wheelParent);

            wheelPiece.RewardText.text = piece.Reward.ToString();
            wheelPiece.transform.RotateAround(_wheelParent.position, Vector3.back, _pieceAngle * index);

            _wheelPiece.Add(wheelPiece);
        }

        private TwoDimensionalWheelPiece InstantiatePiece(TwoDimensionalWheelPiece prefab, Transform parent)
        {
            return Object.Instantiate(prefab, parent.position, Quaternion.identity, parent);
        }

        private void ProcessSpin()
        {
            var index = GetRandomPieceIndex();
            var piece = _wheelPieceData[index];

            if (piece.DropChance == 0 && _nonZeroChancesIndixes.Count != 0)
            {
                index = _nonZeroChancesIndixes[Random.Range(0, _nonZeroChancesIndixes.Count)];
                piece = _wheelPieceData[index];
            }

            var angle = -(_pieceAngle * index);
            var rightOffset = (angle - _halfPieceAngleWithPaddings) % 360;
            var leftOffset = (angle + _halfPieceAngleWithPaddings) % 360;

            var randomAngle = Random.Range(leftOffset, rightOffset);
            var targetRotation = Vector3.back * (randomAngle + 2 * 360);

            var prevAngle = _wheelContentToRotate.eulerAngles.z;
            var currentAngle = _wheelContentToRotate.eulerAngles.z;

            bool isIndicatorOnTheLine = false;

            OnSpinStart?.Invoke(piece);

            var spinAnimation = _wheelContentToRotate.DORotate(targetRotation, _spinDuration, RotateMode.FastBeyond360);
            spinAnimation.SetEase(Ease.InOutQuad);
            spinAnimation.OnUpdate(() =>
            {
                var difference = Mathf.Abs(prevAngle - currentAngle);
                if (difference >= _halfPieceAngle)
                {
                    if (isIndicatorOnTheLine)
                    {
                        _audioSource.PlayOneShot(_audioClip);
                    }
                    prevAngle = currentAngle;
                    isIndicatorOnTheLine = !isIndicatorOnTheLine;
                }
                currentAngle = _wheelContentToRotate.eulerAngles.z;
            }).OnComplete(() =>
            {
                OnSpinEnd?.Invoke(piece);
            });
        }

        private int GetRandomPieceIndex()
        {
            var randomWeight = _random.NextDouble() * _accumulatedWeight;

            for (int i = 0; i < _pieceCount; i++)
            {
                if (_wheelPieceData[i].Weight >= randomWeight)
                {
                    return i;
                }
            }
            return 0;
        }

        private void CalculateWeightsAndIndixes()
        {
            for (int i = 0; i < _wheelPieceData.Count; i++)
            {
                var piece = _wheelPieceData[i];
                _accumulatedWeight += piece.DropChance;

                piece.SetWeight(_accumulatedWeight);
                piece.SetIndex(i);

                if (piece.DropChance > 0)
                {
                    _nonZeroChancesIndixes.Add(i);
                }
            }
        }
    }
}
