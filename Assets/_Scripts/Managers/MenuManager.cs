using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button m_Playbutton;
    public Button m_Optionsbutton;
    public Button m_Exitbutton;
    private bool m_Play;
    // Start is called before the first frame update
    void Start()
    {
        m_Playbutton = m_Playbutton.GetComponent<Button>();
        m_Optionsbutton = m_Optionsbutton.GetComponent<Button>();
        m_Exitbutton = m_Exitbutton.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }


}
