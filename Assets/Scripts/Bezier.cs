﻿using UnityEngine;

public static class Bezier {
  public static Vector3 Lerp(Vector3 begin, Vector3 midpoint, Vector3 end, float t) {
    t = Mathf.Clamp01(t);
    float oneMinusT = 1f - t;

    Vector3 lerp = oneMinusT * oneMinusT * begin + 2f * oneMinusT * t * midpoint + t * t * end;
    return lerp;
  }

  public static Vector3 Midpoint(Transform begin, Transform end) {
    Vector3 beginPos = begin.position;
    Vector3 beginFwd = begin.forward;

    Vector3 endPos = end.position;

    float angle = Vector3.Angle(endPos - beginPos, beginPos);
    float distance = Vector3.Distance(beginPos, endPos) * Mathf.Cos(angle);

    Vector3 midpoint = beginPos + distance * beginFwd;

    var cube = Object.Instantiate(begin);
    cube.transform.position = midpoint;

    return midpoint;
  }
}