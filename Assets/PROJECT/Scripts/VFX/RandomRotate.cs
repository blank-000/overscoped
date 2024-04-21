// using UnityEngine;

// public class RandomRotate : MonoBehaviour
// {
//     public float timeBetweenRotations;
//     public float timeToFinishRotation;

//     float timerBetweenRotations;
//     float rotationTimer;

//     float randomRotationAngle;
//     bool isRotating;

//     void Awake()
//     {
//         timerBetweenRotations = timeBetweenRotations;
//         rotationTimer = timeToFinishRotation;
//         randomRotationAngle = Random.Range(0,360);

//     }


//     void Update()
//     {
//         if(!isRotating) timerBetweenRotations -= Time.deltaTime;
        
//         if(timerBetweenRotations <= 0)
//         {
//             rotationTimer -= Time.deltaTime;
//             Debug.Log(isRotating);
//             isRotating= true;
//             float elapsed = 1 - rotationTimer/timeToFinishRotation;
//             Quaternion targetRotation = Quaternion.Euler(0,  randomRotationAngle, 0);
//             // transform.rotation = targetRotation;
//             transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, elapsed);

//             if(rotationTimer <= 0)
//             {
//                 isRotating = false;
//                 rotationTimer = timeToFinishRotation;
//                 timerBetweenRotations = timeBetweenRotations;
//                 randomRotationAngle = Random.Range(0,360);
                
//             }

//         } 
//     }


// }

using UnityEngine;

public class RandomRotate : MonoBehaviour
{
    public float timeBetweenRotationsSeconds;
    public float timeToFinishRotationSeconds;

    private float timerBetweenRotations;
    private float rotationTimer;
    private float randomRotationAngle;
    private bool isRotating;

    private Quaternion startRotation;
    private Quaternion targetRotation;
    private float rotationProgress;

    void Start()
    {
        ResetTimers();
        ChooseRandomRotationAngle();
        isRotating = false;
    }

    void Update()
    {
        timerBetweenRotations -= Time.deltaTime;

        if (!isRotating)
        {
            if (timerBetweenRotations <= 0)
            {
                isRotating = true;
                ResetTimers();
                ChooseRandomRotationAngle();
            }
        }
        else
        {
            rotationTimer -= Time.deltaTime;
            rotationProgress = 1 - (rotationTimer / timeToFinishRotationSeconds);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, rotationProgress);

            if (rotationTimer <= 0)
            {
                isRotating = false;
                ResetTimers();
            }
        }
    }

    void ResetTimers()
    {
        timerBetweenRotations = timeBetweenRotationsSeconds;
        rotationTimer = timeToFinishRotationSeconds;
    }

    void ChooseRandomRotationAngle()
    {
        randomRotationAngle = Random.Range(0, 360);
        startRotation = transform.rotation;
        targetRotation = Quaternion.Euler(0, randomRotationAngle, 0);
    }
}

