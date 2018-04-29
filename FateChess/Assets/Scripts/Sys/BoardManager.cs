using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : ManagerBase<BoardManager> {
	public Vector2 size;
	int pawnCount = 0;
	[NullAlarm]public Transform pawnTop;
	[NullAlarm]public PawnObj[] pawnObjs;
	[NullAlarm]public BoardObj[] boardObjs;
	public Vector2[] moveOffsets = new Vector2[]{
		new Vector2( 1,  0),
		new Vector2( 0,  1),
		new Vector2(-1,  0),
		new Vector2( 0, -1)
	};

//	public override void Awake () {
//
//	}
	void Start () {
		pawnCount = pawnObjs.Length;

		for (int _y = 0; _y < (int)size.y; _y++) {
			for (int _x = 0; _x < (int)size.x; _x++) {
				int _index = _x + (_y*5);
				pawnObjs [_index].SetNewPawnData (PawnManager.instance.pawnNull);
				pawnObjs [_index].index = _index;
				boardObjs [_index].index = _index;
			}
		}

		pawnObjs [ 2].SetNewPawnData (PawnManager.instance.pawnBasePlayer);
		pawnObjs [22].SetNewPawnData (PawnManager.instance.pawnBaseEnemy);

//		pawnObjs [ 0].SetNewPawnData (PawnManager.instance.pawnBasePlayer);
//		pawnObjs [ 1].SetNewPawnData (PawnManager.instance.pawnBasePlayer);
//		pawnObjs [ 3].SetNewPawnData (PawnManager.instance.pawnBasePlayer);
//		pawnObjs [ 4].SetNewPawnData (PawnManager.instance.pawnBasePlayer);
//		pawnObjs [20].SetNewPawnData (PawnManager.instance.pawnBaseEnemy);
//		pawnObjs [21].SetNewPawnData (PawnManager.instance.pawnBaseEnemy);
//		pawnObjs [23].SetNewPawnData (PawnManager.instance.pawnBaseEnemy);
//		pawnObjs [24].SetNewPawnData (PawnManager.instance.pawnBaseEnemy);

	}

	void Update () {

	}

	public PawnData nowSelectPawn = null;
	public void SelectPawn (PawnData p_data) {
		nowSelectPawn = p_data;
	}

	public IEnumerator AddNowSelectPawn (int p_index) {
		if (nowSelectPawn != null) {
			yield return StartCoroutine( AddPawn (nowSelectPawn, p_index));
			nowSelectPawn = null;
		}
	}

	public IEnumerator AddPawn (PawnData p_data, int p_index) {
		yield return new WaitForSeconds (1f * GameManager.instance.animeRate);

		pawnObjs [p_index].SetNewPawnData (p_data);
		pawnObjs [p_index].anime.Play ("In", -1, 0);
		SoundManager.Play ("MainSoundTable", "PawnIn");

		yield return new WaitForSeconds (1f * GameManager.instance.animeRate);
	}

	public IEnumerator AllPawnMove (E_PawnSide p_side) {
		string _log = "";

		for(int _nowIndex=0; _nowIndex<pawnCount; _nowIndex++){
			PawnObj _pawn = pawnObjs[_nowIndex];
			int _x = (int)(_nowIndex % size.x);
			int _y = (int)(_nowIndex / size.x);
			Vector2 _nowPos = new Vector2 (_x, _y);
			if (_pawn.data.typeData.side == p_side) {
				if (_pawn.alreadyMove) {
					_log += "pawn " + _nowPos.ToString () + " Already Move\n";
					continue;
				}

				if (GetAtkIndexList (_nowPos, _pawn.data.typeData).Count > 0) {
					_log += "pawn " + _nowPos.ToString () + " Can Atk\n";
					continue;
				}

				_log += "pawn " + _nowPos.ToString () + "\n";


				List<int> _targetIndexList = new List<int> ();

				for (int _moveIndex = 0; _moveIndex < 4; _moveIndex++) {
					Vector2 _offset = moveOffsets [_moveIndex];
					Vector2 _targetPos = _nowPos + _offset;
					if (IntExtend.isInRange ((int)_targetPos.x, 0, (int)size.x - 1) && IntExtend.isInRange ((int)_targetPos.y, 0, (int)size.y - 1)) {
						int _targetIndex =  (int)(_targetPos.x + (_targetPos.y * size.x));
						PawnObj _targetPawn = pawnObjs [_targetIndex];
						if (_targetPawn.data.typeData.side == E_PawnSide.None) {
							_log += " *pos " + _targetPos.ToString () + " Can Move\n";
							_targetIndexList.Add (_targetIndex);
						} else {
							_log += "  pos " + _targetPos.ToString () + " Is " + _targetPawn.data.typeData.side.ToString () + "\n";
						}
					} else {
						_log += "  pos " + _targetPos.ToString () + " Out Range" + "\n";
					}
				}

				Vector2 _enemyWay = Vector2.zero;
				for(int _checkIndex=0; _checkIndex<pawnCount; _checkIndex++){
					PawnObj _checkPawn = pawnObjs[_checkIndex];
					if (_checkPawn.data.typeData.side == E_PawnSide.None) {						
					} else if (_checkPawn.data.typeData.side == p_side) {
					} else {
						int _checkX = (int)(_nowIndex % size.x);
						int _checkY = (int)(_nowIndex / size.x);
						Vector2 _enemyPos = new Vector2 (_checkX, _checkY);
						_enemyWay += _enemyPos;
					}
				}
				_enemyWay.Normalize ();



				int _len = _targetIndexList.Count;
				if (_len > 0) {
					float _sqrDistance = 999;
					int _bestMoveIndex = -1;
					int _moveX = 0;
					int _moveY = 0;
					foreach (int _moveIndex in _targetIndexList) {
						_moveX = (int)(_moveIndex % size.x);
						_moveY = (int)(_moveIndex / size.x);
						Vector2 _moveWay = new Vector2 (_moveX, _moveY) - _nowPos;
						float _nowSqrDistance = (_enemyWay - _moveWay).sqrMagnitude;
						if (_nowSqrDistance < _sqrDistance) {
							_sqrDistance = _nowSqrDistance;
							_bestMoveIndex = _moveIndex;
						}
					}

					_moveX = (int)(_bestMoveIndex % size.x);
					_moveY = (int)(_bestMoveIndex / size.x);
					_log += " @ select " + _moveX + "," + _moveY + "\n";
					StartCoroutine(PawnMove (_nowIndex,  _bestMoveIndex));
				}
			} else {
				_log += "pawn " + _nowPos.ToString () + " Is " + _pawn.data.typeData.side.ToString() + "\n";
			}
		}
		Debug.Log (_log);

		for (int f = 0; f < pawnCount; f++) {
			PawnObj _pawn = pawnObjs[f];
			_pawn.alreadyMove = false;
		}

		yield return new WaitForSeconds (0.3f * GameManager.instance.animeRate);

		switch (GameManager.instance.state) {
		case E_GAME_STATE.PlayerMove:
			GameManager.instance.ChangeState (E_GAME_STATE.PlayerAttack);
			break;
		case E_GAME_STATE.EnemyMove:
			GameManager.instance.ChangeState (E_GAME_STATE.EnemyAttack);
			break;
		}

		yield return null;
	}

	public List<int> GetAtkIndexList (Vector2 p_nowPos, PawnTypeData p_typeData) {
		List<Vector2> _rangeList = new List<Vector2> ();
		List<int> _targetIndexList = new List<int> ();

		if (p_typeData.allRange) {
			for (int _y = 0; _y < size.y; _y++) {
				for (int _x = 0; _x < size.x; _x++) {
					_rangeList.Add (new Vector2(_x, _y) - p_nowPos);
				}
			}
		} else {
			foreach (AtkRangeData _atkRange in p_typeData.atkRanges) {
				for (int f = 1; f <= _atkRange.length; f++) {
					_rangeList.Add (_atkRange.range*f);
				}
			}
		}

		foreach(Vector2 _range in _rangeList){
			Vector2 _targetPos = p_nowPos + _range;

			if (IntExtend.isInRange ((int)_targetPos.x, 0, (int)size.x - 1) && IntExtend.isInRange ((int)_targetPos.y, 0, (int)size.y - 1)) {
				int _targetIndex = (int)(_targetPos.x + (_targetPos.y * size.x));
				PawnObj _targetPawn = pawnObjs [_targetIndex];
				if (_targetPawn.data.typeData.side == E_PawnSide.None) {

				} else if (_targetPawn.data.typeData.side == p_typeData.side) {
					
				} else {
					_targetIndexList.Add (_targetIndex);
				}
			}
		}
		return _targetIndexList;
	}

	public IEnumerator AllPawnAtk (E_PawnSide p_side) {
		for (int _nowIndex = 0; _nowIndex < pawnCount; _nowIndex++) {
			PawnObj _pawn = pawnObjs[_nowIndex];
			int _x = (int)(_nowIndex % size.x);
			int _y = (int)(_nowIndex / size.x);
			Vector2 _nowPos = new Vector2 (_x, _y);

			if (_pawn.data.typeData.side == p_side) {
				List<int> _targetIndexList = GetAtkIndexList(_nowPos, _pawn.data.typeData);


				int _len = _targetIndexList.Count;
				if (_len > 0) {
					int _random = Random.Range (0, _len);
					int _otherIndex = _targetIndexList [_random];
					yield return StartCoroutine(PawnAtk (_nowIndex,  _otherIndex));
				}
			}
		}

		GameManager.instance.cmrTargetPos = pawnObjs [12].transform.position;

		switch (GameManager.instance.state) {
		case E_GAME_STATE.PlayerAttack:
			yield return new WaitForSeconds (1f * GameManager.instance.animeRate);
			GameManager.instance.ChangeState (E_GAME_STATE.EnemyMove);
			break;
		case E_GAME_STATE.EnemyAttack:
			yield return new WaitForSeconds (1f * GameManager.instance.animeRate);
			if (GameManager.instance.autoFight) {
				GameManager.instance.ChangeState (E_GAME_STATE.PlayerMove);
			}else{
				GameManager.instance.ChangeState (E_GAME_STATE.PlayerNewFate);
			}
			break;
		}
	}

	public IEnumerator PawnMove (int p_startIndex, int p_targetIndex) {
		PawnObj _startObj = pawnObjs [p_startIndex];
		PawnObj _targetObj = pawnObjs [p_targetIndex];

		_targetObj.SetPawnData (_startObj.data);
		_targetObj.alreadyMove  = true;
		_targetObj.transform.position = _startObj.transform.position;
		_targetObj.SetHp (_startObj.hp);
		_startObj.SetNewPawnData (PawnManager.instance.pawnNull);

		yield return new WaitForSeconds (1f * GameManager.instance.animeRate);
	}

	public IEnumerator PawnAtk (int p_atkIndex, int p_targetIndex) {
		PawnObj _atkObj = pawnObjs [p_atkIndex];
		PawnObj _targetObj = pawnObjs [p_targetIndex];


		GameManager.instance.cmrTargetPos = (_atkObj.transform.position + _targetObj.transform.position)*0.5f;

		for (int f = 0; f < pawnCount; f++) {
			PawnObj _pawn = pawnObjs[f];
			_pawn.anime.Play("NotFocus", -1, 0);
		}
		_atkObj.anime.Play("Atk", -1, 0);
		_targetObj.anime.Play("Hit", -1, 0);

		yield return new WaitForSeconds (0.3f * GameManager.instance.animeRate);
		if (_atkObj.data.typeData.atkEffect != null) {
			GameObject _effect = Instantiate (_atkObj.data.typeData.atkEffect);
			_effect.transform.position = _targetObj.transform.position;
		}
		if (_atkObj.data.typeData.atkSoundName != "") {
			SoundManager.Play ("MainSoundTable", _atkObj.data.typeData.atkSoundName);
		}

		GameManager.instance.CanvasAnimator.Play ("Atk", -1, 0);
		if (_atkObj.data.typeData.side == E_PawnSide.Enemy) {
			GameManager.instance.playerImage.sprite = _targetObj.data.typeData.image;
			GameManager.instance.enemyImage.sprite = _atkObj.data.typeData.image;
		} else {
			GameManager.instance.playerImage.sprite = _atkObj.data.typeData.image;
			GameManager.instance.enemyImage.sprite = _targetObj.data.typeData.image;
		}

		yield return new WaitForSeconds (0.2f * GameManager.instance.animeRate);
//		Debug.Log ("Hit atk = " + _atkObj.data.atk + ", " + _targetObj.hp + " -> " + (_targetObj.hp - _atkObj.data.atk));
		_targetObj.SetHp (_targetObj.hp - _atkObj.data.atk);
		yield return new WaitForSeconds (0.5f * GameManager.instance.animeRate);

		for (int f = 0; f < pawnCount; f++) {
			PawnObj _pawn = pawnObjs[f];
			_pawn.anime.Play("Idle", -1, 0);
		}

		if (_targetObj.hp <= 0) {
			_targetObj.anime.Play("Die", -1, 0);
			SoundManager.Play ("MainSoundTable", "PawnDie");
			yield return new WaitForSeconds (1f * GameManager.instance.animeRate);

			_targetObj.SetNewPawnData (PawnManager.instance.pawnNull);
		}
	}
}
