
using System.Threading.Tasks;

public class PlayerJumpManager : IJumpService
{
    readonly IPlayerController _playerController;
    readonly IJumpDal _jumpDal;
    bool _canJump;
    int _currentJumpCount = 0;
    int _maxJumpCount = 1;
    public PlayerJumpManager(IPlayerController playerController, IJumpDal jumpDal)
    {
        _playerController = playerController;
        _jumpDal = jumpDal;
        _canJump = _playerController.Jumping;
    }
    public void Tick()
    {
        if (_playerController.InputReader.isJump && _currentJumpCount < _maxJumpCount && !_playerController.ObjectInHand)
        {
            _currentJumpCount++;
            _canJump = true;
            _playerController.Jumping = _canJump;
        }

    }
    public void FixedTick()
    {
        if (_canJump)
        {
            _jumpDal.JumpAction(_playerController.JumpForce);
            _playerController.JumpEffect.Play();
        }
        _canJump = false;
    }

    public void ResetJumpCounter()
    {
        _currentJumpCount = 0;
        _playerController.Jumping = false;
        _playerController.JumpEffect.Play();

    }


}
