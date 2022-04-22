namespace kitten.gamepad
{
	using System;
	using System.Collections.Generic;
	using fsync.amath;
	using UserInput = fsync.UserInput;
	using number = System.Double;

	/**
	* 操控状态
	* - move 移动中
	* - begin 刚开始点击
	* - end 刚刚松开
	* - loosed 松开状态
	*/
	// using TTouchAction = System.String;// "move" | "begin" | "end" | "loosed"
	public enum TTouchAction
	{
		Loosed = 0,
		Begin,
		Move,
		End,
	}

	/**
	 * 前一次摇杆状态
	 */
	public class StickLastCtrlState
	{
		public bool pressed = false;
	}

	/**
	 * 摇杆状态
	 */
	public class StickCtrlState
	{
		public static bool IsChanged(StickCtrlState ctrlState, bool pressed, Vector3 pos)
		{
			return ctrlState.pressed != pressed || ctrlState.touchPoint.x != pos.x || ctrlState.touchPoint.y != pos.y;
		}

		/**
		 * 当前触摸位置
		 */
		public Vector3 touchPoint = new Vector3();
		/**
		 * 操控方向
		 */
		public Vector3 dir = new Vector3();
		/**
		 * 操作强度
		 */
		public number strength = 0;
		/**
		 * 操作强度无效
		 */
		public bool isStrengthInvalid = false;
		/**
		 * 是否处于按下状态
		 */
		public bool pressed = false;
		/**
		 * 触摸操控状态
		 */
		public TTouchAction touchAction = TTouchAction.Loosed;

		/**
		 * 控制器轴心位置
		 */
		public Vector3 ctrlPos = new Vector3();
	}
}