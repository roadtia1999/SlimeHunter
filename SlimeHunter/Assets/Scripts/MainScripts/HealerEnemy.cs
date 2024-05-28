using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerEnemy : MonoBehaviour
{
    public GameObject healingArea;
    private GameObject personalArea;
    
    // Start is called before the first frame update
    void Start()
    {
        personalArea = Instantiate(healingArea, transform.position, new Quaternion(0, 0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        personalArea.transform.position = transform.position;
    }

    private void OnDestroy()
    {
        Destroy(personalArea);
    }
}
