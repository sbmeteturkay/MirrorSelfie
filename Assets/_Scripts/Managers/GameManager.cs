using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [Header("Arrow Spawn")]
    [SerializeField] GameObject spawnParent;
    [SerializeField] GameObject roller;
    [SerializeField] GameObject arrowPefab;
    [SerializeField] int spawnNumber = 10;
    [SerializeField] float speed = 1;
    [SerializeField] ArrowDirection currentArrowDirection=ArrowDirection.Empty;
    int arrowIndex = 0;
    int swipedArrow = 0;
    [Header("Effects")]
    [SerializeField] Animator girlAnimator;
    [SerializeField] Image arrowBox;
    [Header("Finish Canvases")]
    [SerializeField] GameObject winCanvas;
    [SerializeField] GameObject loseCanvas;
    // Start is called before the first frame update
    void Start()
    {
        SwipeDetector.OnDragEvent += SwipeDetector_OnDragEvent;
        ArrowTrigger.CurrentArrow += ArrowTrigger_CurrentArrow;
       StartCoroutine(Spawn());
       roller.transform.DOMoveX(-120000 + Vector3.forward.x, 200f).SetEase(ease:Ease.Linear);
    }

    private void ArrowTrigger_CurrentArrow(string obj)
    {
        currentArrowDirection = (ArrowDirection)int.Parse(obj);
        if(obj!="4")
            arrowIndex++;
        print(arrowIndex);
        if (arrowIndex >= spawnNumber)
        {
            if (swipedArrow > spawnNumber / 2)
                this.Wait(1,()=> LevelFinish(true));
            else
                this.Wait(1, () => LevelFinish(false));
        }
    }

   
    private void SwipeDetector_OnDragEvent(SwipeDetector.DraggedDirection obj)
    {
        if ((int)currentArrowDirection == (int)obj)
        {
            print("you made it");
            girlAnimator.SetTrigger(obj.ToString());
            arrowBox.color = Color.green;
            swipedArrow++;
            Helpers.Wait(this, 0.5f,()=>{ arrowBox.color = Color.white; });
        }
        else
        {
            arrowBox.color = Color.red;
            arrowBox.transform.DOShakePosition(1,20,20,90,true);
            Helpers.Wait(this, 0.5f, () => { arrowBox.color = Color.white; });
        }

    }

    public void LevelFinish(bool win)
    {
        if (win)
            winCanvas.SetActive(true);
        else
            loseCanvas.SetActive(true);
    }
    IEnumerator Spawn()
    {
        for(int i = 0; i < spawnNumber; i++)
        {
            ArrowDirection arrowDirection = (ArrowDirection)Random.Range(0, 4);
            var obj=Instantiate(arrowPefab, spawnParent.transform);
            obj.transform.SetParent(roller.transform, true);
            obj.tag= ((int) arrowDirection).ToString();
            //set rotation
            switch (arrowDirection)
            {
                case ArrowDirection.Empty:
                    break;
                case ArrowDirection.Up:
                    break;
                case ArrowDirection.Down:
                    obj.transform.Rotate(new Vector3(0,0,180));
                    break;
                case ArrowDirection.Right:
                    obj.transform.Rotate(new Vector3(0, 0, -90));
                    break;
                case ArrowDirection.Left:
                    obj.transform.Rotate(new Vector3(0, 0, 90));
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(speed);
        }
    }
    public enum ArrowDirection
    {
        Up,
        Down,
        Right,
        Left,
        Empty
    }
    private void OnDestroy()
    {
        SwipeDetector.OnDragEvent -= SwipeDetector_OnDragEvent;
        ArrowTrigger.CurrentArrow -= ArrowTrigger_CurrentArrow;
    }
}
