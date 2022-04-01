
/**
 * 客户端UI工具
 */
namespace kitten
{
	/**
	 * 手势分析
	 */
	namespace guesture { }
	/**
	 * 游戏手柄
	 */
	namespace gamepad { }
	/**
	 * ui事件
	 */
	namespace uievent { }

	namespace manager
	{
		public class ModulesInitConfig
		{
			public fsync.amath.Vector3 screenSize;
			public graphengine.IGraph graphImpl;
		}

		public class Manager
		{
			public static readonly Manager manager = new Manager();

			public void init(ModulesInitConfig config)
			{
				fsync.Device.device.init(config.screenSize);
				graph.Graph.graph.setNativeGraph(config.graphImpl);
				kitten.uievent.UIEventHandler.uiEventHandler.enableUIEvent();
				kitten.uievent.UIEventHandler.uiEventHandler.postInitEvent();
			}

			public void update()
			{
				WTC.DOM.Document.document.Update();
			}
		}
	}

}
