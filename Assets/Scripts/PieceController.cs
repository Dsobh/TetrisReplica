using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    [SerializeField]
    private float fallingTime = 1f;
    private float timeToNextFalling;

    // Start is called before the first frame update
    void Start()
    {
        timeToNextFalling = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Caída de la pieza
        if(timeToNextFalling>= fallingTime)
        {
            this.gameObject.transform.position = new Vector3(gameObject.transform.position.x, 
                                                             gameObject.transform.position.y - 1, 
                                                             gameObject.transform.position.z);
            timeToNextFalling = 0f;
        }
        timeToNextFalling += Time.deltaTime;
    
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
            Quaternion auxQuaternion = new Quaternion(0, 0,0, -90);
            //this.gameObject.transform.rotation = new Quaternion(0, 0,0, -90);
            this.transform.Rotate(new Vector3(0,0,-90));
            Debug.Log(this.gameObject.transform.rotation.z);
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
    }
}
