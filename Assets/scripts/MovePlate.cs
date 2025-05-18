using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;

    //Location on the board
    int matrixX;
    int matrixY;

    //false: movement, true: attacking
    public bool attack = false;

    public void Start()
    {
        if (attack)
        {
            //Set to red
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        //Destroy the victim Chesspiece
        if (attack)
        {
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

            if (cp.name == "white_king") controller.GetComponent<Game>().Winner("Black Wins!");
            if (cp.name == "black_king") controller.GetComponent<Game>().Winner("White Wins!");

            Destroy(cp);
        }

        //Set the Chesspiece's original location to be empty
        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(), 
            reference.GetComponent<Chessman>().GetYBoard());

        //Move reference chess piece to this position
        reference.GetComponent<Chessman>().SetXBoard(matrixX);
        reference.GetComponent<Chessman>().SetYBoard(matrixY);
        reference.GetComponent<Chessman>().SetCoords();

        //Update the matrix
        controller.GetComponent<Game>().SetPosition(reference);

        //Switch Current Player
        controller.GetComponent<Game>().NextTurn();

        //Destroy the move plates including self
        reference.GetComponent<Chessman>().DestroyMovePlates();
    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }
}