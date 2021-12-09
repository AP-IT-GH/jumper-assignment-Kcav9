using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using System;
using Unity.MLAgents.Actuators;
using TMPro;

public class Taxi : Agent
{
    // Start is called before the first frame update
    [SerializeField] private float jumpForce;
    [SerializeField] private KeyCode jumpKey;
    [SerializeField]  private TextMeshPro scoreboard;
    private Vector3 startingPosition;
    private bool canJump = true;
    private bool gotFries = false;
    private bool firstCollision = true;

    private Rigidbody body;
    private Environment environment;

    public event Action OnReset;

    void Update()
    {
        //scoreboard.text = GetCumulativeReward().ToString("f4");
    }


    public override void Initialize()
    {
        body = GetComponent<Rigidbody>();
        environment = GetComponentInParent<Environment>();
        startingPosition = transform.position;

    }

    public override void OnEpisodeBegin()
    {
        canJump = true;
        transform.position = startingPosition;
        body.velocity = Vector3.zero;

        environment.ClearEnvironment();
        OnReset?.Invoke();
    }



    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var jump = 0;
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = 0;

        if (Input.GetKey(jumpKey))
        {
            discreteActionsOut[0] = 1;
        }
           
    }

    private void Jump()
    {
        if (canJump)
        {
            body.AddForce(new Vector3(0, jumpForce, 0), ForceMode.VelocityChange);
            canJump = false;
        }


    }
  


    public override void OnActionReceived(ActionBuffers actions)
    {
        var vectorAction = actions.DiscreteActions;
        if (vectorAction[0] == 1)
        {
            AddReward(-0.01f);
            Jump();
        }
            

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road") )
        {
            canJump = true;

        }
        if (collision.transform.CompareTag("PoliceCar"))
        {
            Debug.Log("collide with obstacle");
            Destroy(collision.gameObject);
            AddReward(-1f);
            EndEpisode();
        }

         if (collision.gameObject.CompareTag("Road") && gotFries)
        {
            canJump = true;
            gotFries = false;
        }

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Point"))
        {
            AddReward(0.2f);
            Console.WriteLine("HITTED FRIES");
            gotFries = true;
        }
    }
}
