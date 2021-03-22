using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public OrderSystem order;
    public bool isBroken = false;
    [SerializeField] private List<GameObject> _parts = new List<GameObject>();
    [SerializeField] private GameObject _model = null;
    float _collisionForce = 200f;


    // Start is called before the first frame update
    void Start()
    {
        order = GameObject.Find("Character").GetComponent<OrderSystem>();
    }

    private void Update()
    {
        if(isBroken)
        {
            StartCoroutine(DestroyParts(.5f));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!isBroken)
        {
            if (other.tag == "EARTH")
            {
                if (order.isGoingToEarth)
                {
                    for (int i = 0; i < _parts.Count; i++)
                    {
                        _parts[i].SetActive(true);
                        _parts[i].GetComponent<Rigidbody>().AddForce((transform.parent.gameObject.transform.right*-1)*Random.Range(200,500));
                    }
                    _model.SetActive(false);
                    transform.parent.gameObject.GetComponent<BoxCollider>().enabled = false;
                    isBroken = true;
                }


            }
        }
    }

    IEnumerator DestroyParts(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Vector3 scaleChange = new Vector3(-0.01f, -0.01f, -0.01f);
        for (int i = 0; i < _parts.Count; i++)
        {
            if (_parts[i] != null)
            {
                _parts[i].transform.localScale += scaleChange;
                if (_parts[i].transform.localScale.y <= 0.1f )
                {
                    Destroy(_parts[i].gameObject);

                }
            }
        }
    }

   

}
