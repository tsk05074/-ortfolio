using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragandDrop : MonoBehaviour
{
    private bool isDragging;
    private Vector3 resetPostion;
    private Vector3 playerPosition;

    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void Start()
    {
        resetPostion = this.transform.position;
        //playerPosition = new Vector3();
    }
    public void OnMouseDown()
    {
        isDragging = true;
    }

    public void OnMouseUp()
    {
        isDragging = false;

        /*
        if (Mathf.Abs(this.transform.localPosition.x - playerTilemap.transform.localPosition.x) <= 0.5f &&
            Mathf.Abs(this.transform.localPosition.y - playerTilemap.transform.localPosition.y) <= 0.5f)
        {
            this.transform.position = new Vector3(playerTilemap.transform.position.x, playerTilemap.transform.position.y,playerTilemap.transform.position.z);
        }
        else
        {
            this.transform.localPosition = new Vector3(resetPostion.x, resetPostion.y, resetPostion.z);
        }
        */

       
        
    }
        void Update()
        {
        /*
            if (isDragging)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                transform.Translate(mousePosition);
            }
        */

        if (Input.GetMouseButtonDown(0))
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.CompareTag("playerTile"))
                {
                    Debug.Log("gdgd");
                   
                }
            }
        }

    }

    private void OnMouseOver()
    {
    }

}
