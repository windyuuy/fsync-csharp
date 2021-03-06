
namespace gcc.uit
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Vector = fsync.amath.Vector;
	using number = System.Double;

	public interface IUIGamepad
	{
		CCGameStick LeftStick { get; set; }
		CCGameStick RightStick { get; set; }
		List<CCGameStick> SkillSticks { get; }
		bool ToDrawDebugView { get; set; }
	}

	// @ccclass("CCGamepad")
	public class UIGamepadHandler
	{

		// @property({ type: CCGameStick, displayName: "左侧摇杆", })
		public CCGameStick leftStick
		{
			get
			{
				return this.data.LeftStick;
			}
			set
			{
				this.data.LeftStick = value;
			}
		}

		// @property({ type: CCGameStick, displayName: "右侧摇杆", })
		public CCGameStick rightStick
		{
			get
			{
				return this.data.RightStick;
			}
			set
			{
				this.data.RightStick = value;
			}
		}

		// @property({ type: [CCGameStick], displayName: "其他摇杆列表", })
		public List<CCGameStick> skillSticks
		{
			get
			{
				return this.data.SkillSticks;
			}

		}

		// @property({ type: Boolean, displayName: "是否显示调试视图", })
		public bool toDrawDebugView
		{
			get
			{
				return this.data.ToDrawDebugView;
			}
			set
			{
				this.data.ToDrawDebugView = value;
			}
		}

		protected IUIGamepad data;
		public void loadFromJson(IUIGamepad data)
		{
			this.data = data;
		}

		public kitten.gamepad.NormalGamepad gamepad;

		public void onLoad()
		{
			this.gamepad = new kitten.gamepad.NormalGamepad().init();

			if (this.leftStick != null)
			{
				this.leftStick.syncViewData(this.gamepad.leftStick);

			}
			if (this.rightStick != null)
			{
				this.rightStick.syncViewData(this.gamepad.rightStick);


			}
			foreach (var stickView in this.skillSticks)
			{
				var stick = new kitten.gamepad.GameStick().init($"skill_{ stickView.StickRange.GetInstanceID()}", this.gamepad.sharedState);

				this.gamepad.virutalCtrls.Add(stick);


				stickView.syncViewData(stick);


			}
			if (this.toDrawDebugView)
			{
				this.gamepad.setupSimpleView();

			}

			this.updateView();
		}

		public void start()
		{

		}

		protected List<CCGameStick> getSkillStickViews()
		{
			var skillStickViews = new List<CCGameStick>();


			if (this.leftStick != null)
			{
				skillStickViews.Add(this.leftStick);

			}
			if (this.rightStick != null)
			{
				skillStickViews.Add(this.rightStick);


			}
			skillStickViews.AddRange(this.skillSticks);

			return skillStickViews;
		}

		public void updateViewVisible()
		{
			var skillStickViews = this.getSkillStickViews();


			var sticks = this.gamepad.virutalCtrls;


			for (int index = 0; index < skillStickViews.Count; index++)
			{
				var view = skillStickViews[index];
				var stick = sticks[index];

				// view.viewNode.active = stick.enable;
				view.ViewNode.SetActive(stick.enable);
			}
		}

		public void updateView()
		{
			this.updateViewVisible();

			var skillStickViews = this.getSkillStickViews();

			var sticks = this.gamepad.virutalCtrls;

			for (int index = 0; index < skillStickViews.Count; index++)
			{
				var stickView = skillStickViews[index];
				var stick = sticks[index];

				stickView.stick = stick;

				stickView.updateView();

			}

			this.gamepad.updateSimpleView(this.toDrawDebugView);
		}

		public void update()
		{
			if (this.gamepad.changedCount > 0 && this.gamepad.inputEnabled)
			{
				this.gamepad.changedCount--;
				this.gamepad.updateVirtualCtrls();
				this.updateView();
			}
		}

		public void setSkillEnabled(number index, bool b)
		{
			this.gamepad.virutalCtrls[(int)index].enable = b;
		}

	}
}
