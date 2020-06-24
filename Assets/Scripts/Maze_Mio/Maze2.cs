using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze2 : MonoBehaviour
{
    //[Header("View stuff")]
    //public Transform cameraTransform;
    //public GameObject characterPrefab;

    [Header("Prefabs and stuff")]
    public GameObject cellPrefab;

    [Header("Size")]
    public int width;
    public int height;

    private Cell[,] cellGrid;

    private Stack<Cell> stack;

    private bool generationStarted = false;
    private bool generationFinished = false;
    private bool characterSpawwned = false;

    // Start is called before the first frame update
    void Start()
    {
        this.cellGrid = new Cell[this.width, this.height];
        this.stack = new Stack<Cell>();
    }

    IEnumerator InstantiateCells()
    {
        for (int i = 0; i < this.width; i++)
        {
            for (int j = 0; j < this.height; j++)
            {
                GameObject cellGO = Instantiate(this.cellPrefab, new Vector3(i, 0, j), Quaternion.identity);
                this.cellGrid[i, j] = cellGO.GetComponent<Cell>();
            }
            yield return null;
        }

        // Para empezar en una aleatoria
        Cell start = this.cellGrid[Random.Range(0, this.width), Random.Range(0, this.height)];
        start.isVisited = true;

        this.stack.Push(start);
        this.generationFinished = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Jump") && !this.generationStarted)
        if (!this.generationStarted)
        {
            this.generationStarted = true;
            StartCoroutine(InstantiateCells());
        }
        if (this.stack.Count > 0)
        {
            Cell current = this.stack.Peek();

            bool valid = false;
            int checks = 0;

            //Quitamos los decimales
            int current_x = (int)current.transform.position.x;
            int current_y = (int)current.transform.position.z;

            while (checks < 10 && !valid)
            {
                checks++;
                WallOrientation direction = (WallOrientation)Random.Range(0, 4);

                switch (direction)
                {
                    case WallOrientation.WEST:
                        if (current_x > 0)
                        {
                            Cell next = this.cellGrid[current_x - 1, current_y];

                            if (!next.isVisited)
                            {
                                current.HideWall(WallOrientation.WEST);
                                next.HideWall(WallOrientation.EAST);

                                next.isVisited = true;
                                this.stack.Push(next);
                                valid = true;
                            }
                        }
                        break;

                    case WallOrientation.NORTH:
                        if (current_y < (this.height - 1))
                        {
                            Cell next = this.cellGrid[current_x, current_y + 1];

                            if (!next.isVisited)
                            {
                                current.HideWall(WallOrientation.NORTH);
                                next.HideWall(WallOrientation.SOUTH);

                                next.isVisited = true;
                                this.stack.Push(next);
                                valid = true;
                            }
                        }
                        break;


                    case WallOrientation.EAST:
                        if (current_x < (this.width - 1))
                        {
                            Cell next = this.cellGrid[current_x + 1, current_y];

                            if (!next.isVisited)
                            {
                                current.HideWall(WallOrientation.EAST);
                                next.HideWall(WallOrientation.WEST);

                                next.isVisited = true;
                                this.stack.Push(next);
                                valid = true;
                            }
                        }
                        break;


                    case WallOrientation.SOUTH:
                        if (current_y > 0)
                        {
                            Cell next = this.cellGrid[current_x, current_y - 1];

                            if (!next.isVisited)
                            {
                                current.HideWall(WallOrientation.SOUTH);
                                next.HideWall(WallOrientation.NORTH);

                                next.isVisited = true;
                                this.stack.Push(next);
                                valid = true;
                            }
                        }
                        break;
                }
            }

            if (!valid)
                this.stack.Pop();
        }
        else if (!this.characterSpawwned && this.generationFinished)
        {
            this.characterSpawwned = true;
            //this.SpawnPlayer();
        }
    }
    /*
    private void SpawnPlayer()
    {
        Destroy(this.cameraTransform.gameObject);

        var position = new Vector3(Random.Range(0, this.width), 2, Random.Range(0, this.height));

        Instantiate(this.characterPrefab, position, Quaternion.identity);
    }*/
}
