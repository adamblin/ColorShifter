using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    CheckpointSystem system;
    bool checkpointActivated = false;
    int checkpointIndex;
    public GameObject objectActivated;
    public GameObject objectDeactivated;

    void Start()
    {
        Deactivate();
    }

    void Update()
    {
        
    }

    public void SetCheckpointSystem(CheckpointSystem c)
    {
        system = c;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player") && !checkpointActivated)
        {
            Activate();
            system.CheckpointActivated(checkpointIndex);
        }
    }
    
    public void Activate()
    {
        checkpointActivated = true;
        objectActivated.SetActive(checkpointActivated);
        objectDeactivated.SetActive(!checkpointActivated);
        Debug.Log($"Checkpoint {checkpointIndex} activado.");
    }


    public void Deactivate()
    {
        checkpointActivated = false;
        objectActivated.SetActive(checkpointActivated);
        objectDeactivated.SetActive(!checkpointActivated);
        Debug.Log($"Checkpoint {checkpointIndex} desactivado.");
    }

    public void SetIndex(int i)
    {
        checkpointIndex = i;
    }

    public Vector3 GetCheckpointPosition()
    {
        return transform.position;
    }

}
