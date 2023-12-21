using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class GridxScript : MonoBehaviour
{
    private GameObject MainGrid;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    private string currentPlayer = "P1";

    int[,] wincheck = new int[3,3];
    
    private bool myturn = false;
    private bool thisiswon=false;

    [SerializeField] GameObject[] gridpeaces; 
    void Start()
    {
        MainGrid = GameObject.FindWithTag("MGrid");
    }
    public void switchgridcolour(Material mat)
    {
        
        for(int i = 0; i < gridpeaces.Length; i++)
        {           
            gridpeaces[i].GetComponent<MeshRenderer>().material = mat;
        }
    }
    public void Paction(Transform transform)
    {
        if (currentPlayer.Equals("P1")){
            GameObject newObject = Instantiate(player1, transform.position, Quaternion.identity);
            newObject.transform.SetParent(this.transform); 
        }
        else if (currentPlayer.Equals("P2"))
        {
            GameObject newObject = Instantiate(player2, transform.position, Quaternion.identity);
            newObject.transform.SetParent(this.transform);
        }
       
        insertArray(transform.localPosition); //it is important to get the koordinates realative to the parent 

        if (checkwin())
        {
            MainGrid.GetComponent<MainGridScript>().gridwin(this.gameObject.transform);
            if (currentPlayer.Equals("P1"))
            {
                GameObject newObject = Instantiate(player1, this.gameObject.transform.position, Quaternion.identity);

                newObject.transform.localScale = new Vector3(4, 4, 4);
            }
            else if (currentPlayer.Equals("P2"))
            {
                GameObject newObject = Instantiate(player2, this.gameObject.transform.position, Quaternion.identity);

                newObject.transform.localScale = new Vector3(4, 4, 4);
            }

            int childCount = this.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Destroy(this.transform.GetChild(i).gameObject);
            }
        }
     }
    
    public void insertArray(Vector3 vector)
    {
        int value = 0;
        if (currentPlayer.Equals("P1"))
        {
            value = 1;
        }
        else if (currentPlayer.Equals("P2"))
        {
            value = -1; 
        }

        int row =1+ (int)vector.y / 2; // divided by 2 because of vector distance of playerobjects ...
        int colum =1+(int)vector.z / 2;



        

        wincheck[colum, row] = value;       

    }
    public bool checkwin()
    {
        bool winbool = false;

       

        int numberOfRows = wincheck.GetLength(0);
        int numberOfColumns = wincheck.GetLength(1);

        int wert; 

        for (int i = 0; i < numberOfRows; i++)
        {
            wert = 0;
            for (int j = 0; j < numberOfColumns; j++)
            {
                wert += wincheck[i, j];               
            }
            if(wert ==-3)
            {
                

                winbool = true;
            }else if(wert ==3)
            {
                
                winbool = true; 
            }

        }

        for (int j = 0; j < numberOfColumns; j++)
        {
            wert = 0;
            for (int i = 0; i < numberOfRows; i++)
            {
                wert += wincheck[i, j];

            }
            if (wert == -3)
            {
               

                winbool = true;
            }
            else if (wert == 3)
            {
              
                winbool = true;
            }
        }
            wert = 0;
            for (int i = 0; i < numberOfRows; i++)
            {
                int j = i; // Die Spalte ist gleich der Zeile für diese diagonale Linie
                wert += wincheck[i, j];
                
            }
            if (wert == -3)
            {
               

                winbool = true;
            }
            else if (wert == 3)
            {
               
                winbool = true;
            }

            wert = 0;
            for (int i = 0; i < numberOfRows; i++)
            {
                int j = numberOfColumns - 1 - i; // Spalte ist umgekehrt zur Zeile für diese diagonale Linie
               wert += wincheck[i, j];
            }
            if (wert == -3)
            {
               

                winbool = true;
            }
            else if (wert == 3)
            {
               
                winbool = true;
            }


        thisiswon = winbool; 
        return winbool;
    }
    public string getcurrentPlayer() { return currentPlayer; }
    public void setcurrentPlayer(string CurrentPlayer) {
        if (CurrentPlayer.Equals("P1") || CurrentPlayer.Equals("P2"))
        {
            currentPlayer = CurrentPlayer;
        }
        else
        {
            Debug.LogWarning("falsche Eingabe im GridxScript"); 
        }
    }

    public bool getmyturn()
    { return myturn; }
    public void setmyturn(bool turn)
    {
        myturn = turn;  
    }
    public bool getthisiswon()
    {
        return thisiswon;
    }
}
