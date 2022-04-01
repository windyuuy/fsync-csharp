
namespace gcc.uit
{
	using UnityEngine;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using fsync.amath;
	using gcc.common;
	// const { ccclass, property } = cc._decorator;

	// import transformTool = transformTool

	public interface IUIGameStick
	{
		public GameObject ViewNode { get; set; }
		public GameObject StickTouchPoint { get; set; }
		public GameObject StickRange { get; set; }
		public GameObject StickCenter { get; set; }
	}

	// @ccclass("CCGamepad")
	public class UIGameStickHandler
	{

		/**
		 * 整体节点
		 */
		// @property({ type: GameObject, displayName: "整体节点" })
		public GameObject viewNode
		{
			get
			{
				return this.data.ViewNode;
			}
			set
			{
				this.data.ViewNode = value;
			}
		}

		/**
		 * 触摸区域
		 */
		// @property({ type: GameObject, displayName: "触控范围", })
		public GameObject stickRange
		{
			get
			{
				return this.data.StickRange;
			}
			set
			{
				this.data.StickRange = value;
			}
		}

		/**
		 * 摇杆中心视图
		 */
		// @property({ type: GameObject, displayName: "滑动区域", tooltip: "控制摇杆触点的滑动区域", })
		public GameObject stickCenter
		{
			get
			{
				return this.data.StickCenter;
			}
			set
			{
				this.data.StickCenter = value;
			}
		}

		/**
		 * 摇杆触摸点视图
		 */
		// @property({ type: GameObject, displayName: "触点" })
		public GameObject stickTouchPoint
		{
			get
			{
				return this.data.StickTouchPoint;
			}
			set
			{
				this.data.StickTouchPoint = value;
			}
		}

		protected IUIGameStick data;
		public void loadFromJson(IUIGameStick data)
		{
			this.data = data;
		}

		public kitten.gamepad.CircleStick stick = null;
		public void syncViewData(kitten.gamepad.CircleStick stick)
		{
			this.stick = stick;

			var stickView = this;

			// 设置触摸范围
			{
				var stickRangeTransform = transformTool.getUITransform(stickView.stickRange);
				UnityEngine.Vector3 worldPos = stickRangeTransform.position;
				var vec = transformTool.convPos3ToVector(worldPos);
				var rect = new BLRect();
				rect.width = stickRangeTransform.rect.width;
				rect.height = stickRangeTransform.rect.height;
				rect.x = vec.x - rect.width / 2;
				rect.y = vec.y - rect.height / 2;
				stick.setTouchRange(rect);
			}
			// 设置触摸中心点
			{
				var stickCenterTransform = transformTool.getUITransform(stickView.stickCenter);
				var vec = transformTool.convPos3ToVector(stickCenterTransform.position);
				stick.setStartPosOrigin(vec);
				var r = (stickCenterTransform.rect.width + stickCenterTransform.rect.height) / 2;
				stick.setCircleRadius(r / 2);
				stick.resetTouchPoint();
			}
		}

		public void updateMainView()
		{
			var stick = this.stick;
			var stickView = this;
			if (stickView.stickCenter)
			{
				// 更新摇杆中心点视图位置
				var ctrlCenter = stick.getCtrlCenterPos();
				var pos = transformTool.getUITransform(stickView.stickCenter.transform.parent).InverseTransformPoint(transformTool.convVectorToPos3(ctrlCenter));
				stickView.stickCenter.transform.localPosition = pos;
			}
			if (stickView.stickTouchPoint)
			{
				// 更新摇杆触摸点视图位置
				var ctrlCenter = stick.getCtrlCenterPos();
				var touchPoint = stick.ctrlStatus.touchPoint;


				var offset = Vector.subDown(touchPoint.clone(), ctrlCenter);
				if (Vector.len(offset) > stick.getCircleRadius() || stick.ctrlStatus.isStrengthInvalid)
				{
					Vector.multUpVar(Vector.normalizeSelf(offset), stick.getCircleRadius());
					var pos = Vector.addUp(offset, ctrlCenter);
					var ccpos = transformTool.getUITransform(stickView.stickTouchPoint.transform.parent).InverseTransformPoint(transformTool.convVectorToPos3(pos));
					stickView.stickTouchPoint.transform.localPosition = ccpos;
				}
				else
				{
					var ccpos = transformTool.getUITransform(stickView.stickTouchPoint.transform.parent).InverseTransformPoint(transformTool.convVectorToPos3(touchPoint));
					stickView.stickTouchPoint.transform.localPosition = ccpos;
				}
			}
		}

		protected TransformTool transformTool = TransformTool.Inst;

		public void updateDetailView()
		{
			var stick = this.stick;
			var stickView = this;
			// 其他更新
			if (stickView.stickTouchPoint)
			{
				if (stick.ctrlStatus.pressed)
				{
					transformTool.setScale(stickView.stickTouchPoint, 1.22);
				}
				else
				{
					transformTool.setScale(stickView.stickTouchPoint, 1);
				}
			}
		}
		public void updateView()
		{
			var stick = this.stick;
			if (stick == null)
			{
				return;
			}

			this.updateMainView();
			this.updateDetailView();
		}

	}
}
