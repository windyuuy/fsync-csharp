// import { UIGameStickHandler } from "./UIGameStickHandler";

// const { ccclass, property, menu } = cc._decorator;

// @ccclass("CCGamepad")
// @menu("gccuit/CCGamepad")

using System;
using UnityEngine;
using System.Collections.Generic;
using gcc.uit;
using number = System.Double;

public class CCGamepad : UnityEngine.MonoBehaviour, IUIGamepad
{
	protected gcc.uit.UIGamepadHandler? delegate1;

	public CCGameStick LeftStick { get => leftStick; set => leftStick = value; }
	public CCGameStick RightStick { get => rightStick; set => rightStick = value; }
	public List<CCGameStick> SkillSticks { get => skillSticks; }

	public bool ToDrawDebugView { get => toDrawDebugView; set => toDrawDebugView = value; }

	// @property({ type: UIGameStickHandler, displayName: "移动摇杆", })
	[SerializeField]
	public CCGameStick? leftStick = null;

	// @property({ type: UIGameStickHandler, displayName: "攻击摇杆", })
	[SerializeField]
	private CCGameStick? rightStick = null;

	// @property({ type: [UIGameStickHandler], displayName: "其他摇杆列表", })
	[SerializeField]
	private List<CCGameStick> skillSticks = new List<CCGameStick>();

	[SerializeField]
	private bool toDrawDebugView = false;

	public void Awake()
	{
		this.delegate1 = new gcc.uit.UIGamepadHandler();
		this.delegate1.loadFromJson(this);
		this.delegate1.onLoad();
	}

	public void Start()
	{
		this.delegate1!.start();
	}

	public void Update()
	{
		this.delegate1!.update();
	}

    public kitten.gamepad.NormalGamepad gamepad
	{
		get
		{
			return this.delegate1!.gamepad;
		}
	}

	/**
	 * 显示隐藏触摸板
	 */
	public void setPadVisible(bool b)
	{
		this.delegate1!.gamepad.inputEnabled = b;
		if (this.gameObject.activeSelf != b)
		{
			try
			{
				this.gameObject.SetActive(b);
			}
			catch (Exception e)
			{
				UnityEngine.Debug.LogError(e);
			}
		}
	}
}
