namespace kitten.gamepad
{
	using number = System.Double;
	using fsync.amath;

	public class MouseStick : GameStick
	{

		public override bool handlerInput(fsync.UserInputData data)
		{
			if (base.handlerInput(data))
			{
				// pass
			}
			else if (this.detectSkillRollInput(data))
			{
				// pass
			}
			else
			{
				return false;
			}
			return true;
		}

		/**
		 * 检测鼠标控制技能方向
		 * @param data 
		 */
		protected virtual bool detectSkillRollInput(fsync.UserInputData data)
		{
			// if (data.action == "onmousedown")
			// {
			// 	this.ctrlStatusRaw.pressed = true;

			// }
			// else if (data.action == "onmouseup")
			// {
			// 	this.ctrlStatusRaw.pressed = false;

			// }
			// else if (data.action == "onmousemove")
			// {
			// 	var ctrlPos = this.ctrlStatusRaw.ctrlPos;
			// 	var offset = new number[] { data.event1.clientX - ctrlPos.x, data.event1.clientY - ctrlPos.y };
			// 	var strength = System.Math.Sqrt(offset[0] * offset[0] + offset[1] * offset[1]);
			// 	this.ctrlStatusRaw.dir.x = offset[0] / strength;
			// 	this.ctrlStatusRaw.dir.y = offset[1] / strength;
			// 	this.ctrlStatusRaw.strength = strength;
			// }
			// else
			// {
			// 	return false;
			// }
			// return true;

			if (data.action == "onmousedown")
			{
				if (this.ctrlStatusRaw.pressed)
				{
					return true;
				}

				var t = data.event1;
				var pos = Vector3.fromNumArray(new number[] { t.clientX, t.clientY });
				if (BLRect.containPoint_s(this.getTouchRange(), pos))
				{
					this.ctrlStatusRaw.pressed = true;
					this.ctrlStatusRaw.touchPoint.x = pos.x;
					this.ctrlStatusRaw.touchPoint.y = pos.y;
					Vector.normalizeSelf(this.ctrlStatusRaw.dir);
				}
			}
			else if (data.action == "onmouseup")
			{
				if (this.ctrlStatusRaw.pressed)
				{
					var t = data.event1;
					var pos = Vector3.fromNumArray(new number[] { t.clientX, t.clientY });
					this.ctrlStatusRaw.pressed = false;
					this.ctrlStatusRaw.touchPoint.x = pos.x;
					this.ctrlStatusRaw.touchPoint.y = pos.y;
				}
			}
			else if (data.action == "onmousemove")
			{
				if (this.ctrlStatusRaw.pressed)
				{
					var t = data.event1;
					var pos = Vector3.fromNumArray(new number[] { t.clientX, t.clientY });
					this.ctrlStatusRaw.touchPoint.x = pos.x;
					this.ctrlStatusRaw.touchPoint.y = pos.y;
					Vector.normalizeSelf(this.ctrlStatusRaw.dir);
				}
			}
			else
			{
				return false;
			}
			return true;
		}

	}

	/**
	 * 主技能摇杆
	 */
	public class MainSkillStick : MouseStick
	{

	}

}