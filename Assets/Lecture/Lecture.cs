using UnityEngine;
using UnityEngine.UI;

public class Lecture : MonoBehaviour
{

    public int size = 3;
    public GameObject buttonPrefab;
    public Transform grid; 

    public Button[,] buttons;

    private void Start()
    {
        buttons = new Button[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                GameObject clone = Instantiate(buttonPrefab, grid);
                Button b = clone.GetComponent<Button>(); 
                Text t = clone.GetComponentInChildren<Text>();
                buttons[i, j] = b;
                t.text = "5";
                int finalI = i;
                int finalJ = j;
                // Arrow Function
                //b.onClick.AddListener(() =>
                //{
                //    print($"{finalI}-{finalJ}");
                //});
                // pass function
                b.onClick.AddListener(winCheck);
                // pass function with arrow function
                b.onClick.AddListener(() => winCheck());
            }
        }
    }

    void winCheck()
    {
        Text t = buttons[0, 0].GetComponentInChildren<Text>();
        t.text = "0";
    }

}
