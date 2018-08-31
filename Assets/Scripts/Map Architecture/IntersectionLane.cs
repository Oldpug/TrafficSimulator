using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class IntersectionExit {
  public BasicLane[] NextLanes;

  public IntersectionLane NextIntersection;
}

public class IntersectionLane : Lane {
  [SerializeField]
  public IntersectionExit[] IntersectionExits;

  public override Transform End {
    get {
      return null;
    }
  }

  public override Lane Next {
    get {
      return null;
    }

    set { }
  }

  public BasicLane GetIntersectionExit(IntersectionLane nextIntersection, Transform carPosition) {
    foreach (var i in IntersectionExits)
      if (i.NextIntersection == nextIntersection && carPosition.forward + i.NextLanes[Random.Range(0, i.NextLanes.Length)].End.transform.forward != Vector3.zero)
        return i.NextLanes[Random.Range(0, i.NextLanes.Length)];

    return IntersectionExits[0].NextLanes[0];
  }
}