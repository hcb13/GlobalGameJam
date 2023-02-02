using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{

    [SerializeField]
    private GameObject prefabCollectAnimation;

    private Transform myCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Items"))
        {
            myCollider = collision.transform;
            ShowCollectAnimation();
        }
    }

    public void ShowCollectAnimation()
    {
        GameObject _gameObject = Instantiate(prefabCollectAnimation, myCollider.position, Quaternion.identity);
        GameObject.FindGameObjectsWithTag("LifeHUD")[0].GetComponent<LifeHUD>().UpdateAnimatorLifes(+1);
        Destroy(myCollider.parent.gameObject);
        Destroy(_gameObject, 1f);
        
    }

}
