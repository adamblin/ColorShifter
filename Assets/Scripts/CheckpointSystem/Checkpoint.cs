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
        CharacterMovement player = other.GetComponent<CharacterMovement>();
        if (player != null)
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
    }

    public void Deactivate()
    {
        checkpointActivated = false;
        objectActivated.SetActive(checkpointActivated);
        objectDeactivated.SetActive(!checkpointActivated);
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
