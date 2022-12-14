using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class JoystickInput : Singleton<JoystickInput>
{
    [SerializeField] private Rigidbody _rigidbody;
    
    // [SerializeField] private FixedJoystick _joystick;
   
    [SerializeField] private Transform tfCenterJoystick;
    [SerializeField] private Transform modelTF;
    [SerializeField] private Player player;
    public Vector3 move;
    public bool isControl => Vector3.Distance(tfCenterJoystick.localPosition, Vector3.zero)>0.001;
    public float moveSpeed;
    private void FixedUpdate() 
    {
        // Vector2 moveInput = player.moveAction.ReadValue<Vector2>();
        // move = new Vector3(moveInput.x, 0f, moveInput.y);
        // move.Normalize();
        // Move(moveSpeed); 
        // move = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical ).normalized;
        // if( !player.isDashing &&(_joystick.Horizontal != 0 || _joystick.Vertical != 0))
        // {
        //     modelTF.rotation =  Quaternion.LookRotation(JoystickInput.Instance.move, Vector3.up);   
        //     Move(moveSpeed); 
        // }
        // else if(player.isDashing)
        // {
        //     Dash(moveSpeed);
        // }
    }
    public void Move(float speed)
    {
        _rigidbody.velocity = new Vector3(move.x *speed, _rigidbody.velocity.y, move.z*speed);

    }

    public void Dash(float speed)
    {
         _rigidbody.velocity = modelTF.forward * speed;
    }

    public void Stop()
    {
        _rigidbody.velocity = Vector3.zero;
    }
}
