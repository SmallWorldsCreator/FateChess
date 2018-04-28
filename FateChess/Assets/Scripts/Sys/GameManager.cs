using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_GAME_STATE{
	Init,
	Draw,
	PlayerUseCard,
	PlayerMove,
	PlayerAttack,
	EnemyMove,
	EnemyAttack,
	PlayerNewFate,
	SetBar,
	EnemyNewFate,
	EnemyUseCard,
	Len
};

public class GameManager : ManagerBase<GameManager> {
	public E_GAME_STATE state;
	[NullAlarm]public HeroObj[] heros;
	[LockInInspector]public E_PawnSide nowSide = E_PawnSide.Player; 
//	public override void Awake () {
//
//	}

	public HeroObj nowHero{
		get{
			return heros [(int)nowSide];
		}
	}
	public HeroObj player{
		get{
			return heros [(int)E_PawnSide.Player];
		}
	}
	public HeroObj enemy{
		get{
			return heros [(int)E_PawnSide.Enemy];
		}
	}

	Dictionary<string, E_GAME_STATE> stateDict = new Dictionary<string, E_GAME_STATE> ();

	// Use this for initialization
	void Start () {
		player.Init (E_PawnSide.Player);
		enemy.Init (E_PawnSide.Enemy);

		for (int f = 0; f < (int)E_GAME_STATE.Len; f++) {
			stateDict.Add (((E_GAME_STATE)f).ToString(), (E_GAME_STATE)f);
		}

		ChangeState (E_GAME_STATE.Init);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeStateByAnime (string p_state) {
		E_GAME_STATE _state; 
		if (stateDict.TryGetValue (p_state, out _state)) {
			ChangeState (_state);
		} else {
			Debug.LogError ("No State : [" + p_state + "]");
		}
	}
	public void ChangeState (E_GAME_STATE p_state) {
		state = p_state;
		switch (state) {
		case E_GAME_STATE.Init:
			player.AddCard (PawnManager.instance.GetRamdonData(E_PawnSide.Player));
			player.AddCard (PawnManager.instance.GetRamdonData(E_PawnSide.Player));
			player.AddCard (PawnManager.instance.GetRamdonData(E_PawnSide.Player));

			player.GenerateNewFate ();
			player.SetHp (5);
			ChangeState (E_GAME_STATE.Draw);
			break;
		case E_GAME_STATE.Draw:			
			player.DrawCard ();
			break;
		case E_GAME_STATE.PlayerUseCard:
			
			break;
		case E_GAME_STATE.PlayerMove:
			// @@@@@
			ChangeState(E_GAME_STATE.PlayerNewFate);
			break;
		case E_GAME_STATE.PlayerAttack:
			
			break;
		case E_GAME_STATE.EnemyMove:
			
			break;
		case E_GAME_STATE.EnemyAttack:
			
			break;
		case E_GAME_STATE.PlayerNewFate:
			player.GenerateNewFate ();

			break;
		case E_GAME_STATE.SetBar:
			
			break;
		case E_GAME_STATE.EnemyNewFate:
			enemy.GenerateNewFate ();
			break;
		case E_GAME_STATE.EnemyUseCard:
			enemy.EnemyUseCard ();
			break;
		}
	}
}
