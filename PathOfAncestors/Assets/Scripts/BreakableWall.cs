using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public OrderSystem order;
    public bool isBroken = false;
    [SerializeField] private List<GameObject> _parts = new List<GameObject>();
    [SerializeField] private GameObject _model = null;
    float _collisionForce = 0.01f;


    // Start is called before the first frame update
    void Start()
    {
        order = GameObject.Find("Character").GetComponent<OrderSystem>();
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
                        _parts[i].GetComponent<Rigidbody>().AddForce((transform.parent.gameObject.transform.forward)*_collisionForce);
                    }
                    _model.SetActive(false);
                    transform.parent.gameObject.GetComponent<BoxCollider>().enabled = false;

                }


            }
        }
    }

}
