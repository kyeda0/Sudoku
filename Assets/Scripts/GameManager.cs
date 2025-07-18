using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    [SerializeField] private NumberPanelController _numberPanelController;
    private Cell _selectCell;
    private int[,] _boarder = new int[9,9];

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
        _selectCell = _cell;
      _selectCell.HightLight(true,Color.azure);
      _numberPanelController.ShowPanel();
    }


    public void SetNumberToSelctCell(int number)
    {
        if (_selectCell != null)
            _selectCell.SetValue(number, true);

        int _row = _selectCell.Row;
        int _col = _selectCell.Col;

        if (IsValid(number, _row, _col))
        {
            _boarder[_row, _col] = number;
            _selectCell.SetValue(number,true);
            _selectCell.SetErrorHightLight(false);
        }
        else
        {
            _selectCell.SetValue(number,true);
            _selectCell.SetErrorHightLight(true);
        }
    }

    private bool IsValid(int value, int row, int col)
    {
        for (int c = 0; c < 9 ; c++)
        {
            if (_boarder[row, c] == value)
            {
                return false;
            }
        }

        for (int r = 0; r < 9; r++)
        {
            if (_boarder[r, col] == value)
            {
                return false;
            }
        }

        int startRow = (row / 3) * 3;
        int startCol = (col / 3) * 3;

        for (int r = startRow; r < startRow + 3; r++)
        {
            for (int c = startCol; c < startCol + 3; c++)
            {
                if (_boarder[r, c] == value)
                {
                    return false;
                }
            }
        }
        
        return true;
    }
}
