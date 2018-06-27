using UnityEngine;

public class Car : MonoBehaviour {
  [SerializeField]
  private float speed = 0.75f;

  [SerializeField]
  private float tDelta = 0.0075f;

  [SerializeField]
  private float obstacleDetectionDistance = 1f;

  private float t = 1f;

  private Lane currentLane;

  private Vector3 midpoint;

  private Rigidbody rigidBody;

  private bool IsTurning {
    get {
      return t < 1f;
    }
  }

  private bool IsFacingObstacle {
    get {
      RaycastHit hit;
      bool isFacingObstacle = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, obstacleDetectionDistance, ~0, QueryTriggerInteraction.Ignore);

      if (isFacingObstacle)
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
      else
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);

      return isFacingObstacle;
    }
  }

  private void Awake() {
    rigidBody = GetComponent<Rigidbody>();
  }

  private void MoveForward() {
    rigidBody.AddForce(transform.forward * speed);
  }

  private void Brake() {
    //rigidBody.AddForce(transform.forward * (speed * -2));
    rigidBody.velocity = Vector3.zero;
  }

  private void Turn() {
    rigidBody.MovePosition(Bezier.Lerp(currentLane.Begin.position, midpoint, currentLane.End.position, t));
    rigidBody.MoveRotation(Quaternion.Lerp(currentLane.Begin.rotation, currentLane.End.rotation, t));

    t += tDelta;
  }

  private void StopTurning() {
    rigidBody.velocity = Vector3.zero;
    MoveForward();
  }

  private void OnTriggerEnter(Collider other) {
    Lane newLane = other.GetComponent<Lane>();

    if (newLane == null)
      return;

    currentLane = newLane;
    midpoint = Bezier.Midpoint(newLane.Begin, newLane.End);
    t = tDelta;
  }

  private void FixedUpdate() {
    if (IsFacingObstacle)
      Brake();
    else if (IsTurning) {
      Turn();

      if (!IsTurning)
        StopTurning();
    } else
      MoveForward();
  }
}