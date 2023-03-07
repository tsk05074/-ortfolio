using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using DG.Tweening;

public class DragandDrop : MonoBehaviour
{
    public bool isDragging;
    public bool mouseButtonReleased;
    private Vector3 currentPosition;

    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;

    private PlayerSpawner playerSpawner;
    private WaveSpawner enemySpawner;

    int mask = (1 << 8);

    private void Awake()
    {
        mainCamera = Camera.main;
        playerSpawner = GameObject.Find("PlayerSpawner").GetComponent<PlayerSpawner>();
        enemySpawner = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();

    }

    public void OnMouseDown()
    {
        if (gameObject.CompareTag("Player1") || gameObject.CompareTag("Player2") || gameObject.CompareTag("Player3") ||
            gameObject.CompareTag("Player4") || gameObject.CompareTag("Player5") || gameObject.CompareTag("Player6") ||
                gameObject.CompareTag("Player7") || gameObject.CompareTag("Player8") || gameObject.CompareTag("Player9"))
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
                if (hit.transform.CompareTag("Player3") || hit.transform.CompareTag("Player2") || hit.transform.CompareTag("Player1") || 
                    hit.transform.CompareTag("Player4") || hit.transform.CompareTag("Player5") || hit.transform.CompareTag("Player6")
                    || hit.transform.CompareTag("Player7") || hit.transform.CompareTag("Player8") || hit.transform.CompareTag("Player9"))
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

    void OnTriggerStay(Collider collision)
    {
        if (this.gameObject.CompareTag("Player1") && this.gameObject.CompareTag("Player1") == collision.CompareTag("Player1") && !isDragging)
        {
            isDragging = false;
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/Player 4"), transform.position, Quaternion.identity);
            //obj.transform.SetParent(collision.transform);
            obj.transform.parent = gameObject.transform.parent;
            obj.GetComponent<Weapon>().Setup(enemySpawner);


            //obj.transform.SetParent(playerSpawner.playerZone.t);
            //obj.GetComponent<Weapon>().Setup(enemySpawner);
            //Instantiate(obj,transform.position,Quaternion.identity);
            obj.transform.DOShakeScale(1.5f);

        }
        else if (this.gameObject.CompareTag("Player2") && this.gameObject.CompareTag("Player2") == collision.CompareTag("Player2") && !isDragging)
        {
            isDragging = false;
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/Player 5"), transform.position, Quaternion.identity);
            //obj.transform.SetParent(collision.transform);
            obj.transform.parent = gameObject.transform.parent;
            obj.GetComponent<Weapon>().Setup(enemySpawner);
            obj.transform.DOShakeScale(1.5f);

            //Instantiate(obj,transform.position,Quaternion.identity);

        }
        else if (this.gameObject.CompareTag("Player3") && this.gameObject.CompareTag("Player3") == collision.CompareTag("Player3") && !isDragging)
        {
            isDragging = false;
            Destroy(collision.gameObject);
            Destroy(this.gameObject);

            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/Player 6"), transform.position, Quaternion.identity);
            //obj.transform.SetParent(collision.transform);
            obj.transform.parent = gameObject.transform.parent;
            obj.GetComponent<Weapon>().Setup(enemySpawner);

            obj.transform.DOShakeScale(1.5f);


            //Instantiate(obj,transform.position,Quaternion.identity);
        }
        else if (this.gameObject.CompareTag("Player4") && this.gameObject.CompareTag("Player4") == collision.CompareTag("Player4") && !isDragging)
        {
            isDragging = false;
            Destroy(collision.gameObject);
            Destroy(this.gameObject);

            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/Player 7"), transform.position, Quaternion.identity);
            //obj.transform.SetParent(collision.transform);
            obj.transform.parent = gameObject.transform.parent;
            obj.GetComponent<Weapon>().Setup(enemySpawner);

            obj.transform.DOShakeScale(1.5f);


            //Instantiate(obj,transform.position,Quaternion.identity);
        }
        else if (this.gameObject.CompareTag("Player5") && this.gameObject.CompareTag("Player5") == collision.CompareTag("Player5") && !isDragging)
        {
            isDragging = false;
            Destroy(collision.gameObject);
            Destroy(this.gameObject);

            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/Player 8"), transform.position, Quaternion.identity);
            //obj.transform.SetParent(collision.transform);
            obj.transform.parent = gameObject.transform.parent;
            obj.GetComponent<Weapon>().Setup(enemySpawner);

            obj.transform.DOShakeScale(1.5f);


            //Instantiate(obj,transform.position,Quaternion.identity);
        }
        else if (this.gameObject.CompareTag("Player6") && this.gameObject.CompareTag("Player6") == collision.CompareTag("Player6") && !isDragging)
        {
            isDragging = false;
            Destroy(collision.gameObject);
            Destroy(this.gameObject);

            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/Player 9"), transform.position, Quaternion.identity);
            //obj.transform.SetParent(collision.transform);
            obj.transform.parent = gameObject.transform.parent;
            obj.GetComponent<Weapon>().Setup(enemySpawner);

            obj.transform.DOShakeScale(1.5f);


            //Instantiate(obj,transform.position,Quaternion.identity);
        }
    }
}
