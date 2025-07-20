using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] private int _row;
    [SerializeField] private int _col;
    [SerializeField] private Text _numberText;
    [SerializeField] private Image _background;
    private bool _isError;
    private bool _isEditor = true;
    private  bool _active;
    public int Row => _row;
    public int Col => _col;
    public bool IsEditor => _isEditor;


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
        _background.color = active  ? _color : Color.white;
    }

    public void SetErrorHightLight(bool _active)
    {
        _isError = _active;
        _background.color = _active ? Color.red : Color.white;
    }

    public void OnClick()
    {
        if (GameManager._instance != null)
        {
            GameManager._instance.SelectCell(this);
        }
    } 
}
