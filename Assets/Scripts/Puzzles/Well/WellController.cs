using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Puzzles.Well
{
    public class WellController : MonoBehaviour, IPuzzle
    {
        [SerializeField] private float _sliderSpeedIncrease = 0.1f;
        [SerializeField] private float _sliderSpeedDeacrease = 0.1f;
        [SerializeField] private float _progressSpeedIncrease = 1f;
        [SerializeField] private float _progressSpeedDeacrease = 1f;
        [SerializeField] private float _threshold = 0.1f;
        
        [Header("UI")]
        [SerializeField] private GameObject _puzzlePanel;
        [SerializeField] private Slider _sliderTarget;
        [SerializeField] private Slider _sliderPointer;
        [SerializeField] private Image _catchingProgressUI;

        [Header("Events")] 
        [SerializeField] private UnityEvent _onResolve;
        
        private float _catchProgress;
        private float _sliderSpeed;
        private float _targetPosition = 0.5f;
        
        [ContextMenu("ActivatePuzzle")]
        public void ActivatePuzzleEvent()
        {
            _puzzlePanel.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(UpdateHandler());
            StartCoroutine(RandomizeTargetPosition());
        }
        
        public bool ActivatePuzzle()
        {
            _puzzlePanel.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(UpdateHandler());
            StartCoroutine(RandomizeTargetPosition());

            return true;
        }

        public void DiactivatePuzzle()
        {
            throw new System.NotImplementedException();
        }

        public void ResolvePuzzle()
        {
            _puzzlePanel.SetActive(false);
        }

        private IEnumerator UpdateHandler()
        {
            while (true)
            {
                _sliderSpeed = Mathf.Clamp01(_sliderSpeed);
                
                if (Input.GetMouseButton(0))
                {
                    CatchingControl();
                }
                else
                {
                    _sliderSpeed = 0;
                    _sliderPointer.value -= _sliderSpeedDeacrease;
                }

                CheckCatching();

                _sliderTarget.value = Mathf.LerpUnclamped(_sliderTarget.value, _targetPosition, 1f * Time.deltaTime);
                
                yield return null;
            }   
        }

        private void CatchingControl()
        {
            _sliderSpeed += _sliderSpeedIncrease;
            _sliderPointer.value += _sliderSpeed;
        }

        private void CheckCatching()
        {
            if (_sliderPointer.value < _sliderTarget.value + _threshold &&
                _sliderPointer.value >= _sliderTarget.value - _threshold)
            {
                _catchingProgressUI.fillAmount += _progressSpeedIncrease;

                if (_catchingProgressUI.fillAmount >= 1)
                {
                    _onResolve?.Invoke();
                    StopAllCoroutines();
                    ResolvePuzzle();
                }
            }
            else
            {
                _catchingProgressUI.fillAmount -= _progressSpeedDeacrease;
            }
        }

        private IEnumerator RandomizeTargetPosition()
        {
            while (true)
            {
                yield return new WaitForSeconds(2f);

                _targetPosition = Random.Range(0.1f, _sliderTarget.maxValue);
            }
        }
    }
}