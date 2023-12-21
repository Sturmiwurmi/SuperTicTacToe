using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGridScript : MonoBehaviour
{

    [SerializeField] private Material black;
    [SerializeField] private Material yellow; 

    [SerializeField] private GameObject[] Gridbars;
    [SerializeField] private Material Player1Mat;
    [SerializeField] private Material Player2Mat;

    [SerializeField] private GameObject[] TTTs; 

    private string currentPlayer = "P1";

    int[,] mainwincheck = new int[3, 3];

    //für die winanimation
    float delayBetweenActions = 1.0f;
    int currentIndex = 0;
    void Start()
    {
        
        for (int i = 0; i < Gridbars.Length; i++)
        {
            Gridbars[i].GetComponent<MeshRenderer>().material = Player1Mat;
        }

        for(int i = 0;i < TTTs.Length; i++)
        {
            TTTs[i].GetComponent<GridxScript>().setmyturn(true);
            TTTs[i].GetComponent<GridxScript>().switchgridcolour(yellow); 
        }
        
    }
    public void setnextgrid(Vector3 vec)
    {
        for (int i = 0; i < TTTs.Length; i++)
        {
            string tagcomparison = vec.y/2 + " " + vec.z/2;
            if (TTTs[i].gameObject.tag.Equals(tagcomparison))
            {
                if (!TTTs[i].gameObject.GetComponent<GridxScript>().getthisiswon())
                {
                    TTTs[i].GetComponent<GridxScript>().setmyturn(true);
                    TTTs[i].GetComponent<GridxScript>().switchgridcolour(yellow);
                }
                else
                {
                  
                    for (int j = 0; j < TTTs.Length; j++)
                    {
                        if (!TTTs[j].gameObject.tag.Equals(tagcomparison)&& !TTTs[j].gameObject.GetComponent<GridxScript>().getthisiswon())
                        {
                            TTTs[j].GetComponent<GridxScript>().setmyturn(true);
                            TTTs[j].GetComponent<GridxScript>().switchgridcolour(yellow);
                        }
                    }

                }
                
            }

           
          
        }
    }
    public void action()
    {
        for (int i = 0; i < TTTs.Length; i++)
        {
            if (!TTTs[i].gameObject.GetComponent<GridxScript>().getthisiswon())
            {
                TTTs[i].GetComponent<GridxScript>().setmyturn(false);
                TTTs[i].GetComponent<GridxScript>().switchgridcolour(black);
            }
        }

        switchcurrentPlayer(); 
    }
    public void gridwin(Transform gridtransform)
    {
        Vector3 vector = gridtransform.localPosition; 
        int value = 0;
        if (currentPlayer.Equals("P1"))
        {
            value = 1;
        }
        else if (currentPlayer.Equals("P2"))
        {
            value = -1;
        }

        int row = 1 + (int)vector.y / 6; // divided by 6 because of vector distance of grids ...
        int colum = 1 + (int)vector.z / 6;

        mainwincheck[colum, row] = value;

        if (CheckMainGridWin())
        {
            Debug.Log(currentPlayer + " hat gewonnen"); 
            for (int i = 0; i < TTTs.Length; i++)
            {
                TTTs[i].GetComponent<GridxScript>().setmyturn(false);
            }

            int childCount = this.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Destroy(this.transform.GetChild(i).gameObject);
            }
            Winanimation(); 
        } 
    }
    public void Winanimation()
    {
        InvokeRepeating("Winanimation2", 0, delayBetweenActions);

       
    }
    private void Winanimation2()
    {
       
        if (currentIndex < Gridbars.Length)
        {
           
            if (currentPlayer.Equals("P1")) // weil der aktuelle spieler nach spielende eigentlich an der reihe gewesen wäre 
            {                               // hat der spieler gewonnen, der davor an der reihe gewesen ist. der 
                Gridbars[currentIndex].GetComponent<MeshRenderer>().material = Player2Mat;
            } else if (currentPlayer.Equals("P2"))
            {
                Gridbars[currentIndex].GetComponent<MeshRenderer>().material = Player1Mat;
            }
                currentIndex++;
        }
        else
        {
            // Alle Aktionen wurden durchgeführt, beende das Wiederholen
            CancelInvoke("PerformActionWithDelay");
        }
    }
    public bool CheckMainGridWin()
    {
        bool winbool = false;



        int numberOfRows = mainwincheck.GetLength(0);
        int numberOfColumns = mainwincheck.GetLength(1);

        int wert;

        for (int i = 0; i < numberOfRows; i++)
        {
            wert = 0;
            for (int j = 0; j < numberOfColumns; j++)
            {
                wert += mainwincheck[i, j];
            }
            if (wert == -3)
            {
                

                return true;
            }
            else if (wert == 3)
            {
              
                return true;
            }

        }

        for (int j = 0; j < numberOfColumns; j++)
        {
            wert = 0;
            for (int i = 0; i < numberOfRows; i++)
            {
                wert += mainwincheck[i, j];

            }
            if (wert == -3)
            {
              

                return true;
            }
            else if (wert == 3)
            {
                
                return true;
            }
        }
        wert = 0;
        for (int i = 0; i < numberOfRows; i++)
        {
            int j = i; // Die Spalte ist gleich der Zeile für diese diagonale Linie
            wert += mainwincheck[i, j];

        }
        if (wert == -3)
        {
           

            return true;
        }
        else if (wert == 3)
        {
            
            return true;
        }

        wert = 0;
        for (int i = 0; i < numberOfRows; i++)
        {
            int j = numberOfColumns - 1 - i; // Spalte ist umgekehrt zur Zeile für diese diagonale Linie
            wert += mainwincheck[i, j];
        }
        if (wert == -3)
        {
           

            return true;
        }
        else if (wert == 3)
        {
         
            return true;
        }



        return winbool;
    }
    private void switchcurrentPlayer()
    {
        if (currentPlayer.Equals("P1"))
        {
            currentPlayer = "P2";
        }
        else if(currentPlayer.Equals("P2"))
        {
            currentPlayer = "P1";
        }
        swapgridcolour();
        for (int i = 0; i < TTTs.Length; i++)
        {
            TTTs[i].GetComponent<GridxScript>().setcurrentPlayer(currentPlayer);
        }
    }
    public void swapgridcolour()
    {
        if (currentPlayer.Equals("P1"))
        {
            for (int i = 0; i < Gridbars.Length; i++)
            {
                Gridbars[i].GetComponent<MeshRenderer>().material = Player1Mat;
            }
        }
        else if (currentPlayer.Equals("P2"))
        {
            for (int i = 0; i < Gridbars.Length; i++)
            {
                Gridbars[i].GetComponent<MeshRenderer>().material = Player2Mat;
            }
        }
    }
    void Update()
    {
        
    }
}
