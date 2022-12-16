using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class JoystickInput : Singleton<JoystickInput>
{
    // [SerializeField] private Rigidbody _rigidbody;
    
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform tfCenterJoystick;
    public Vector3 move;
    public bool isControl => Vector3.Distance(tfCenterJoystick.localPosition, Vector3.zero)>0.001;
    private void Update() 
    {
        // _rigidbody.velocity = new Vector3(_joystick.Horizontal *_moveSpeed, _rigidbody.velocity.y, _joystick.Vertical*_moveSpeed);
        // if(_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        // {
        //    _rigidbody.transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
        // }
        move = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical ).normalized;
        controller.Move(move*Time.deltaTime * _moveSpeed);
    }
}