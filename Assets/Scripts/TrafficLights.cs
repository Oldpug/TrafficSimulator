using UnityEngine;

[System.Serializable]
public class Pair
{
    public GameObject first, second;

    public void HideIntersectionEntrances(bool isHidden)
    {
        first.SetActive(isHidden);
        second.SetActive(isHidden);
    }
}

public class TrafficLights : MonoBehaviour
{

    public static readonly string StopperTag = "IntersectionStopper";

    [SerializeField]
    private float switchCooldown;
    private float cooldown;

    [SerializeField]
    private Pair horizontal, vertical;

    bool isHidden = false;

    void Start()
    {
        cooldown = switchCooldown;
        horizontal.HideIntersectionEntrances(isHidden);
        vertical.HideIntersectionEntrances(!isHidden);
    }

    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            ChangeIntersection();
            cooldown = switchCooldown;
        }
    }

    private void ChangeIntersection()
    {
        isHidden = !isHidden;

        horizontal.HideIntersectionEntrances(isHidden);
        vertical.HideIntersectionEntrances(!isHidden);
    }
}
