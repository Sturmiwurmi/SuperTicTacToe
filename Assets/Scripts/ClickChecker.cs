using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickChecker : MonoBehaviour
{
    private GridxScript myGrid;
    private GameObject MainGrid; 
    void Start()
    {
        myGrid =this.GetComponentInParent<GridxScript>();
        MainGrid = GameObject.FindWithTag("MGrid"); 
    }
    private void OnMouseDown()
    {
        if (myGrid.getmyturn()&&!myGrid.getthisiswon())
        {
            myGrid.Paction(this.transform);
            Destroy(this.gameObject);

            MainGrid.GetComponent<MainGridScript>().action();
            MainGrid.GetComponent<MainGridScript>().setnextgrid(this.transform.localPosition); 
        }
    }
    
    void Update()
    {
        
    }
}
