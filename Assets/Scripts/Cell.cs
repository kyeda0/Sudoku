using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] private int _row;
    [SerializeField] private int _col;
    [SerializeField] private Text _numberText;
    [SerializeField] private Image _background;
    [SerializeField] private float _scaleCell;
    public bool _active;
    private Coroutine _currentAnimation;
    private bool _isError;
    private bool _isEditor = true;
    public int Row => _row;
    public int Col => _col;
    public bool IsEditor => _isEditor;
    public Image Background => _background;


    public void SetPosition(int _r, int _c)
    {
        _row = _r;
        _col = _c;
    }
    public void SetValue(int value, bool editor)
    {
        _isEditor = editor;
         _numberText.text = value == 0 ? "" : value.ToString();
        _numberText.color = Color.black;
    }

    public void HightLight( bool active, Color _color)
    {
        _active = active;
        if(_currentAnimation != null)
            StopCoroutine(_currentAnimation);
        
        StartCoroutine(AnimateHighlight(_background, active ? _color : Color.white,active));
    }
    public void OnClick()
    {
        if (GameManager._instance != null)
        {
            GameManager._instance.SelectCell(this);
        }
    }



    private IEnumerator AnimateHighlight(Image _background, Color _targetColor,bool active, float _duration = 0.3f)
    {
        Color _startColor = _background.color;
        float _time = 0f;
        while(_time < _duration)
        {
            _time += Time.deltaTime;
            _background.color = Color.Lerp(_startColor, _targetColor, _time / _duration);
            
              _background.transform.localScale = active ? 
                  Vector3.Lerp(new Vector3(0.5f,0.5f,1f),  Vector3.one * _scaleCell, _time / _duration) 
                  : Vector3.Lerp(Vector3.one * _scaleCell, new Vector3(0.5f,0.5f,1f),_time/_duration);
              _numberText.fontSize = active ? 30 : 25;
            yield return null;
        }

        _background.color = _targetColor;
        _currentAnimation = null;
    }
}
