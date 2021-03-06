
namespace graph
{
	using ISprite = graphengine.ISprite;

	public class Graph
	{
		public static readonly Graph graph = new Graph();

		protected graphengine.IGraph _graph;
		public void setNativeGraph(graphengine.IGraph graph)
		{
			this._graph = graph;
		}

		public ISprite createSprite()
		{
			return _graph.createSprite();
		}

		public class SystemEvent
		{
			public object data;
		}

		public fsync.Event.SEvent<string, SystemEvent> systemEventCenter;
		public fsync.Event.SEvent<string, SystemEvent> getSystemEvent()
		{
			if (systemEventCenter == null)
			{
				systemEventCenter = new fsync.Event.SEvent<string, SystemEvent>();
			}
			return systemEventCenter;
		}
	}
	public class PredefSystemEvent
	{
		public static string GameFinished = "GameFinished";
	}

}
