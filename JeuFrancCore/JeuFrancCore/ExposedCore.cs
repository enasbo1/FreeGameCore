using System.Data;
using System.Numerics;
namespace JeuFrancCore;
using GameState = (Vector2, Vector2, Vector2);
using GameAction = (bool, bool);

public struct InitParams
{
    public InitParams()
    {
    }
    public Vector2 FieldSize { get; set; } = Vector2.One;
    public float BallSpeed { get; set; } = 2f;
    public float PlayerSpeed { get; set; } = 1f;
}

public class ExposedCore : ACore<GameLogik, InitParams, GameState, GameAction>
{
    private Vector2 _fieldSize;
    private float _ballSpeed;
    private float _playerSpeed;
    private GameAction _action;
    private GameState _state;
    private bool _terminal = false;
    private float _reward = 0f;
    private readonly GameLogik _logik = new();

    public override void SetInitParams(InitParams initParams)
    {
        _fieldSize = initParams.FieldSize;
        _ballSpeed = initParams.BallSpeed;
        _playerSpeed = initParams.PlayerSpeed;
    }

    public override void Init()
    {
        _logik.Init(_playerSpeed, _ballSpeed, _fieldSize);
    }
}