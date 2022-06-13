
namespace kitten.gamepad
{

	/**
	 * 自动重定位的摇杆
	 */
	public class GameStick : CircleStick
	{

		/**
		 * 玩家放开触摸摇杆时,摇杆中心点和当前触摸点复位
		 */
		protected bool needResetAfterLoose;

		public override CircleStick init(string id, StickSharedState sharedState)
		{
			this.needResetAfterLoose = false;
			base.init(id, sharedState);
			return this;
		}

		/// <summary>
		/// 针对附带初始偏移的情况作处理
		/// </summary>
		protected virtual void updateTouchPoint()
        {

        }

		protected virtual void updateCtrlStatus()
        {
			/**
			* 游戏摇杆的特性:
			* - 当玩家第一次触摸摇杆时,摇杆的触摸起点要设置为当前触摸点
			* - 玩家放开触摸摇杆时,摇杆中心点和当前触摸点复位
			*/
			if (this.ctrlStatusRaw.touchAction == TTouchAction.Begin)
			{
				this.needResetAfterLoose = false;
				this.setStartPos(this.ctrlStatusRaw.touchPoint);
				this.updateTouchPoint();
				this.calcTouchVector();
			}
			else if (this.ctrlStatusRaw.touchAction == TTouchAction.End)
			{
				this.needResetAfterLoose = true;
				this.updateTouchPoint();
				this.calcTouchVector();
			}
			else
			{
				if (this.needResetAfterLoose)
				{
					this.needResetAfterLoose = false;
					this.resetStartPos();
					this.resetTouchPoint();
				}
				this.updateTouchPoint();
				this.calcTouchVector();
			}

		}

		public override void updateStatus()
		{
			this.updateTouchAction();
			this.updateCtrlStatus();
			// this.exportTouchStatus()
		}

	}

}
