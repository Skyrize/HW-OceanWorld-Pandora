using UnityEngine;

public class CheckpointHolder : MonoBehaviour
{
    private Checkpoint m_lastCheckpoint;

    public void SetLastCheckpoint(Checkpoint checkpoint)
    {
        m_lastCheckpoint = checkpoint;
    }
    
    public void SetLastCheckpoint(GameObject obj)
    {
        SetLastCheckpoint(obj.GetComponent<Checkpoint>());
    }

    public void ResetCheckpoint()
    {
        m_lastCheckpoint = null;
    }
}