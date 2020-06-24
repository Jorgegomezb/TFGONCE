using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    //[Header("View stuff")]
    //public Transform cameraTransform;
    //public GameObject characterPrefab;

    [Header("Prefabs and stuff")]
    public GameObject cellPrefab;

    [Header("Size")]
    public int width;
    public int height;

    [Header("Pos Ini")]
    public int posXini;
    public int posZini;

    [Header("Enter")]
    public int enterX;
    public int enterZ;

    [Header("Exit")]
    public int exitX;
    public int exitZ;

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
        int posX = posXini, posZ;
        for(int i = 0; i < this.width; i++)
        {
            posZ = posZini;
            for (int j = 0; j < this.height; j++)
            {
                GameObject cellGO = Instantiate(this.cellPrefab, new Vector3(posX, 0, posZ), Quaternion.identity);
                this.cellGrid[i, j] = cellGO.GetComponent<Cell>();
                posZ += 2;
            }
            posX += 2;
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
        if(this.stack.Count > 0)
        {
            Cell current = this.stack.Peek();

            bool valid = false;
            int checks = 0;

            //Quitamos los decimales
            // Son las coordenadas del array[width, heigh]
            int current_x = (int)((current.transform.position.x - posXini) / 2);
            int current_y = (int)((current.transform.position.z - posZini) / 2);

            while (checks < 10 && !valid)
            {
                checks++;
                WallOrientation direction = (WallOrientation)Random.Range(0, 4);

                switch (direction)
                {
                    case WallOrientation.WEST:
                        if(current_x > 0)
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

            if(!valid)
                this.stack.Pop();
        }
        else if(!this.characterSpawwned && this.generationFinished)
        {
            this.characterSpawwned = true;
            //this.SpawnPlayer();

            Cell entrada = this.cellGrid[enterX, enterZ];
            entrada.HideWall(WallOrientation.SOUTH);
            Cell salida = this.cellGrid[exitX, exitZ];
            salida.HideWall(WallOrientation.NORTH);
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
