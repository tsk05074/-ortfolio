using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

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
                SpawnTower(hit.transform);
            }
            else
            {
                transform.position = currentPosition;
            }
        }
    }

    public void SpawnTower(Transform tileTransform)
    {
        //Tile tile = tileTransform.GetComponent<Tile>();
        if (tileTransform.transform.childCount == 0)
        {
            transform.position = new Vector3(tileTransform.transform.localPosition.x, tileTransform.transform.localPosition.y, tileTransform.transform.localPosition.z);

            transform.SetParent(tileTransform);
        }
        else
        {
            transform.position = currentPosition;
        }
    }

    void Update()
    {
        //마우스가 UI에 머물러 있으 ㄹ때는 아래 코드가 실행되지 않도록 함
        if (EventSystem.current.IsPointerOverGameObject() == true)
        {
            return;
        }

        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }

        if (Input.GetMouseButtonDown(0))
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.CompareTag("Player3"))
                {
                    UIManager.Instance.OnPnanel(hit.transform);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            UIManager.Instance.OffPanel();
        }
     
    }
}
