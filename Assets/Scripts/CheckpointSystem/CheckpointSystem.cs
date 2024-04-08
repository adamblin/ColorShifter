using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{

    public Checkpoint[] checkpoints;

    public int currentCheckpointIndex=0;

    void Start()
    {
        currentCheckpointIndex = -1;
        GetCheckpointsReferences();
        LoadData();
        

    }

    void GetCheckpointsReferences()
    {
        int checkpointCount = transform.childCount;
        checkpoints = new Checkpoint[checkpointCount];

        for (int i = 0; i < checkpointCount; i++)
        {          
            checkpoints[i] = transform.GetChild(i).GetComponent<Checkpoint>();
            checkpoints[i].SetIndex(i);
            checkpoints[i].SetCheckpointSystem(this);
        }

    }

   void SaveData()
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        Debug.Log("Guardando checkpoint en la escena: " + sceneName);
        PlayerPrefs.SetInt(sceneName, currentCheckpointIndex);
    }

    void LoadData()
    {
        currentCheckpointIndex = PlayerPrefs.GetInt(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, -1);
        if (currentCheckpointIndex > -1)
        {
            checkpoints[currentCheckpointIndex].Activate();
            CharacterMovement player = FindObjectOfType<CharacterMovement>();
            player.SetPosition(checkpoints[currentCheckpointIndex].GetCheckpointPosition());
        }
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteKey(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }


    public bool CheckpointAvailable()
    {
        return currentCheckpointIndex > -1;
    }

    public Vector3 GetCurrentCheckpointPosition()
    {
        return checkpoints[currentCheckpointIndex].GetCheckpointPosition();
    }

    public void CheckpointActivated(int i)
    {
        if (i > currentCheckpointIndex)
        {
            currentCheckpointIndex = i;
            SaveData();
        }
    }

}
