using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapController : MonoBehaviour
{
    
    //public List<Transform> snapPoints;
    //public List<DragAndDrop1> draggableObjects;
    //public float snapRange = 0.5f;

    //void Start()
    //{
    //    foreach (DragAndDrop1 draggable in draggableObjects)
    //    {
    //        draggable.dragEndedCallback = OnDragEnded;
    //    }
    //}

    //void OnDragEnded(DragAndDrop1 draggable)
    //{
    //    float closestDistance = -1;
    //    Transform closestSnapPoint = null;

    //    foreach (Transform snapPoint in snapPoints)
    //    {
    //        float currentDistance = Vector2.Distance(draggable.transform.localPosition, snapPoint.localEulerAngles);
    //        if (closestSnapPoint == null || currentDistance < closestDistance)
    //        {
    //            Debug.Log("배치완료");
    //            closestSnapPoint = snapPoint;
    //            closestDistance = currentDistance;
    //        }
    //    }

    //    if (closestSnapPoint != null && closestDistance <= snapRange)
    //    {
    //        draggable.transform.localPosition = closestSnapPoint.localPosition;
    //    }
    //}
    
}
