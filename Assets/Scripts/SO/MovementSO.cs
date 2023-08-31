using UnityEngine;

[CreateAssetMenu(fileName = "Movement info", menuName = "Info/Movement Information", order = 51)]
public class MovementSO : ScriptableObject
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private int _dashCooldown;
    [SerializeField] private int _dashDuration;

    public float Speed => _speed;
    public float JumpForce => _jumpForce;
    public float DashSpeed => _dashSpeed;
    public int DashCooldown => _dashCooldown;
    public int DashDuration => _dashDuration;
}
