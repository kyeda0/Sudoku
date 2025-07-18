using UnityEngine;

public class NumberButton : MonoBehaviour
{
    [SerializeField] private int number;


    public void OnClick()
    {
        if(GameManager._instance !=null)
            GameManager._instance.SetNumberToSelctCell(number);
    }
}
