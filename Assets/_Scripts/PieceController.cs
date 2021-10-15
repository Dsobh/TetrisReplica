using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

enum Estate {
    O = 0,
    R = 90,
    L = -90,
    D = 180};

public class PieceController : MonoBehaviour
{
    [SerializeField]
    private string identity;

    private Estate actualEstate = Estate.O;
    private Estate nextState = Estate.O;

    private static Vector2[,] wallKickDataForRest;
    private static Vector2[,] wallKickDataForI;

    [SerializeField]
    private float originalFallingTime = 1f;
    private float fallingTime = 1f;
    private float fallingTimeFast = 1f/12;

    private float timeToNextFalling;
    public SpawnManager _spawnManager;
    private bool canMove = true;

    private ScoreManager _scoreManager;

    public delegate void _OnLevelChange(int level);
    public static event _OnLevelChange OnLevelChange;
    public UnityEvent OnChange;

    public delegate void _OnNumberOfLinesChange(int linesNumber, int score);
    public static event _OnNumberOfLinesChange OnNumberOfLinesChange;
    public UnityEvent OnLineChange;

    public delegate void _OnGameOverTrigger();
    public static event _OnGameOverTrigger OnGameOverTrigger;
    public UnityEvent OnGameOver;

    // Start is called before the first frame update
    void Start()
    {
        fallingTime = originalFallingTime;
        fallingTimeFast = fallingTime/12;
        timeToNextFalling = 0f;
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        wallKickDataForRest = WallKickData.returnRestData();
        wallKickDataForI = WallKickData.returnIData();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            //Movimiento de la pieza hacia la izquierda
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MovePiece(-1);
            }

            //Movimiento de la pieza hacia la derecha
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MovePiece(1);

            }

            //Rotamos la pieza 90 grados
            if (Input.GetKeyDown(KeyCode.LeftControl) && !identity.Equals("O"))
            {
                bool rotationResult = true;
                Estate aux = nextState;
                //Actualizamos el estado de rotación
                nextState = nextState - 90;
                if(Mathf.Abs((float)nextState) == 180f)
                {
                    nextState = Estate.D;
                }
                Debug.Log(actualEstate + "-> " + nextState);
                //Rotamos
                this.transform.Rotate(new Vector3(0, 0, 90));
                if(IsValidPosition())
                {
                    UpdateGrid();
                     actualEstate = nextState;
                }else
                {
                    //Aplicamos Wallkick
                    if(identity == "I")
                    {
                        rotationResult = TryWallKick(wallKickDataForI);
                    }else
                    {
                        rotationResult = TryWallKick(wallKickDataForRest);
                    }

                    //Si no hay un wallkick satisfactorio, no rotamos
                    if(!rotationResult)
                    {
                        this.transform.Rotate(new Vector3(0, 0, -90));
                        nextState = aux;
                    }else
                    {
                        actualEstate = nextState;
                        UpdateGrid();
                    }
                }
            }

            if(Input.GetKey(KeyCode.DownArrow))
            {
                fallingTime = fallingTimeFast;
            }

            if(Input.GetKeyUp(KeyCode.DownArrow))
            {
                fallingTime = originalFallingTime;
            }

            //Caída de la pieza
            if (timeToNextFalling >= fallingTime)
            {
                this.gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                                                                        gameObject.transform.position.y - 1,
                                                                        gameObject.transform.position.z);
                if (IsValidPosition())
                {
                    UpdateGrid();
                }
                else
                {
                    CheckGameOver();
                    this.gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                                                                        gameObject.transform.position.y + 1,
                                                                        gameObject.transform.position.z);
                    canMove = false;
                    _spawnManager.SpawnPiece();
                    DeleteFullRows();
                    UpdateLevel();
                }
                timeToNextFalling = 0f;
            }
            timeToNextFalling += Time.deltaTime;
        }
    }

    /// <summary>
    /// Mueve la pieza hacia los lados
    /// </summary>
    /// <param name="direction">Indica la dirección hacia la que se tiene que mover la pieza.
    /// 1 -> Derecha
    /// -1 -> Izquierda</param>
    private void MovePiece(int direction)
    {
        this.gameObject.transform.position = new Vector3(gameObject.transform.position.x + direction,
                                                                gameObject.transform.position.y,
                                                                gameObject.transform.position.z);
        if (!IsValidPosition())
        {
            this.gameObject.transform.position = new Vector3(gameObject.transform.position.x - direction,
                                                                gameObject.transform.position.y,
                                                                gameObject.transform.position.z);
        }
        else
        {
            UpdateGrid();
        }
    }

    /// <summary>
    /// Comprueba si la posición a la que se va a mover la pieza es válida
    /// </summary>
    /// <param name="nextMove">Vector2 que indica hacia donde se moverá la pieza</param>
    /// <returns>Devuelve true si la pieza puede moverse y false si no puede moverse</returns>
    private bool IsValidPosition()
    {
        foreach (Transform block in this.transform)
        {
            Vector2 position = (GridController.RoundVector(block.position));

            if (!GridController.IsInPlayfield(position))
            {
                return false;
            }

            Transform possibleBlock = GridController.blocks[(int)position.x, (int)position.y];
            if (possibleBlock != null && possibleBlock.transform.parent != this.transform)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Método para actualizar la disposición de la pieza en el grid.
    /// </summary>
    private void UpdateGrid()
    {
        //Borrar la pieza actual del grid
        for (int y = 0; y < GridController.getHeight(); y++) //Recorremos las filas
        {
            for (int x = 0; x < GridController.getWidth(); x++) //Recorremos las columnas
            {
                if (GridController.blocks[x, y] != null)
                {
                    if (GridController.blocks[x, y].parent == this.transform)
                    {
                        GridController.blocks[x, y] = null;
                    }
                }
            }
        }

        //Actualizar grid
        foreach (Transform block in this.transform)
        {
            //Aquí el ajuste no es necesario porque siempre trabajamos con las coordenadas del escenario
            Vector2 position = GridController.RoundVector(block.position);
            GridController.blocks[(int)position.x, (int)position.y] = block;
            //La x son las columnas, las y son las filas
        }
    }

    private void DeleteFullRows()
    {
        for (int y = 0; y < GridController.getHeight(); y++) //Recorremos las filas
        {
            if (IsRowFull(y))
            {
                DeleteRow(y);
                _scoreManager.IncreaseCombo();
                DecreaseSuperiorRows(y + 1);
                y--;
            }
        }
        _scoreManager.calcCombo();
        OnLineChange.Invoke();
        if (OnNumberOfLinesChange != null)
        {
            OnNumberOfLinesChange(_scoreManager.linesCounter, _scoreManager.score);
        }
        _scoreManager.ResetCombo();
    }

    private bool IsRowFull(int row)
    {
        for (int x = 0; x < GridController.getWidth(); x++) //Recorremos las columnas
        {
            if (GridController.blocks[x, row] == null)
            {
                return false;
            }
        }
        return true;
    }

    private void DeleteRow(int row)
    {
        for (int x = 0; x < GridController.getWidth(); x++) //Recorremos las columnas
        {
            Destroy(GridController.blocks[x, row].gameObject);
            GridController.blocks[x, row] = null;
        }
        _scoreManager.IncreaseLine();
    }

    private void DecreaseSuperiorRows(int row)
    {
        for (int y = row; y < GridController.getHeight(); y++) //Recorremos las filas
        {
            DecreaseRow(y);
        }
    }

    private void DecreaseRow(int row)
    {
        for (int x = 0; x < GridController.getWidth(); x++) //Recorremos las columnas
        {
            if (GridController.blocks[x, row] != null)
            {
                GridController.blocks[x, row - 1] = GridController.blocks[x, row];
                GridController.blocks[x, row - 1].position += new Vector3(0, -1, 0);
                GridController.blocks[x, row] = null;
            }
        }
    }

    private void CheckGameOver()
    {
        if (!IsValidPosition() && this.transform.position.y >= 20)
        {
            Debug.Log("Game Over");
            Time.timeScale = 0;
            Destroy(this.gameObject);
            OnGameOver.Invoke();
            if (OnGameOverTrigger != null)
            {
                OnGameOverTrigger();
            }
        }
    }

    private void UpdateLevel()
    {

        if (_scoreManager.linesCounter >= _scoreManager.levelCounter * 10)
        {
            OnChange.Invoke();
            _scoreManager.IncreaseLevel();
            if (OnLevelChange != null)
            {
                OnLevelChange(_scoreManager.levelCounter);
            }
        }
    }

    private bool TryWallKick(Vector2[,] data)
    {
        int dataRow = 0;

        //Comprobamos que fila de rotaciones debemos probar
        if(actualEstate == Estate.O)
        {
            if(nextState == Estate.R)
            {
                dataRow = 0; //Primera fila
            }else
            {
                dataRow = 7;
            }
        }else if(actualEstate == Estate.R)
        {
            if(nextState == Estate.O)
            {
                dataRow = 1; //Primera fila
            }else
            {
                dataRow = 2;
            }
        }else if(actualEstate == Estate.L)
        {
            if(nextState == Estate.O)
            {
                dataRow = 6; //Primera fila
            }else
            {
                dataRow = 5;
            }
        }else
        {
            if(nextState == Estate.R)
            {
                dataRow = 3; //Primera fila
            }else
            {
                dataRow = 4;
            }
        }

        for(int i=0; i<5; i++)
        {
            Vector2 movement = data[dataRow, i];
            this.gameObject.transform.position += new Vector3(movement.x, movement.y, 0);
            if(!IsValidPosition())
            {
                //Debug.Log(movement.x + " " + movement.y);
                this.gameObject.transform.position += new Vector3(-movement.x, -movement.y, 0);
            }else
            {
                return true;
            }
        }
        return false;

    }

}
