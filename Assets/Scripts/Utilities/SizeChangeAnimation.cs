using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChangeAnimation : MonoBehaviour
{

    Vector2 startScale;
    public float sizeChangeAmount;
    public float speed;

    private void Start()
    {
        startScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector2(startScale.x + Mathf.Sin(Time.time*speed) *sizeChangeAmount, startScale.y + Mathf.Sin(Time.time*speed) * sizeChangeAmount);
    }
}
