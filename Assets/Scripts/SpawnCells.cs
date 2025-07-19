using UnityEngine;

public class SpawnCellsGeneration : MonoBehaviour
{
  private Transform _gridParent;
  [SerializeField] private GameObject _cellPrefab;
  public  Cell[,] _cells = new Cell[9,9];
  [SerializeField] private int _g;


  private void Awake()
  {
    _gridParent = gameObject.GetComponent<Transform>().transform;
  }
  public void Init()
  {
    for (int i = 0;  i < _g * _g; i++)
    {
        int row = i / _g;
        int col = i % _g;
        GameObject cellPrefab = Instantiate(_cellPrefab, _gridParent);
        cellPrefab.name = "Cell " + i;

        Cell cell = cellPrefab.GetComponent<Cell>();
        cell.SetPosition(row,col);
        cell.SetValue(0,true);
        _cells[row,col] = cell;
    }
  }
  
  public Cell GetCell(int _row, int _col)
  {
    return _cells[_row, _col];
  }
}
