using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [Header("Player 1 bars")]
    [SerializeField] private HealthBarUI p1HealthBar;
    [SerializeField] private FocusBarUI p1FocusBar;
    [SerializeField] private LaserAmmoHUD p1LaserAmmoHUD;
    [SerializeField] private MissileAmmoHUD p1MissileAmmoHUD;

    [Header("Player 2 bars")]
    [SerializeField] private HealthBarUI p2HealthBar;
    [SerializeField] private FocusBarUI p2FocusBar;
    [SerializeField] private LaserAmmoHUD p2LaserAmmoHUD;
    [SerializeField] private MissileAmmoHUD p2MissileAmmoHUD;

    public void BindPlayers(PlayerConfig player1, PlayerConfig player2)
    {
        p1HealthBar.Bind(player1);
        p1FocusBar.Bind(player1);
        p1LaserAmmoHUD.Bind(player1);
        p1MissileAmmoHUD.Bind(player1);

        p2HealthBar.Bind(player2);
        p2FocusBar.Bind(player2);
        p2LaserAmmoHUD.Bind(player2);
        p2MissileAmmoHUD.Bind(player2);
    }
}
