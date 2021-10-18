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
    private List<int> bag = new List<int>(7);
    [SerializeField]
    private Transform UIPosition;
    private int offsetIndex;
    private GameObject newPiece;

    private GameObject nextPiece;

    void Start()
    {
        FillBag();
        nextPiece = GetOnePiece();
        SpawnPiece();
    }

    /// <summary>
    /// Hace Spawn de una pieza según la nextPiece. Se llama desde PieceController.
    /// </summary>
    public void SpawnPiece()
    {

        GameObject pieceInstance = Instantiate(nextPiece, this.transform.position, nextPiece.transform.rotation);
        pieceInstance.SetActive(true);
        Destroy(newPiece);
        SpawnUI();
    }

    /// <summary>
    /// Hace spawn de la siguiente pieza en la UI. Deshabilita el script de la pieza.
    /// </summary>
    private void SpawnUI()
    {
        nextPiece = GetOnePiece();
        newPiece = Instantiate(nextPiece, (UIPosition.position + piecesOffset[offsetIndex]), nextPiece.transform.rotation);
        newPiece.GetComponent<PieceController>().enabled = false;
        newPiece.SetActive(true);
    }

    //Llenamos la bolsa
    private void FillBag()
    {
        for (int i = 0; i < 7; i++)
        {
            bag.Add(i);
        }
    }

    private GameObject GetOnePiece()
    {
        if (bag.Count <= 0)
        {
            FillBag();
        }
        int index = Random.Range(0, bag.Count - 1);
        offsetIndex = bag[index];
        GameObject piece = pieces[offsetIndex];
        bag.RemoveAt(index);
        return piece;
    }
}