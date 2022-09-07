using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    [SerializeField] GameObject spawnParent;
    [SerializeField] GameObject roller;
    [SerializeField] GameObject arrowPefab;
    [SerializeField] int spawnNumber = 10;
    [SerializeField] float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(Spawn());
       roller.transform.DOMoveX(-10000 + Vector3.forward.x, 20f).SetEase(ease:Ease.Linear);
    }

    IEnumerator Spawn()
    {
        for(int i = 0; i < spawnNumber; i++)
        {
            var obj=Instantiate(arrowPefab, spawnParent.transform);
            obj.transform.SetParent(roller.transform, true);
            yield return new WaitForSeconds(speed);
        }
    }
}
