using UnityEngine;

public class Node : MonoBehaviour {

    [SerializeField]
    private int NodeIndex;

    public int GetIndex()
    {
        return NodeIndex;
    }
}
