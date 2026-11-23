using System.Numerics;

namespace JeuFrancCore;
using GameState = (Vector2, Vector2, Vector2 );
using GameAction = (bool, bool);


public class GameLogik
{
    private GameAction _up_down = (false, false);
    private Vector2 _ballMomentum = Vector2.Zero;
    private Random _rng;
    private static float _playerRatio = 0.1f;
    
    private static Vector2 _reflectY = new Vector2(1f, -1f); 
    private static Vector2 _reflectX = new Vector2(-1f, 1f); 
    
    static Vector2 FromAngle(float radians)
        => new Vector2(MathF.Cos(radians), MathF.Sin(radians));

    public GameLogik()
    {
        _rng = new Random((int)DateTime.Now.Ticks);
        Init();
    }
    public void Init()
    {
        float r = _rng.NextSingle() * 2.0f - 1.0f; 
        _ballMomentum = Vector2.One * FromAngle(r * MathF.PI);
    }
    public GameState Step(double timeStep, GameAction action, GameState state, Vector2 fieldSize)
    {
        _up_down = (_up_down.Item1 ^ action.Item1, _up_down.Item2 ^ action.Item2);

        Vector2 newPos = _ballMomentum * (float)timeStep + state.Item1;
        if (newPos.Y > fieldSize.Y || 
            newPos.Y < -fieldSize.Y)
        {
            _ballMomentum *= _reflectY;
            newPos = _ballMomentum * (float)timeStep + state.Item1;
        }

        if (newPos.X < -fieldSize.X)
        {
            //player 1 lost
        }
        if (newPos.X > fieldSize.X)
        {
            //player 2 lost
        }

        if (PlayerTouched(state.Item1, newPos, state.Item2, fieldSize.Y) ||
            PlayerTouched(state.Item1, newPos, state.Item3, fieldSize.Y))
        {
            _ballMomentum *= _reflectX;
            newPos = _ballMomentum * (float)timeStep + state.Item1;
        }
        //START WIP
        _up_down = (
            _up_down.Item1 ^ state.Item2.Y + (fieldSize.Y * _playerRatio),
            )
        
        state = (
            newPos,
                
        )
        //END WIP
        return state;
    }

    private static bool PlayerTouched(Vector2 prevPos, Vector2 nextPos, Vector2 playerPos, float fieldHeight)
    {
        if (Math.Abs(prevPos.Y - playerPos.Y) < fieldHeight * _playerRatio)
            return prevPos.X < playerPos.X ^ nextPos.X < playerPos.X;
        
        return false;
    }
}