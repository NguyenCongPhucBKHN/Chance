using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class JoystickInput : Singleton<JoystickInput>
{
    [SerializeField] private Rigidbody _rigidbody;
    
    [SerializeField] private FixedJoystick _joystick;
   
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform tfCenterJoystick;
    [SerializeField] private Transform modelTF;
    public Vector3 move;
    public bool isControl => Vector3.Distance(tfCenterJoystick.localPosition, Vector3.zero)>0.001;
     public float moveSpeed;
    private void FixedUpdate() 
    {
        move = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical ).normalized;
        if(_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            modelTF.rotation =  Quaternion.LookRotation(JoystickInput.Instance.move, Vector3.up);    
        }
        Move(moveSpeed);
    }
    public void Move(float speed)
    {
        _rigidbody.velocity = new Vector3(_joystick.Horizontal *speed, _rigidbody.velocity.y, _joystick.Vertical*speed);

    }
}
