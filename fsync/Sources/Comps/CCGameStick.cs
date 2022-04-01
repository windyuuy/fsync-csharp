
// const { ccclass, property, menu } = cc._decorator;

// @ccclass("CCGameStick")
// @menu("gccuit/CCGameStick")
using gcc.uit;
using kitten.gamepad;
using UnityEngine;

public class CCGameStick : MonoBehaviour, IUIGameStick
{

	protected gcc.uit.UIGameStickHandler delegate1;

	/**
	 * 整体节点
	 */
	// @property({ type: cc.Node, displayName: "整体节点" })
	[SerializeField]
	UnityEngine.GameObject? viewNode = null;
	public GameObject ViewNode { get => viewNode; set => viewNode = value; }

	/**
	 * 触摸区域
	 */
	// @property({ type: cc.Node, displayName: "触控范围", })
	[SerializeField]
	UnityEngine.GameObject? stickRange = null;
	public GameObject StickRange { get => stickRange; set => stickRange = value; }
	public GameObject StickCenter { get => stickCenter; set => stickCenter = value; }
	public GameObject StickTouchPoint { get => stickTouchPoint; set => stickTouchPoint = value; }

	/**
	 * 摇杆中心视图
	 */
	// @property({ type: cc.Node, displayName: "滑动区域", tooltip: "控制摇杆触点的滑动区域", })
	[SerializeField]
	UnityEngine.GameObject? stickCenter = null;

	/**
	 * 摇杆触摸点视图
	 */
	// @property({ type: cc.Node, displayName: "触点" })
	[SerializeField]
	UnityEngine.GameObject? stickTouchPoint = null;
	public kitten.gamepad.CircleStick? stick = null;

	public CCGameStick() : base()
	{
		this.delegate1 = new gcc.uit.UIGameStickHandler();
		this.delegate1.loadFromJson(this);
	}


	public void syncViewData(kitten.gamepad.CircleStick stick)
	{
		this.delegate1.syncViewData(stick);
	}

	public void updateView()
	{
		this.delegate1.updateView();
	}
}
