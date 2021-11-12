using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemFactory : MonoBehaviour
{
    public GameObject lc1, lc2, lc3, rc1, rc2, rc3;

    // Variables of PID control
    const float Kp = 6f;
    const float Ki = 1f;
    const float Kd = 1f;

    float err = 90f;
    float integral = 0f;
    float last_err = 0f;
    DateTime last_time;
    float cur_angle;


    // Start is called before the first frame update
    void Start()
    {    
    }

    // Update is called once per frame
    void Update()
    {   
        PID_Control(-30,1,1,1,1,1);

    }


    void motor_on(HingeJoint hinge, float control_input)
    {
        hinge.useMotor = true;
        JointMotor jm = hinge.motor;
        jm.targetVelocity = control_input;
        hinge.motor = jm;
    }

    void motor_off(HingeJoint hinge)
    {
        hinge.useMotor = false;
    }

    public void PID_Control(float l1, float l2, float l3, float r1, float r2, float r3)
    {
        motor_off(lc1.GetComponent<HingeJoint>());

        cur_angle = lc1.transform.localEulerAngles.y;
        if(cur_angle > 180) cur_angle = -(360 - cur_angle);
        err = l1 - cur_angle;
    
        integral += err * Time.deltaTime;

        float diff = (err - last_err)/Time.deltaTime;

        float control_input = Kp * err + Ki * integral + Kd * diff;


        motor_on(lc1.GetComponent<HingeJoint>(), control_input);
        Debug.Log(control_input);

        last_err  = err;



    }
}
