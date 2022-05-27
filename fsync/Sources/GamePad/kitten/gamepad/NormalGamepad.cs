namespace kitten.gamepad
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using fsync.amath;
	using UserInput = fsync.UserInput;

	/**
	 * 虚拟游戏手柄
	 * - 虚拟设备
	 */
	public class NormalGamepad
	{

		protected bool enable = true;

		/**
		 * 控制输入是否可用
		 */
		public bool inputEnabled
		{
			get
			{
				return this.enable;
			}
			set
			{
				this.enable = value;
			}
		}

		/**
		 * 左手控制器
		 */
		public MoveStick leftStick
		{
			get
			{
				return this.virutalCtrls[0] as MoveStick;
			}
		}
		/**
		 * 左手控制器状态
		 */
		public StickCtrlState leftStickStatus
		{
			get
			{
				return this.leftStick.ctrlStatus;
			}
		}
		/**
		 * 右手控制器
		 */
		public MainSkillStick rightStick
		{
			get
			{
				return this.virutalCtrls[1] as MainSkillStick;
			}
		}
		/**
		 * 右手控制器状态
		 */
		public StickCtrlState rightStickStatus
		{
			get
			{
				return this.rightStick.ctrlStatus;
			}
		}

		/**
		 * 更新手柄状态,包含:
		 * - 延迟状态
		 */
		public virtual void updateVirtualCtrls()
		{
			this.virutalCtrls.ForEach((ctrl) => ctrl.updateStatus());
			// var inputMap: { [key: string]: CircleStick } = {}
			// var overwriteMap: { [key: string]: CircleStick } = {}
			// this.virutalCtrls.forEach((ctrl) => {
			// 	ctrl.getInputPorts().forEach((id) => {
			// 		overwriteMap[id] = ctrl
			// 		inputMap[ctrl.identity] = ctrl
			// 	})
			// })
			// var validMap = {}
			// Object.values(overwriteMap).forEach((ctrl) => {
			// 	validMap[ctrl.identity] = ctrl
			// })
			// // 排除掉被覆盖的输入摇杆
			// Object.values(inputMap).forEach((ctrl) => {
			// 	if (!validMap[ctrl.identity]) {
			// 		ctrl.resetExportStatus()
			// 	}
			// })
		}

		public event Action? OnUpdated = null;
		public virtual void update()
        {
			this.updateVirtualCtrls();

			OnUpdated?.Invoke();
		}

		/**
		 * 摇杆列表
		 */
		public List<CircleStick> virutalCtrls;

		/**
		 * 摇杆共享状态
		 */
		public StickSharedState sharedState;

		public virtual NormalGamepad init()
		{

			this.sharedState = new StickSharedState();


			this.virutalCtrls = new List<CircleStick>();
			while (this.virutalCtrls.Count < 2)
			{
				this.virutalCtrls.Add(null);
			}

			{
				var ctrl = new MoveStick().init("movestick", this.sharedState);
				this.virutalCtrls[0] = ctrl;

				var pos = new Vector3();
				pos.x = UserInput.inst.clientSize.x * 0.2;
				pos.y = UserInput.inst.clientSize.y * 0.8;
				// 默认设置在左边
				ctrl.setStartPosOrigin(pos);
			}
			{
				var ctrl = new MainSkillStick().init("skillstick", this.sharedState);
				this.virutalCtrls[1] = ctrl;


				var pos = new Vector3();
				pos.x = UserInput.inst.clientSize.x * 0.8;
				pos.y = UserInput.inst.clientSize.y * 0.8;
				// 默认设置在右边
				ctrl.setStartPosOrigin(pos);
			}

			this.virtualCtrlViews = new List<CircleStickView>();


			UserInput.inst.addHandler("gamecontroller", (data) =>
			{
				this.handlerInput(data);
			});
			return this;
		}

		/**
		 * 触控调试视图列表
		 */
		protected List<CircleStickView> virtualCtrlViews;

		/**
		 * 创建调试视图
		 */
		public virtual void setupSimpleView()
		{
			while (this.virtualCtrlViews.Count < 2)
			{
				this.virtualCtrlViews.Add(null);
			}

			{
				// 左
				var view = new CircleStickView().init();
				view.setupView(this.virutalCtrls[0], "rgba(200, 255, 255, 1.0)");
				this.virtualCtrlViews[0] = view;
			}
			{
				// 右
				var view = new CircleStickView().init();
				view.setupView(this.virutalCtrls[1], "rgba(255, 200, 255, 1.0)");
				this.virtualCtrlViews[1] = view;
			}

			foreach (var ctrl in this.virutalCtrls.Skip(2))
			{
				// 右
				var view = new CircleStickView().init();
				view.setupView(ctrl, "rgba(255, 255, 200, 1.0)");
				this.virtualCtrlViews.Add(view);
			}

		}

		public virtual void updateSimpleView(bool visible)
		{
			foreach (var ctrl in this.virutalCtrls)
			{
				// 右
				var view = this.virtualCtrlViews.Find(v => v.ctrlId == ctrl.identity);
				if (view != null)
				{
					view.visible = visible;
					view.updateView(ctrl);
				}
			}
		}

		/**
		 * 状态是否改变
		 */
		public int changedCount = 0;

		/**
		 * 处理各类输入
		 * @param data 
		 */
		public virtual bool handlerInput(fsync.UserInputData data)
		{
			if (!this.enable)
			{
				return false;
			}

			// if (data.action == "onsetup") {
			// 	this.setupSimpleView()
			// } else 
			{
				var b = false;
				// for (var ctrl of this.virutalCtrls) {
				// for (var ctrl of this.virutalCtrls.reverse()) {
				for (var i = this.virutalCtrls.Count - 1; i >= 0; i--)
				{
					var ctrl = this.virutalCtrls[i];
					var b2 = ctrl.handlerInput(data);
					b = b || b2;
				}

                if (this.changedCount < 2)
                {
					// 因为松开位置同样也是当前位置, 所以松开时还在按下位置处
					this.changedCount = b ? 2 : 0;
				}

				return b;
			}
		}

	}
}