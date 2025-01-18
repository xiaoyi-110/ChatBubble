
using System.Diagnostics;
using Cinemachine;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{

    private LevelState _levelState;
    public enum LevelState{
        PlayerAttack,
        PlayerAovid
    }

    private void Update() {
        switch(_levelState){
            case LevelState.PlayerAttack:
                //TODO
                break;
            case LevelState.PlayerAovid:
                //TODO
                break;
            default:
                break;  
        }
    }
}