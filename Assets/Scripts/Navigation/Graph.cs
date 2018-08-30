using UnityEngine;

public class Graph : MonoBehaviour {
  private Node[] nodes;

  public static Graph Instance { get; private set; }

  private void Awake() {
    if (Instance == null)
      Instance = this;
    else if (Instance != this)
      Destroy(gameObject);
  }
}