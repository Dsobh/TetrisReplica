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
            //Caída de la pieza
            if(timeToNextFalling>= fallingTime)
            {
                if(IsValidPosition(new Vector2(0, -1)))
                {
                    this.gameObject.transform.position = new Vector3(gameObject.transform.position.x, 
                                                                        gameObject.transform.position.y - 1, 
                                                                        gameObject.transform.position.z);
                }else{
                    canMove = false;
                    Debug.Log("Estoy en el else del pieceController");
                    _spawnManager.SpawnPiece();
                }
                timeToNextFalling = 0f;
            }
            timeToNextFalling += Time.deltaTime;

        
            //Movimiento de la pieza hacia la izquierda
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MovePiece(-1);
                UpdateGrid();
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
        }
    }

    /// <summary>
    /// Mueve la pieza hacia los lados
    /// </summary>
    /// <param name="direction">Indica la dirección hacia la que se tiene que mover la pieza.
    /// 1 -> Derecha
    /// -1 -> Izquierda</param>
    private void MovePiece(int direction){
        if(IsValidPosition(new Vector2(direction, 0))){
            this.gameObject.transform.position = new Vector3(gameObject.transform.position.x + direction, 
                                                                gameObject.transform.position.y, 
                                                                gameObject.transform.position.z);
        }
    }

    /// <summary>
    /// Comprueba si la posición a la que se va a mover la pieza es válida
    /// </summary>
    /// <param name="nextMove">Vector2 que indica hacia donde se moverá la pieza</param>
    /// <returns>Devuelve true si la pieza puede moverse y false si no puede moverse</returns>
    private bool IsValidPosition(Vector2 nextMove)
    {

        foreach (Transform block in this.transform)
        {
            Vector2 nextPosition = (GridController.RoundVector(block.position) + nextMove);

            if(!GridController.IsInPlayfield(nextPosition))
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
        for(int i = 0; i < GridController.getHeight(); i++) //Recorremos las filas
        {
            for (int j = 0; j < GridController.getWidth(); j++) //Recorremos las columnas
            {
                if(i <= GridController.getMaxHeight()) //Comprobamos que estamos en el PlayField y no en la zona del spawn
                {
                    GameObject cell = GridController.getCell(i, j);
                    if(cell != null){
                        if(cell.transform.parent == this){
                            //Tenemos que ajustar la celda porque en la matriz el 0,0 empieza arriba y en el escenario empieza abajo
                            GridController.setCell(GridController.getMaxHeight() - i, j, null);
                        }
                    }
                }
            }
        }

        //Actualizar grid
        foreach (Transform block in this.transform)
        {
            if((int)block.transform.position.y <= GridController.getMaxHeight()) //Comprobamos que estamos en el PlayField y no en la zona del spawn
            {
                Debug.Log((int)block.transform.position.x + ", " + (int)block.transform.position.y);
                //Aquí el ajuste no es necesario porque siempre trabajamos con las coordenadas del escenario
                GridController.setCell((int)block.transform.position.y, (int)block.transform.position.x, block.gameObject);
                //La x son las columnas, las y son las filas
            }
        }
    }
}
