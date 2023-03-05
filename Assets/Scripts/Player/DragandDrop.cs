using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class DragandDrop : MonoBehaviour
{
    private bool isDragging;
    private bool mouseButtonReleased;
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
        if (gameObject.CompareTag("Player1") || gameObject.CompareTag("Player2") || gameObject.CompareTag("Player3"))
        {
            isDragging = true;
            currentPosition = transform.position;
        }
       
    }

    public void OnMouseUp()
    {
        isDragging = false;
        mouseButtonReleased = true;

        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f, mask))
        {
            if (hit.transform.CompareTag("playerTile"))
            {
                SpawnTower(hit.transform);
            }
            else
            {
                //transform.position = currentPosition;
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
                if (hit.transform.CompareTag("Player3") || hit.transform.CompareTag("Player2") || hit.transform.CompareTag("Player1"))
                {
                    bool isplayer = true;

                    UIManager.Instance.OnPnanel(hit.transform, isplayer);
                }
                //else if (hit.transform.CompareTag("Enemy1") || hit.transform.CompareTag("Enemy2") || hit.transform.CompareTag("Enemy3"))
                //{
                //    Debug.Log("에너미 클릭함");

                //    bool isplayer = false;
                //    UIManager.Instance.OnPnanel(hit.transform, isplayer);

                //}
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            UIManager.Instance.OffPanel();
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        string thisGameobjectName;
        string collisionGameobjectName;

        if (collision.CompareTag("Player3") && isDragging)
        {
            Debug.Log("충돌완료");
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/3"), collision.gameObject.transform.position, Quaternion.identity);
            //Instantiate(obj,transform.position,Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        /*
        thisGameobjectName = gameObject.name.Substring(0, name.Length);
        collisionGameobjectName = collision.gameObject.name.Substring(0, name.Length);

        if (mouseButtonRelease && thisGameobjectName == "Player 3" && thisGameobjectName == collisionGameobjectName)
        {
            Instantiate(Resources.Load("Sprites/Player/3"), transform.position, Quaternion.identity);
            mouseButtonRelease = false;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        */
    }

}
