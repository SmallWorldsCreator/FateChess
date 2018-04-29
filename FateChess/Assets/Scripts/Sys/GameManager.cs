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
	[NullAlarm]public Animator CanvasAnimator;
	public E_GAME_STATE state;
	[NullAlarm]public HeroObj[] heros;
	[LockInInspector]public E_PawnSide nowSide = E_PawnSide.Player;
	[NullAlarm]public Transform cmrTop;
	[LockInInspector]public Vector3 cmrTargetPos;
	public float cmrMoveSpeed;
	public float animeRate = 1;
	public bool autoFight = false;


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

		cmrTargetPos = BoardManager.instance. pawnObjs [12].transform.position;

		ChangeState (E_GAME_STATE.Init);
	}
	
	// Update is called once per frame
	void Update () {
		cmrTop.transform.position = Vector3.MoveTowards(cmrTop.transform.position, cmrTargetPos, cmrMoveSpeed * Time.deltaTime);

	}

	public void ChangeStateByAnime (string p_state) {
		E_GAME_STATE _state; 
		if (stateDict.TryGetValue (p_state, out _state)) {
			ChangeState (_state);
		} else {
			Debug.LogError ("No State : [" + p_state + "]");
		}
	}

	[Button("ChangeState")]public E_GAME_STATE changeStateBut;
	public void ChangeState (E_GAME_STATE p_state) {
		Debug.Log ("ChangeState" + state + " -> " + p_state);
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
			StartCoroutine(BoardManager.instance.AllPawnMove (E_PawnSide.Player));
			break;
		case E_GAME_STATE.PlayerAttack:
			StartCoroutine(BoardManager.instance.AllPawnAtk (E_PawnSide.Player));
			break;
		case E_GAME_STATE.EnemyMove:
			StartCoroutine(BoardManager.instance.AllPawnMove (E_PawnSide.Enemy));
			break;
		case E_GAME_STATE.EnemyAttack:
			StartCoroutine(BoardManager.instance.AllPawnAtk (E_PawnSide.Enemy));
			break;
		case E_GAME_STATE.PlayerNewFate:
			player.GenerateNewFate ();
			CanvasAnimator.Play("PlayerNewFate", -1, 0);
			break;
		case E_GAME_STATE.SetBar:
			//set bar animation
			CanvasAnimator.Play("CardPutTogether", -1, 0);
			break;
		case E_GAME_STATE.EnemyNewFate:
			//enemy new fate animation
			CanvasAnimator.Play("EnemyNewFate", -1, 0);
			enemy.GenerateNewFate ();
			break;
		case E_GAME_STATE.EnemyUseCard:
			StartCoroutine(enemy.EnemyUseCard ());
			break;
		}
	}
}
