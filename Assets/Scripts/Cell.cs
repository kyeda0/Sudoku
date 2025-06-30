using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] private int _row;
    [SerializeField] private int _col;
    [SerializeField] private Text _numberText;
    [SerializeField] private Image _background;

    private bool _isEditor = true;



    public void SetPosition(int _r, int _c)
    {
        _row = _r;
        _col = _c;
    }
    public void SetValue(int value, bool editor)
    {
        value = Random.Range(0,9);
        _isEditor = editor;
        _numberText.text = value == 0 ? "0" : value.ToString();
        _numberText.color = editor ? Color.black : Color.gray;
    }

    public void HihgLight( bool _active)
    {
        _background.color = _active ? new Color(1f,1f,0,8f) : Color.white;
    }


    public void OnClick()
    {
        if(!_isEditor) return;
        
        GameManager._instance.SelectCell(this);
    } 
}
