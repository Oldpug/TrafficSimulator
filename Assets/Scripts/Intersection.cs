using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pair
{
    public GameObject first, second;
}

public class Intersection : MonoBehaviour
{

    [SerializeField]
    private float switchCooldown;
    private float cooldown;

    bool firstPair = true;
    bool secondPair = false;

    [SerializeField]
    private Pair horizontal;
    [SerializeField]
    private Pair vertical;

    // Use this for initialization
    void Start()
    {
        cooldown = switchCooldown;
        horizontal.first.SetActive(firstPair);
        horizontal.second.SetActive(firstPair);
        vertical.first.SetActive(secondPair);
        vertical.second.SetActive(secondPair);
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            Debug.Log(cooldown);
        }
        else
        {
            ChangeIntersection();
            cooldown = switchCooldown;
        }
    }

    private void ChangeIntersection()
    {
        firstPair = !firstPair;
        horizontal.first.SetActive(firstPair);
        horizontal.second.SetActive(firstPair);

        secondPair = !secondPair;
        vertical.first.SetActive(secondPair);
        vertical.second.SetActive(secondPair);
    }
}
