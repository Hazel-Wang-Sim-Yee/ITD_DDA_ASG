using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FallingCandy : MonoBehaviour
{
    private const int targetY = 20;
    Vector3 target;
    private void Awake()
    {
        Reset();
    }
    public void Reset()
    {
        target = transform.position;
        target.y = -targetY;
    }
    // Update is called once per frame
    void Update()
    {
        MoveDown();
    }
    private void MoveDown()
    {
        float step = 3f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }

    public void Hide()
    {
        transform.position = target;
        gameObject.SetActive(false);
    }

    private void OnBecameInvisible()
    {
        if(transform.position.y > ScreenHelper.ScreenTop) { return;}
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Player")) { return;}

        transform.position = target;
        gameObject.SetActive(false);
        ActivityScoreManager.instance.AddScore();
    }
}
