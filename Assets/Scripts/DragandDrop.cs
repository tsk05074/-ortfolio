using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DragandDrop : MonoBehaviour
{
    private bool isDragging;
    private Vector3 currentPosition;

    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;

    int mask = (1 << 8);

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnMouseDown()
    {
        isDragging = true;
        currentPosition = transform.position;
    }

    public void OnMouseUp()
    {
        isDragging = false;

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
