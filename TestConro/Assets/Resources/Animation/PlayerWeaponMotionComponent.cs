using UnityEngine;
using System.Collections;

/// <summary>
/// 变换武器的动作 通过切换Tween对象来实现武器动作切换
/// </summary>
public class PlayerWeaponMotionComponent : MonoBehaviour {
	public Transform mHandBone;
	public GameObject mMoveMotion;
	public GameObject mAttackMotion;
	public GameObject mSkillMotion;

	private bool mIsAttack=false;
	private bool mIsSkill=false;


	// Use this for initialization
	void Start () {
		PlayMove ();
	}
	
	// Update is called once per frame
	void Update () {
		Tick ();
	}

	void Tick()
	{
		if (mIsAttack) {
			mHandBone.localPosition = mAttackMotion.transform.localPosition;
			mHandBone.localRotation = mAttackMotion.transform.localRotation;

		} else if (mIsSkill) {
			mHandBone.localPosition = mSkillMotion.transform.localPosition;
			mHandBone.localRotation = mSkillMotion.transform.localRotation;
			
		} else {
			mHandBone.localPosition = mMoveMotion.transform.localPosition;
			mHandBone.localRotation = mMoveMotion.transform.localRotation;
		}
	}

	/// <summary>
	/// loop
	/// </summary>
	public void PlayMove()
	{
		PlayTween (mMoveMotion);
	}

	public void PlayAttack()
	{
		mIsAttack = true;
		PlayTween (mAttackMotion);
	}

	/// <summary>
	/// Tween finish call back
	/// </summary>
	public void OnAttackFinish()
	{
		mIsAttack = false;
	}

	/// <summary>
	/// loop need stop
	/// </summary>
	public void PlaySkill()
	{
		mIsSkill = true;
		PlayTween (mSkillMotion);
	}

	/// <summary>
	/// stop skill
	/// </summary>
	public void StopSkill()
	{
		mIsSkill = false;
	}

	void PlayTween(GameObject target)
	{
		UITweener[]  tweens=target.GetComponents<UITweener> ();
		for (int i = 0; i < tweens.Length; i++) {
			tweens [i].ResetToBeginning ();
			tweens [i].PlayForward ();
		}
	}

}
	
