using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DragandDrop : MonoBehaviour
{
    private bool isDragging;
    private Vector3 resetPostion;
    private Vector3 currentPosition;

    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;

    int mask = (1 << 8);

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void Start()
    {
        resetPostion = this.transform.position;
    }

    public void OnMouseDown()
    {
        isDragging = true;
        currentPosition = transform.position;
    }

    public void OnMouseOver()
    {
        
    }

    public void OnMouseUp()
    {
        isDragging = false;


        //ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        //if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        //{
        //    if (hit.transform.CompareTag("playerTile"))
        //    {
        //        Debug.Log("playerTile 히트했음");
        //        SpawnTower(hit.transform);
        //    }
        //    else
        //    {
        //        transform.position = currentPosition;
        //        Debug.Log("다른거 히트함");
        //    }
        //}

        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f, mask))
        {
            if (hit.transform.CompareTag("playerTile"))
            {
                Debug.Log("playerTile 히트했음");
                SpawnTower(hit.transform);
            }
            else
            {
                transform.position = currentPosition;
                Debug.Log("다른거 히트함");
            }
        }
    }

    public void SpawnTower(Transform tileTransform)
    {
        //Tile tile = tileTransform.GetComponent<Tile>();
        Debug.Log("tileTransform childCout" + tileTransform.transform.childCount);
        if (tileTransform.transform.childCount == 0)
        {
            transform.position = new Vector3(tileTransform.transform.localPosition.x, tileTransform.transform.localPosition.y, tileTransform.transform.localPosition.z);

            transform.SetParent(tileTransform);
            Debug.Log("배치완료");
        }
        else
        {
            transform.position = currentPosition;
        }
     
        /*
        if (currentDistance < closestDistance)
        {
            Debug.Log("배치완료");
            this.transform.position = new Vector3(tileTransform.transform.position.x, tileTransform.transform.position.y, tileTransform.transform.position.z);

        }
        */

        /*
        if (tile.IsBuildTower == true)
        {
            transform.position = resetPostion;
            return;
        }
        else
        {
            if (Mathf.Abs(this.transform.localPosition.x - tileTransform.transform.localPosition.x) <= 0.5f &&
              Mathf.Abs(this.transform.localPosition.y - tileTransform.transform.localPosition.y) <= 0.5f)
            {
                this.transform.position = new Vector3(tileTransform.transform.position.x, tileTransform.transform.position.y, tileTransform.transform.position.z);
            }
            transform.SetParent(tileTransform);
        }
        */
        /*
               if (Mathf.Abs(this.transform.localPosition.x - tileTransform.transform.localPosition.x) <= 0.2f &&
               Mathf.Abs(this.transform.localPosition.y - tileTransform.transform.localPosition.y) <= 0.2f)
               {
                   if (tileTransform.transform.childCount == 0)
                   {
                       Debug.Log(tileTransform.transform.childCount);

                       this.transform.position = new Vector3(tileTransform.transform.position.x, tileTransform.transform.position.y, tileTransform.transform.position.z);
                       transform.SetParent(tileTransform);
                       Debug.Log(tileTransform.transform.childCount);
                   }
                   else
                   {
                       transform.position = resetPostion;

                   }
               }
               */
        //if (tileTransform.transform.childCount == 0)
        //{
        //    transform.SetParent(tileTransform);
        //    transform.position = new Vector3(tileTransform.transform.position.x, tileTransform.transform.position.y, tileTransform.transform.position.z);
        //    Debug.Log("배치완료");
        //}
        //else
        //{
        //    transform.position = resetPostion;

        //}

    }

    void Update()
    {
        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }


    }
}
