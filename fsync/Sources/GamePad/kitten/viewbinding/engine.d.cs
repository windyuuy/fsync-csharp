
namespace graphengine
{
	using number = System.Double;

	public interface IDrawable
	{
		void destroy();
		void setColor(string color);
		void setPos(number x, number y);
		bool visible { get; set; }
	}

	public interface ISprite : IDrawable
	{
		void setSize(number width, number height);
		void setRadius(number radius);
	}

	public interface IGraph
	{
		public ISprite createSprite();
	}
}
