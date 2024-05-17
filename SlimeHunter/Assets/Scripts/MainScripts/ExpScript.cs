using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpScript : MonoBehaviour
{
    public int EXP;
    private bool vaccumed;
    public float vaccumSpeed;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        vaccumed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (vaccumed)
        {
            Vector3 move = player.transform.position - transform.position;
            move.Normalize();

            transform.Translate(move * vaccumSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameController.currentEXP += EXP;
        Destroy(gameObject);
    }

    public void Vaccumed()
    {
        MoveWithBackground move = gameObject.GetComponent<MoveWithBackground>();
        move.enabled = false;
        vaccumed = true;
    }
}
