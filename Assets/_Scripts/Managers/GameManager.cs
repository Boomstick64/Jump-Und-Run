using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // declaring the variable to that will store the slider infomation
    public Slider m_Slider;

    //Declaring Health indidcator for text Variable
    public Text m_HealthIndicator;
    private float m_RoundedCurrentHealth;
    private float m_RoundedMaxHealth;

    //Getting the current health of the player
    private PlayerController m_PlayerController;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerController = GameObject.Find("Player").GetComponent<PlayerController>();
        m_Slider.maxValue = m_PlayerController.m_MaxHealth;
        m_HealthIndicator.text = "";
        m_RoundedMaxHealth = ((int)m_PlayerController.m_MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        m_Slider.value = m_PlayerController.currentHealth;
        m_RoundedCurrentHealth = ((int)m_PlayerController.currentHealth);
        m_HealthIndicator.text = "Health: " + m_RoundedCurrentHealth.ToString() + "/" + m_RoundedMaxHealth.ToString();
        if (Input.GetKeyDown(KeyCode.R))
        {
            //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            Resources.UnloadUnusedAssets();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        }

        if (m_PlayerController.currentHealth <= 0f)
        {
            Application.Quit();
        }
    }
}
