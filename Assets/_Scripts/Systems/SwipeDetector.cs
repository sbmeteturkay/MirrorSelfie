using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

// This class is used to manage the grid UI meaning position the tails, resize them, and create the grid seen by the user
public class SwipeDetector : MonoBehaviour, IDragHandler, IEndDragHandler
{

    //pass this event on arrow spwaner
    public static event Action<DraggedDirection> OnDragEvent;


    #region FIELDS
    private Grid grid;

    public enum DraggedDirection
    {
        Up,
        Down,
        Right,
        Left
    }
    #endregion

    #region  IDragHandler - IEndDragHandler
    public void OnEndDrag(PointerEventData eventData)
    {
       // Debug.Log("Press position + " + eventData.pressPosition);
        //Debug.Log("End position + " + eventData.position);
        Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;
        //Debug.Log("norm + " + dragVectorDirection);
       OnDragEvent?.Invoke(GetDragDirection(dragVectorDirection));
    }

    //It must be implemented otherwise IEndDragHandler won't work 
    public void OnDrag(PointerEventData eventData)
    {

    }

    private DraggedDirection GetDragDirection(Vector3 dragVector)
    {
        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);
        DraggedDirection draggedDir;
        if (positiveX > positiveY)
        {
            draggedDir = (dragVector.x > 0) ? DraggedDirection.Right : DraggedDirection.Left;
        }
        else
        {
            draggedDir = (dragVector.y > 0) ? DraggedDirection.Up : DraggedDirection.Down;
        }
        Debug.Log(draggedDir);
        return draggedDir;
    }
    #endregion
}