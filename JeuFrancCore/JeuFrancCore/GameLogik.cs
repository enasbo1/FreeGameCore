using System.Numerics;

namespace JeuFrancCore;
using GameState = (Vector2, Vector2, Vector2);
using GameAction = (bool, bool);


public class GameLogik : IGameLogik<GameState, GameAction>
{
    private GameAction _up_down = (false, false);
    private Vector2 _ballMomentum = Vector2.Zero;
    private Random _rng;
    private static float _playerRatio = 0.1f;
    private float _playerSpeed;
    private Vector2 _fieldSize;
    
    private static Vector2 _reflectY = new Vector2(1f, -1f); 
    private static Vector2 _reflectX = new Vector2(-1f, 1f); 
    private static bool _initialized = false;
    
    static Vector2 FromAngle(float radians)
        => new Vector2(MathF.Cos(radians), MathF.Sin(radians));

    public GameLogik()
    {
        _rng = new Random((int)DateTime.Now.Ticks);
    }
    internal void Init(float playerSpeed, float ballSpeed, Vector2 fieldSize)
    {
        _fieldSize = fieldSize;
        _playerSpeed = playerSpeed;
        _initialized = true;
        float r = _rng.NextSingle() * 2.0f; 
        _ballMomentum = FromAngle(r * MathF.PI) * ballSpeed;
    }
    public (GameState, float, bool) Step(float timeStep, GameAction action, GameState state)
    {
        System.Diagnostics.Trace.Assert(_initialized, "L'environnement doit être initialisé avant d'être joué");
        float reward = 0f;
        bool terminal = false;
        _up_down = (_up_down.Item1 ^ action.Item1, _up_down.Item2 ^ action.Item2);
        
        Vector2 newPos = _ballMomentum * (float)timeStep + state.Item1;
        if (newPos.Y > _fieldSize.Y || 
            newPos.Y < -_fieldSize.Y)
        {
            _ballMomentum *= _reflectY;
            newPos = _ballMomentum * (float)timeStep + state.Item1;
        }

        if (newPos.X < -_fieldSize.X)
        {
            reward = 1f;
            terminal = true;
        }
        if (newPos.X > _fieldSize.X)
        {
            reward = -1f;
            terminal = true;
        }

        if (PlayerTouched(state.Item1, newPos, state.Item2, _fieldSize.Y) ||
            PlayerTouched(state.Item1, newPos, state.Item3, _fieldSize.Y))
        {
            _ballMomentum *= _reflectX;
            newPos = _ballMomentum * (float)timeStep + state.Item1;
        }
        
        float i1 = state.Item1.Y + (_up_down.Item1 ? timeStep * _playerSpeed : -timeStep * _playerSpeed);
        float i2 = state.Item2.Y + (_up_down.Item2 ? timeStep * _playerSpeed : -timeStep * _playerSpeed);
        
        _up_down = (
            _up_down.Item1 ^
            (i1 + _fieldSize.Y * _playerRatio < _fieldSize.Y ||
             i1 - _fieldSize.Y * _playerRatio > 0
            ),
            _up_down.Item2 ^
            (i2 + _fieldSize.Y * _playerRatio < _fieldSize.Y ||
             i2 - _fieldSize.Y * _playerRatio > 0
            )
        );
        
        state.Item2.Y += (_up_down.Item1 ? timeStep : -timeStep);
        state.Item3.Y += (_up_down.Item2 ? timeStep : -timeStep);

        state.Item1 = newPos;
        return (state, reward, terminal);
    }

    private static bool PlayerTouched(Vector2 prevPos, Vector2 nextPos, Vector2 playerPos, float fieldHeight)
    {
        if (Math.Abs(prevPos.Y - playerPos.Y) < fieldHeight * _playerRatio)
            return prevPos.X < playerPos.X ^ nextPos.X < playerPos.X;
        
        return false;
    }
}