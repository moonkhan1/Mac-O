using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManager : IMovementsService
{
    readonly IPlayerController _playerController;
    readonly IMoveDal _moverDal;
    float _inputValue;
    public PlayerMovementManager(IPlayerController playerController, IMoveDal moverDal)
    {
        _playerController = playerController;
        _moverDal = moverDal;
    }
    public void Tick()
    {
        _inputValue = _playerController.InputReader.Horizontal * _playerController.Speed;
    }
    public void FixedTick()
    {
        if (_inputValue == 0f) return;
        _moverDal.MoveAction(Vector3.right, _inputValue * Time.fixedDeltaTime);

        if (_inputValue != 0f && !_playerController.Jumping)
        {
            _playerController.MovementEffect.Play();
        }
        else
        {
            _playerController.MovementEffect.Stop();
            
        }
        
    }
}
