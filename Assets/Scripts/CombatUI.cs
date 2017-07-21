using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour {

    public Text EnemyTextHP, PlayerTextHP;
    public EnemyBehaviour Enemy;
    public PlayerCombatBehaviour Player;
    public Canvas UICanvas;
    public Slider EnemySlider, PlayerSlider;
    public Image EnemyFillImage, PlayerFillImage;
    public Color m_FullHealth = Color.green;
    public Color m_ZeroHealth = Color.grey;
    public Button AttackButton, DefendButton;

    public void Start()
    {
        UICanvas.enabled = false;
    }
    public void CombatEnabled()
    {
        UICanvas.enabled = true;
        SetEnemyHealthUI();
        SetPlayerHealthUI();
    }
    public void SetEnemyHealthUI()
    {
        Debug.Log(Enemy.EnemyCurrentHP);
        EnemyTextHP.text = "Enemy: " + Enemy.EnemyCurrentHP + " / " + Enemy.EnemyMaxHP;
        EnemySlider.value = Enemy.EnemyCurrentHP;
        EnemySlider.maxValue = Enemy.EnemyMaxHP;
        EnemyFillImage.color = Color.Lerp(m_ZeroHealth, m_FullHealth, 
            (float)Enemy.EnemyCurrentHP / (float)Enemy.EnemyMaxHP);
    }
    public void SetPlayerHealthUI()
    {
        PlayerTextHP.text = "Player: " + Player.PlayerCurrentHP + " / " + Player.PlayerMaxHP;
        PlayerSlider.value = Player.PlayerCurrentHP;
        PlayerSlider.maxValue = Player.PlayerMaxHP;
        PlayerFillImage.color = Color.Lerp(m_ZeroHealth, m_FullHealth, 
            (float)Player.PlayerCurrentHP / (float)Player.PlayerMaxHP);
    }
    public void HideUI()
    {
        AttackButton.gameObject.SetActive(false);
        DefendButton.gameObject.SetActive(false);
    }
    public void ShowUI()
    {
        AttackButton.gameObject.SetActive(true);
        DefendButton.gameObject.SetActive(true);
    }
    public void CombatDisabled()
    {
        UICanvas.enabled = false;
    }
}
