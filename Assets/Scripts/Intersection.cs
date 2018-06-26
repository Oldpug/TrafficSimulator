using UnityEngine;

[System.Serializable]
public class Pair
{
    public GameObject first, second;

    public void HideIntersectionEntrances(bool isActive)
    {
        first.SetActive(isActive);
        second.SetActive(isActive);
    }
}

public class Intersection : MonoBehaviour
{

    [SerializeField]
    private float switchCooldown;
    private float cooldown;

    [SerializeField]
    private Pair horizontal, vertical;

    bool isActive = true;

    void Start()
    {
        cooldown = switchCooldown;
        horizontal.HideIntersectionEntrances(isActive);
        vertical.HideIntersectionEntrances(!isActive);
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
        isActive = !isActive;

        horizontal.HideIntersectionEntrances(isActive);
        vertical.HideIntersectionEntrances(!isActive);
    }
}
