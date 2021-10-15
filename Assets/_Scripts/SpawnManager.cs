using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] pieces;
    [SerializeField]
    private Vector3[] piecesOffset;
    [SerializeField]
    private ArrayList bag = new ArrayList();
    [SerializeField]
    private Transform UIPosition;
    private int nextPieceIndex;
    private GameObject newPieceAux;
    private GameObject newPiece;

    void Start() {
        nextPieceIndex = Random.Range(0, 6);
        SpawnPiece();
        FillBag();
    }

    /// <summary>
    /// Hace Spawn de una pieza según el nextPieceIndex. Se llama desde PieceController.
    /// </summary>
    public void SpawnPiece()
    {
        GameObject piece = pieces[nextPieceIndex];
        nextPieceIndex = Random.Range(0, 6);
        GameObject pieceInstance = Instantiate(piece, this.transform.position, piece.transform.rotation);
        pieceInstance.SetActive(true);
        Destroy(newPiece);
        SpawnUI();
    }

    /// <summary>
    /// Hace spawn de la siguiente pieza en la UI. Deshabilita el script de la pieza.
    /// </summary>
    private void SpawnUI()
    {
        GameObject newPieceAux = pieces[nextPieceIndex];
        newPiece = Instantiate(newPieceAux, (UIPosition.position  + piecesOffset[nextPieceIndex]), newPieceAux.transform.rotation);
        newPiece.GetComponent<PieceController>().enabled = false;
        newPiece.SetActive(true);
    }

    private void FillBag()
    {
        GameObject piece;
        bool pieceExist = false;
      /* while(bag.Count < 7)
        {
            piece = pieces[Random.Range(0,6)];
            foreach (GameObject p in bag)
            {
                if(piece.name == p.name)
                {
                    pieceExist = true;
                }
            }
            Debug.Log(piece.name);
            if(pieceExist == false)
            {
                bag.Add(piece);
            }
        }*/
    }
}
