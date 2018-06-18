using UnityEngine;

public class Lane : MonoBehaviour
{
    [SerializeField]
    public Transform Midpoint;

    [SerializeField]
    public Transform End;

    public Transform Start
    {
        get
        {
            return transform;
        }
    }
}