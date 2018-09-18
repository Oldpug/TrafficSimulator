﻿using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField]
    public float MaxSpeed = 6f;

    [SerializeField]
    private float brakingSpeed = float.MaxValue;

    [SerializeField]
    private float viewDistance = float.MaxValue;

    [SerializeField]
    private float laneCorrectionDistance = 0.005f;

    [SerializeField]
    public Lane Lane;

    private Rigidbody body;

    private bool isInitialized;

    private float speed;

    private bool isTurning;

    private float laneLength;

    private Vector3 laneBeginFwd;

    private Vector3 laneBeginPos;

    private Vector3 lastFramePos;

    public float Velocity;

    public float TraveledDistance;

    public bool IsFacingObstacle
    {
        get
        {
            var pos = transform.position + new Vector3(0, 1, 0);

            RaycastHit hit;
            var isFacingObstacle = Physics.Raycast(pos, transform.TransformDirection(Vector3.forward), out hit, viewDistance, ~0, QueryTriggerInteraction.Collide);

            if (isFacingObstacle)
                Debug.DrawRay(pos, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            else
                Debug.DrawRay(pos, transform.TransformDirection(Vector3.forward) * 1000, Color.white);

            return isFacingObstacle;
        }
    }

    private void RecordMovement()
    {
        var x = Vector3.Distance(lastFramePos, transform.position);
        Velocity = x > laneCorrectionDistance ? x : 0;
        lastFramePos = transform.position;
    }

    private void InitLane()
    {
        if (Lane == null)
        { 
            SpawnCache.DespawnCar(this);
            return;
        }

        laneBeginPos = transform.position;
        laneBeginFwd = transform.forward;

        laneLength = Vector3.Distance(transform.position, Lane.End.position);

        isTurning = transform.forward != Lane.End.forward;
    }

    private void AdvanceLane()
    {
        transform.position = Lane.End.position;
        transform.rotation = Lane.End.rotation;

        TraveledDistance += Vector3.Distance(laneBeginPos, transform.position);

        var next = Lane.Next;
        var intersection = next as IntersectionLane;

        Lane = intersection == null ? next : intersection.GetRandomExit(transform);
        InitLane();
    }

    private void Move()
    {
        var currentDistance = Vector3.Distance(laneBeginPos, transform.position);

        if (isTurning)
        {
            var dir = Vector3.Lerp(laneBeginFwd, Lane.End.forward, currentDistance / laneLength);
            body.MoveRotation(Quaternion.LookRotation(dir, Vector3.up));
        }

        body.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);

        if (Mathf.Abs(laneLength - currentDistance) < 0.1f)
            AdvanceLane();
    }

    private void Brake()
    {
        speed = Mathf.Max(speed - brakingSpeed * Time.fixedDeltaTime, 0);
    }

    private void Accelerate()
    {
        speed = Mathf.Min(speed + brakingSpeed * Time.fixedDeltaTime, MaxSpeed);
    }

    public void Init()
    {
        lastFramePos = transform.position;
        speed = MaxSpeed;
        InitLane();
        isInitialized = true;
    }

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (isInitialized)
        {
            RecordMovement();

            if (IsFacingObstacle)
                Brake();
            else
                Accelerate();

            Move();
        }
    }
}