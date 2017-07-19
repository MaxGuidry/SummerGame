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
        EnemyTextHP.text = "Enemy: " + Enemy.EnemyCurrentHP + " / " + Enemy.EnemyMaxHP;
        EnemySlider.value = Enemy.EnemyCurrentHP;
        EnemyFillImage.color = Color.Lerp(m_ZeroHealth, m_FullHealth, Enemy.EnemyCurrentHP / Enemy.EnemyMaxHP);
    }
    public void SetPlayerHealthUI()
    {
        PlayerTextHP.text = "Player: " + Player.PlayerCurrentHP + " / " + Player.PlayerMaxHP;
        PlayerSlider.value = Player.PlayerCurrentHP;
        PlayerFillImage.color = Color.Lerp(m_ZeroHealth, m_FullHealth, Player.PlayerCurrentHP / Player.PlayerMaxHP);
    }
    public void CombatDisabled()
    {
        UICanvas.enabled = false;
    }
}
