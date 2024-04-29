using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projSpeed;
    private float timePasseed;
    private RaycastHit rayHit;
    private Ray ray;
    private Vector3 rotatePosition;

    // Start is called before the first frame update
    void Start()
    {
        timePasseed = 0;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out rayHit, Mathf.Infinity))
        {
            rotatePosition = rayHit.point - transform.position;
        }

        float rotZ = Mathf.Atan2(rotatePosition.y, rotatePosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ + 90f);
    }

    // Update is called once per frame
    void Update()
    {
        timePasseed += Time.deltaTime;
        if (timePasseed >= 5)
        {
            Destroy(gameObject);
        }

        transform.Translate(Vector3.down * projSpeed * Time.deltaTime);
        // Debug.Log(transform.forward);
    }
}
