using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] Image slider;
    [SerializeField] Text currentLevelText, nextLevelText;
    [SerializeField] GameObject startPanel;
    public GameObject gameOverPanel;

    public void StartButton()
    {
        startPanel.SetActive(false);
    }
    public void UpdateProggresFill(float value)
    {
        slider.GetComponent<Image>().fillAmount = value;
    }
    public void SetLevelText(int level)
    {
        currentLevelText.text = level.ToString();
        nextLevelText.text = (level + 1).ToString();
    } 
}
