using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DragandDrop : MonoBehaviour
{
    private bool isDragging;
    private Vector3 resetPostion;
    private Vector3 prePostion;

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
        prePostion = transform.position;
    }
    public void OnMouseDown()
    {
        isDragging = true;
    }

    public void OnMouseUp()
    {
        isDragging = false;

        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.CompareTag("playerTile"))
            {
                SpawnTower(hit.transform);
            }
        }
    }

    public void SpawnTower(Transform tileTransform)
    {
        Tile tile = tileTransform.GetComponent<Tile>();

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
            tile.IsBuildTower = true;
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
