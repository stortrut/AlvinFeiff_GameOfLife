using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOfLife : MonoBehaviour
{
    public GameObject cellPrefab;
    public float cellSize = 0.25f; //Size of our cells
    public int spawnChancePercentage = 70;
    public int numberOfColums, numberOfRows;
    private List<GameObject> cells = new List<GameObject>();
    private float cellUpdateTime = 1f;
    private float currentCellUpdateTime;

    public Slider cellSpeedSlider;
    public Slider spawnChanceSlider;
    public TextMeshProUGUI spawnChanceText;

    public bool cellUpdatePause = true;


    private void Start()
    {
        //Set values
        currentCellUpdateTime = cellUpdateTime;
        ChangeCellUpdateSpeed();
        ChangeCellSpawnRate();

        //For each row
        for (int y = 0; y < numberOfRows; y++)
        {
            //for each column in each row
            for (int x = 0; x < numberOfColums; x++)
            {
                //Create our game cell objects, multiply by cellSize for correct world placement
                Vector2 newPos = new Vector2(x * cellSize - Camera.main.orthographicSize *
                    Camera.main.aspect,
                    y * cellSize - Camera.main.orthographicSize);

                var newCell = Instantiate(cellPrefab, newPos, Quaternion.identity);
                newCell.transform.localScale = Vector2.one * cellSize;
                cells.Add(newCell);
            }
        }
    }


    private void Update()
    {
        currentCellUpdateTime -= Time.deltaTime;

        if(currentCellUpdateTime <= 0)
        {
            UpdateCells();
            currentCellUpdateTime = cellUpdateTime;
        }
    }

    private void UpdateCells()
    {
        if (cellUpdatePause) { return; }

        for (int i = 0; i < cells.Count; i++)
            cells[i].GetComponent<Cell>().CheckNearbyCells();
        for (int i = 0; i < cells.Count; i++)
            cells[i].GetComponent<Cell>().UpdateLifeStatus();
    }


    //UI methods
    public void PauseCells()
    {
        cellUpdatePause = true;
    }
    public void PlayCells()
    {
        cellUpdatePause = false;
    }
    public void ChangeCellUpdateSpeed()
    {
        cellUpdateTime = cellSpeedSlider.value;
    }
    public void ChangeCellSpawnRate()
    {
        spawnChancePercentage = Mathf.RoundToInt(spawnChanceSlider.value);
        spawnChanceText.text = spawnChancePercentage.ToString();
    }
    public void GenerateRandomCellLayout()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            //Random check to see if it should be alive
            if (Random.Range(0, 100) < spawnChancePercentage)
                cells[i].GetComponent<Cell>().ChangeLifeStatus(true);
        }
    }
    public void ClearAllCells()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].GetComponent<Cell>().ChangeLifeStatus(false);
        }
    }
   
}
