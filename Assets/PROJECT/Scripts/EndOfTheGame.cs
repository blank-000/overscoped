using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfTheGame : MonoBehaviour
{
    [SerializeField] GameObject EndScreen;
    public void Replay()
    {
        SceneManager.LoadScene(0);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")){
            DisplayEndScreen();
        }
    }

    public void ActivateEnd()
    {
        DisplayEndScreen();
    }

    void DisplayEndScreen()
    {
        EndScreen.SetActive(true);
    }
}
