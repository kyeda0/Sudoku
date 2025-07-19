using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    [SerializeField] private NumberPanelController _numberPanelController;
    [SerializeField] private SpawnCellsGeneration _spawnCellsGeneration;
    private Cell _selectCell;
    private int[,] _boarder = new int[9,9];

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            Debug.Log("Его нет");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InitGame()
    {
        _spawnCellsGeneration.Init();
        GenerationSudoku();
        UpdateCells();
    }
    
    public void SelectCell(Cell _cell)
    { 
        _selectCell = _cell;
      _selectCell.HightLight(true,Color.azure);
      _numberPanelController.ShowPanel();
    }

    private void GenerationSudoku()
    {
       bool _success =  FillBoard(0,0);

       if (_success)
       {
           RemoveCells(40);
           UpdateCells();
       }
    }

    private bool FillBoard(int _row, int _col)
    {
        if (_row == 9)
            return true;
        int _nextRow = (_col == 8) ? _row + 1 : _row;
        int _nextCol = (_col + 1) % 9;

        List<int> _numbers = new List<int>();
        for (int i = 1; i <= 9; i++)
        {
            _numbers.Add(i);
        }

        Shuffle(_numbers);
        foreach (int _num in _numbers)
        {
            if (IsValid(_num, _row, _col))
            {
                _boarder[_row, _col] = _num;
                if (FillBoard(_nextRow, _nextCol))
                {
                    return true;
                }

                _boarder[_row, _col] = 0;
            }
        }

        return false;
    }

    private void Shuffle(List<int> _list)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            int j = Random.Range(i, _list.Count);
            (_list[i], _list[j]) = (_list[j], _list[i]);
        }
    }

    private void UpdateCells()
    {
        for (int _row = 0; _row < 9; _row++)
        {
            for (int _col = 0; _col < 9; _col++)
            {
                var _cell = _spawnCellsGeneration.GetCell(_row, _col);
                _cell.SetValue(_boarder[_row,_col],false);
                if (_boarder[_row,_col] == 0)
                {
                    _cell.SetValue(_boarder[_row,_col], true);
                }
            }
        }
    }
    public void SetNumberToSelectCell(int number)
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

    private void RemoveCells(int _countToRemove)
    {
        int _removed = 0;
        while (_removed < _countToRemove)
        {
            int _row = Random.Range(0, 9);
            int _col = Random.Range(0, 9);
            if (_boarder[_row, _col] == 0)
                continue;

            _boarder[_row, _col] = 0;
            _removed++;
        }
    }
}
