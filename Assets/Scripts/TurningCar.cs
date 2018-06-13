using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningCar : MonoBehaviour
{
    [SerializeField]
    float _speed = 0.5f;

    Rigidbody _body;

    bool _isTurning;

    Transform _from, _to;

    void _rotate()
    {
        _body.MoveRotation(Quaternion.Lerp(_from.rotation, _to.rotation, Time.time * _speed));
    }

    public void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    public void OnTriggerEnter(Collider other)
    {
        var lane = other.GetComponent<Lane>();

        if (lane == null)
            return;

        _from = lane.Start;
        _to = lane.End;
        _isTurning = true;
    }

    public void FixedUpdate()
    {
        if (_isTurning)
            _rotate();

        if (_to != null && transform.rotation == _to.rotation)
            _isTurning = false;

        _body.AddForce(transform.forward * _speed);
    }
}