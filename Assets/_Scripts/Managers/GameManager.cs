using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // declaring the variable to that will store the slider infomation
    public Slider m_Slider;

    //Getting the current health of the player
    private PlayerController m_PlayerController;

    // Start is called before the first frame update
    void Start()
    {
        m_Slider.maxValue = m_PlayerController.m_MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        m_Slider.value = m_PlayerController.currentHealth;

        if (Input.GetKeyDown(KeyCode.R))
        {
            //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            Resources.UnloadUnusedAssets();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        }
    }
}
