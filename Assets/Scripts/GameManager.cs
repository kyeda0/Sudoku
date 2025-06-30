using UnityEditor;
using UnityEngine;

public class GameManager: MonoBehaviour
{
    public static GameManager _instance;
    private Cell _selectCell;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectCell(Cell _cell)
    {
        if (_selectCell != null)
        {
            _selectCell.HihgLight(false);
        }
        _selectCell = _cell;
        _selectCell.HihgLight(true);
    }


    private void SetNumberToSelctCell(int number)
    {
        if(_selectCell != null)
            _selectCell.SetValue(number,true);
    }
}
