[System.Serializable]

public class Timer
{
    public bool isRunning;
    public float currTime;
    public float maxTime;

    public bool isMasterRunning;
    public float currMasterTime;
    public float maxMasterTime;

    public void StartTimer(float value)
    {
        maxTime = value;
        currTime = maxTime;
        isRunning = true;
    }
    public void StartMasterTimer(float value)
    {
        maxMasterTime = value;
        currMasterTime = maxMasterTime;
        isMasterRunning = true;
    }
    public void RunTimer()
    {
        currTime -= UnityEngine.Time.deltaTime;
        
        if (currTime <= 0)
        {
            currTime = 0;
            isRunning = false;
        }
    }
    public void RunMasterTimer()
    {
        currMasterTime -= UnityEngine.Time.deltaTime;

        if (currMasterTime <= 0)
        {
            currMasterTime = 0;
            isMasterRunning = false;
        }
    }
}