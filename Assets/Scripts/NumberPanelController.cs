using System.Collections;
using UnityEngine;

public class NumberPanelController : MonoBehaviour
{
    [SerializeField] private RectTransform _panel;
    [SerializeField] private float _animationDuration = 0.3f;
    private Vector2 _hiddenPos;
    private Vector2 _showPos;
    private Coroutine _currentAnimation;
   private  void Awake()
   {
       _showPos = new Vector2(0f,50f);
       _hiddenPos = new Vector2(0f, _panel.rect.height);
       _panel.anchoredPosition = _hiddenPos;
   }

   public void ShowPanel()
   {
       AnimateTo(_showPos);
   }

   public void HidePanel()
   {
       AnimateTo(_hiddenPos);
   }

   private void AnimateTo(Vector2 _target)
   {
       if (_currentAnimation != null)
       {
           StopCoroutine(_currentAnimation);
       }

       _currentAnimation = StartCoroutine(AnimationMove(_target));
   }
   private IEnumerator AnimationMove(Vector2 _targetPos)
   {
       Vector2 _startPos = _panel.anchoredPosition;
       float _elapsed = 0f;
       while (_elapsed < _animationDuration)
       {
           _elapsed += Time.deltaTime;
           float t = Mathf.SmoothStep(0, 1, _elapsed / _animationDuration);
           _panel.anchoredPosition = Vector2.Lerp(_startPos, _targetPos, t);
           yield return null;
       }

       _panel.anchoredPosition = _targetPos;
   }

}
