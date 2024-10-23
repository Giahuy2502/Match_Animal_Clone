using System;
using UnityEngine;
using UnityEngine.UI;

namespace UiUtilities
{
    [RequireComponent(typeof(Button))]
    public class UtilityButton : MonoBehaviour
    {
        private const string AnimatorControllerName = "Button (Legacy) 2";

        [SerializeField] private UISoundType _uiSoundType;

        [SerializeField] private bool _useUISound = true;
        [SerializeField] private bool _useUIAnimation;
        private Animator _animator;
        private RuntimeAnimatorController _animatorController;
        private Button _button;
        private void OnEnable()
        {
            SetupButton();
            SetupAnimation();
        }
        private void SetupAnimation()
        {
            if (!_useUIAnimation) return;
            _animator = GetComponent<Animator>();
            if (!_animator)
                _animator = gameObject.AddComponent<Animator>();

            _animatorController = Resources.Load<RuntimeAnimatorController>(AnimatorControllerName);
            _animator.runtimeAnimatorController = _animatorController;
        }

        private void SetupButton()
        {
            _button = GetComponent<Button>();
            if (_useUIAnimation) _button.transition = Selectable.Transition.Animation;
            _button.onClick.RemoveListener(OnClick);
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
                UiSoundManager.Instance.PlaySound(_uiSoundType);
        }

        public void Test()
        {
            UiSoundManager.Instance.PlayMusic(_uiSoundType);
        }
    }
}