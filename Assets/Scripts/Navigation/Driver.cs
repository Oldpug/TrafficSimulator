using UnityEngine;

public class Driver : MonoBehaviour {
  [SerializeField]
  private int[] path;

  private int currentNode;

  public BasicLane GetDirection(IntersectionLane intersection) {
    if (++currentNode >= path.Length)
      return null;

    return intersection.GetIntersectionExit(path[currentNode], transform);
  }
}