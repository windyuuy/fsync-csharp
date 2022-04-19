
namespace fsync.amath
{
	public class Rect : IWHRectSpec
	{

		public Rect()
		{
			this.x = 0;
			this.y = 0;
			this.width = 0;
			this.height = 0;
		}

		public static Vector2 top(IWHRectSpec self)
		{
			return new Vector2(self.x, self.y + self.height / 2);
		}

		public static Vector2 bottom(IWHRectSpec self)
		{
			return new Vector2(self.x, self.y - self.height / 2);
		}

		public static Vector2 center(IWHRectSpec self)
		{
			return new Vector2(self.x, self.y);
		}

		public static T copyRectLike<T>(T self, IWHRectSpec rect) where T : IWHRectSpec
		{
			var x = rect.x;
			var y = rect.y;
			var width = rect.width;
			var height = rect.height;
			self.x = x;
			self.y = y;
			self.width = width;
			self.height = height;
			return self;
		}


	}


}

