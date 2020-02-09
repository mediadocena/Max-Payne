using UnityEngine;

public class bulletTime : MonoBehaviour
{

    public float slowdownFactor = 0.05f;
    public float slowdownLength = 2f;
    public bool isSlowed = false;
    
    //public GameObject PlayeraudioSource;
    //public AudioSource audio;
    void Start()
    {
       
    }
    public void DoSlowmotion() {
        isSlowed = true;
        
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;

    }
    public void StopSlowmotion() {
        isSlowed = false;
       
        Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }


}
