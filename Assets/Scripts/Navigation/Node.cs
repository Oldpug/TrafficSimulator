using UnityEngine;

public class Node : MonoBehaviour {
  private static int lastIndex = -1;

  public int Index { get; private set; }

  private void Awake() {
    Index = ++lastIndex;
  }
}