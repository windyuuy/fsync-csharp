
namespace kitten.gamepad
{

	using System;
	using System.Linq;
	using System.Collections.Generic;
	using fsync.amath;
	using number = System.Double;

	using UserInput = fsync.UserInput;

	/**
	 * 环状摇杆
	 */
	public class CircleStick
	{

		/**
		 * 是否启用
		 */
		public bool enable = true;

		/**
		 * 控制器id
		 */
		public string identity = "unkown";

		/**
		* 控制器轴心起始位置
		*/
		protected Vector3 ctrlPosOrigin;

		/**
		 * 获取输入端口列表
		 */
		public virtual List<string> getInputPorts()
		{
			return this.inputPorts;
		}

		protected List<string> inputPorts;
		protected virtual void updateInputPorts()
		{
			this.inputPorts = this.multiTouchMap.Keys.Where((key) =>
			{
				return this.multiTouchMap[key] == this.identity;
			}).ToList();
		}

		public virtual StickCtrlState ctrlStatus
		{
			get
			{
				return this.ctrlStatusRaw;
			}
		}

		/**
		 * 获取触控范围中心店
		 */
		public virtual Vector3 getCtrlCenterPos()
		{
			return this.ctrlStatus.ctrlPos;
		}

		/**
		 * 获取摇杆位置
		 */
		public virtual Vector3 getCtrlTouchPoint()
		{
			return this.ctrlStatus.touchPoint;
		}

		protected StickLastCtrlState lastCtrlStatus;

		/**
		 * 控制器内部状态
		 */
		protected StickCtrlState ctrlStatusRaw;

		/**
		 * 控制器对外状态
		 */
		// ctrlStatus: StickCtrlState

		// resetExportStatus() {
		// 	this.ctrlStatus.pressed = false
		// 	this.ctrlStatus.touchAction = "loosed"
		// 	this.ctrlStatus.strength = 0
		// 	Vector.merge(this.ctrlStatus.ctrlPos, this.ctrlPosOrigin)
		// 	Vector.merge(this.ctrlStatus.touchPoint, this.ctrlPosOrigin)
		// 	Vector.resetValues(this.ctrlStatus.dir, 0)
		// }

		// /**
		//  * 导出状态
		//  */
		// publicTouchStatus() {
		// 	this.ctrlStatus.pressed = this.ctrlStatusRaw.pressed
		// 	Vector.merge(this.ctrlStatus.dir, this.ctrlStatusRaw.dir)
		// 	this.ctrlStatus.strength = this.ctrlStatusRaw.strength
		// 	this.ctrlStatus.touchAction = this.ctrlStatusRaw.touchAction
		// 	Vector.merge(this.ctrlStatus.touchPoint, this.ctrlStatusRaw.touchPoint)
		// 	Vector.merge(this.ctrlStatus.ctrlPos, this.ctrlStatusRaw.ctrlPos)
		// }

		public virtual void updateStatus()
		{
			this.updateTouchAction();
			this.calcTouchVector();
			// this.publicTouchStatus()
		}

		public virtual void updateTouchAction()
		{
			if (this.lastCtrlStatus.pressed)
			{
				if (this.ctrlStatusRaw.pressed)
				{
					// press -> press
					this.ctrlStatusRaw.touchAction = TTouchAction.Move;
				}
				else
				{
					// press -> loosed
					this.ctrlStatusRaw.touchAction = TTouchAction.End;
				}
			}
			else
			{
				if (this.ctrlStatusRaw.pressed)
				{
					// loosed -> press
					this.ctrlStatusRaw.touchAction = TTouchAction.Begin;
				}
				else
				{
					// loosed -> loosed
					this.ctrlStatusRaw.touchAction = TTouchAction.Loosed;
				}
			}
			this.lastCtrlStatus.pressed = this.ctrlStatusRaw.pressed;

		}

		/**
		 * 计算触摸矢量数据
		 */
		public virtual void calcTouchVector()
		{
			var ctrlPos = this.ctrlStatusRaw.ctrlPos;
			var pos = this.ctrlStatusRaw.touchPoint;
			this.ctrlStatusRaw.dir.x = pos.x - ctrlPos.x;
			this.ctrlStatusRaw.dir.y = pos.y - ctrlPos.y;
			this.ctrlStatusRaw.strength = this.ctrlStatusRaw.dir.len;
		}

		public virtual CircleStick init(string id, StickSharedState sharedState)
		{
			this.identity = id;
			this.ctrlPosOrigin = new Vector3();
			this.touchRange = new IWHRectSpec()
			{
				height = 0,
				width = 0,
				x = 0,
				y = 0,
			};
			// this.ctrlStatus = new StickCtrlState();
			this.ctrlStatusRaw = new StickCtrlState();
			this.lastCtrlStatus = new StickLastCtrlState();
			this.inputPorts = new List<string>();

			this.sharedState = sharedState;

			return this;
		}

		/**
		 * 动态设置当前摇杆中心点
		 * @param pos 
		 */
		public virtual void setStartPos(Vector3 pos)
		{
			Vector.merge(this.ctrlStatusRaw.ctrlPos, pos);
		}

		/**
		 * 重置当前摇杆中心为原始中心点
		 */
		public virtual void resetStartPos()
		{
			this.setStartPos(this.ctrlPosOrigin);
		}

		public virtual void resetTouchPoint()
		{
			Vector.merge(this.ctrlStatusRaw.touchPoint, this.ctrlStatusRaw.ctrlPos);
		}

		/**
		 * 设置主视图
		 * @param pos 
		 */
		public virtual void setStartPosOrigin(Vector3 pos)
		{
			Vector.merge(this.ctrlPosOrigin, pos);
			Vector.merge(this.ctrlStatusRaw.ctrlPos, pos);
		}

		/**
		 * 触控半径
		 */
		protected number circleRadius = 10;

		/**
		 * 设置触控半径
		 * @param radius 
		 */
		public virtual void setCircleRadius(number radius)
		{
			this.circleRadius = radius;
		}

		/**
		 * 获取触控半径
		 * @param radius
		 */
		public virtual number getCircleRadius()
		{
			return this.circleRadius;
		}

		IWHRectSpec touchRange;

		/**
		 * 设置触控范围
		 * @param rect 
		 */
		public virtual void setTouchRange(IWHRectSpec rect)
		{
			this.touchRange.height = rect.height;
			this.touchRange.width = rect.width;
			this.touchRange.x = rect.x;
			this.touchRange.y = rect.y;
		}

		/**
		 * 获取触控范围
		 */
		protected virtual IWHRectSpec getTouchRange()
		{
			// var width = this.circleRadius
			// var height = this.circleRadius
			// return {
			// 	x: this.ctrlPos.x - width / 2,
			// 	y: this.ctrlPos.y - height / 2,
			// 	width: width,
			// 	height: height,
			// }
			return this.touchRange;
		}

		/**
		 * 处理触控输入
		 * @param data 
		 */
		public virtual bool handlerInput(fsync.UserInputData data)
		{
			if (!this.enable)
			{
				this.cleanTouchMap();
				return false;
			}

			if (this.detectVirtualCirleInput(data))
			{
				// pass
			}
			else
			{
				return false;
			}
			return true;
		}

		public virtual void cleanTouchMap()
		{
			foreach (var key in this.multiTouchMap)
			{
				this.sharedState.multiTouchMap.Remove(key.Key);
			}
		}

		/**
		 * 触摸状态map
		 */
		protected Dictionary<string, string> multiTouchMap = new Dictionary<string, string>();
		// protected static multiTouchMap: { [id: string]: string } = fsync.EmptyTable()
		protected StickSharedState sharedState;

		/**
		 * 检测虚拟手柄输入
		 * @param data 
		 */
		protected virtual bool detectVirtualCirleInput(fsync.UserInputData data)
		{
			if (data.action == "ontouchstart")
			{
				foreach (var t in data.event1.touches)
				{
					if (this.sharedState.multiTouchMap.ContainsKey(t.identifier))
					{
						continue;
					}

					var pos = Vector3.fromNumArray(new number[] { t.clientX, t.clientY });
					if (BLRect.containPoint_s(this.getTouchRange(), pos))
					{
						this.sharedState.multiTouchMap[t.identifier] = this.identity;
						this.multiTouchMap[t.identifier] = this.identity;
						this.ctrlStatusRaw.pressed = true;
						this.ctrlStatusRaw.touchPoint.x = pos.x;
						this.ctrlStatusRaw.touchPoint.y = pos.y;
						Vector.normalizeSelf(this.ctrlStatusRaw.dir);
					}
				}
			}
			else if (data.action == "ontouchend" || data.action == "ontouchcancel")
			{
				foreach (var t in data.event1.touches)
				{
					if (this.multiTouchMap[t.identifier] == this.identity)
					{
						var pos = Vector3.fromNumArray(new number[] { t.clientX, t.clientY });
						this.ctrlStatusRaw.pressed = false;
						this.ctrlStatusRaw.touchPoint.x = pos.x;
						this.ctrlStatusRaw.touchPoint.y = pos.y;
						this.sharedState.multiTouchMap.Remove(t.identifier);
						this.multiTouchMap.Remove(t.identifier);
					}
				}
			}
			else if (data.action == "ontouchmove")
			{
				foreach (var t in data.event1.touches)
				{
					if (this.multiTouchMap[t.identifier] == this.identity)
					{
						var pos = Vector3.fromNumArray(new number[] { t.clientX, t.clientY });
						this.ctrlStatusRaw.touchPoint.x = pos.x;
						this.ctrlStatusRaw.touchPoint.y = pos.y;
						// this.ctrlStatus.dir.x = pos.x - this.ctrlPos.x
						// this.ctrlStatus.dir.y = pos.y - this.ctrlPos.y
						// this.ctrlStatus.strength = Vector.len(this.ctrlStatus.dir)
						Vector.normalizeSelf(this.ctrlStatusRaw.dir);
					}
				}
			}
			else
			{
				return false;
			}
			return true;
		}

	}

}
