using UnityEngine;

[System.Serializable]
public class EntrancesArray
{
    public GameObject[] Entrances;
    public void HideEntrances(bool isHidden)
    {
        foreach (GameObject e in Entrances)
        {
            e.SetActive(isHidden);
        }
    }
}

[System.Serializable]
public class EntranceGroup
{
    [SerializeField]
    private EntrancesArray[] entrance;
    private int currentGroup = 0;

    public void ChangePermission()
    {
        Debug.Log(currentGroup);

        entrance[currentGroup].HideEntrances(false);

        currentGroup++;
        if (currentGroup >= entrance.Length)
            currentGroup = 0;

        entrance[currentGroup].HideEntrances(true);
    }
}

public class TrafficLights : MonoBehaviour
{

    public static readonly string StopperTag = "IntersectionStopper";

    [SerializeField]
    private float switchCooldown;
    private float cooldown;

    [SerializeField]
    private EntranceGroup entrances;

    bool isHidden = false;

    void Start()
    {
        cooldown = switchCooldown;
    }

    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            entrances.ChangePermission();
            cooldown = switchCooldown;
        }
    }
}
