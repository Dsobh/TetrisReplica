using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    [SerializeField]
    private float fallingTime = 1f;
    private float timeToNextFalling;
    public SpawnManager _spawnManager;
    private bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        timeToNextFalling = 0f;
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if(canMove)
        {
            //Movimiento de la pieza hacia la izquierda
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MovePiece(-1);
            }

            //Movimiento de la pieza hacia la derecha
            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                MovePiece(1);

            }
            
            //Rotamos la pieza 90 grados
            if(Input.GetKeyDown(KeyCode.LeftControl))
            {
                //Que puede entrar en un borde despues de rotar
                //Quaternion auxQuaternion = new Quaternion(0, 0,0, -90);
                this.transform.Rotate(new Vector3(0,0,-90));
            }

            //Caída de la pieza
            if(Input.GetKeyDown(KeyCode.DownArrow) || timeToNextFalling>= fallingTime)
            {
                this.gameObject.transform.position = new Vector3(gameObject.transform.position.x, 
                                                                        gameObject.transform.position.y - 1, 
                                                                        gameObject.transform.position.z);
                if(IsValidPosition())
                {
                    UpdateGrid();
                }else{  
                    this.gameObject.transform.position = new Vector3(gameObject.transform.position.x, 
                                                                        gameObject.transform.position.y + 1, 
                                                                        gameObject.transform.position.z);
                    canMove = false;
                    _spawnManager.SpawnPiece();
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
    private void MovePiece(int direction){
        this.gameObject.transform.position = new Vector3(gameObject.transform.position.x + direction, 
                                                                gameObject.transform.position.y, 
                                                                gameObject.transform.position.z);
        if(!IsValidPosition()){
            this.gameObject.transform.position = new Vector3(gameObject.transform.position.x - direction,
                                                                gameObject.transform.position.y, 
                                                                gameObject.transform.position.z);
        }else
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

            if(!GridController.IsInPlayfield(position))
            {
                return false;
            }

            Transform possibleBlock = GridController.blocks[(int)position.x, (int)position.y];
            if(possibleBlock != null && possibleBlock.transform.parent != this.transform)
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
        for(int y = 0; y < GridController.getHeight(); y++) //Recorremos las filas
        {
            for (int x = 0; x < GridController.getWidth(); x++) //Recorremos las columnas
            {
                if(GridController.blocks[x,y] != null)
                {
                    if(GridController.blocks[x,y].parent == this.transform)
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
}
