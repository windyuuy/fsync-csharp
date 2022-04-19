
namespace fsync.amath
{
	using number = System.Double;

	/**
	 * BLRect = 左下角 + size
	 */
	public class BLRect : IWHRectSpec
	{

		public BLRect()
		{
			this.x = 0;
			this.y = 0;
			this.width = 0;
			this.height = 0;
		}
		public BLRect(number x, number y, number width, number height)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		public number centerX
		{
			get
			{
				return this.x;
			}
		}

		public number centerY
		{
			get
			{
				return this.y;
			}
		}

		public Vector2 top
		{
			get
			{
				return BLRect.top_s(this);
			}
		}

		public Vector2 bottom
		{
			get
			{
				return BLRect.bottom_s(this);
			}
		}

		public Vector2 center
		{
			get
			{
				return BLRect.center_s(this);
			}
		}

		public number leftX
		{
			get
			{
				return this.x;
			}
		}

		public number rightX
		{
			get
			{
				return this.x + this.width;
			}
		}

		public Vector2 left
		{
			get
			{
				var self = this;
				return new Vector2(self.x, self.y + this.height / 2);
			}
		}

		public Vector2 right
		{
			get
			{
				var self = this;
				return new Vector2(self.x + self.width, self.y + this.height / 2);
			}
		}

		public BLRect fromRectLike(IWHRectSpec spec)
		{
			return BLRect.copyRectLike_s(this, spec);
		}

		public BLRect copyRectLike(IWHRectSpec spec)
		{
			return BLRect.copyRectLike_s(this, spec);
		}

		public BLRect reset()
		{
			return BLRect.reset_s(this);
		}

		public BLRect mergeFrom(IWHRectSpec rect)
		{
			return BLRect.mergeFrom_s(this, rect);
		}

		public BLRect clone()
		{
			return BLRect.clone_s(this);
		}

		public bool containPoint(IVector pt)
		{
			return BLRect.containPoint_s(this, pt);
		}

		/**
		 * 将点就近限制在矩形框内
		 * @param rect 
		 * @param pt 
		 */
		public void limitPointSelf(IVector pt)
		{
			BLRect.limitPointSelf_s(this, pt);
		}

		public static Vector2 top_s(IWHRectSpec self)
		{
			return new Vector2(self.x + self.width / 2, self.y + self.height);
		}

		public static Vector2 bottom_s(IWHRectSpec self)
		{
			return new Vector2(self.x + self.width / 2, self.y);
		}

		public static Vector2 center_s(IWHRectSpec self)
		{
			return new Vector2(self.x + self.width / 2, self.y + self.height / 2);
		}

		public static IWHRectSpec fromRectLike_s(IWHRectSpec self)
		{
			var x = self.x;
			var y = self.y;
			var width = self.width;
			var height = self.height;
			return new BLRect(x, y, width, height);
		}

		public static T copyRectLike_s<T>(T self, IWHRectSpec rect) where T : IWHRectSpec
		{
			self.x = rect.x;
			self.y = rect.y;
			self.width = rect.width;
			self.height = rect.height;
			return self;
		}

		public static T reset_s<T>(T self) where T : IWHRectSpec
		{
			self.x = 0;
			self.y = 0;
			self.width = 0;
			self.height = 0;
			return self;
		}

		public static T mergeFrom_s<T>(T self, IWHRectSpec rect) where T : IWHRectSpec
		{
			self.width = rect.width;
			self.height = rect.height;
			self.x = rect.x;
			self.y = rect.y;
			return self;
		}

		public static BLRect clone_s(IWHRectSpec self)
		{
			var rect = new BLRect();
			BLRect.mergeFrom_s(rect, self);
			return rect;
		}

		public static bool containPoint_s(IWHRectSpec rect, IVector pt)
		{
			var ns = pt.getBinData();
			var x = ns[0];
			var y = ns[1];
			if (
				(rect.x - x) * (rect.x + rect.width - x) <= 0
				&& (rect.y - y) * (rect.y + rect.height - y) <= 0
			)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/**
		 * 将点就近限制在矩形框内
		 * @param rect
		 * @param pt
		 */
		public static void limitPointSelf_s(IWHRectSpec rect, IVector pt)
		{
			var ns = pt.getBinData();
			var x = ns[0];
			var y = ns[1];
			if (x < rect.x)
			{
				x = rect.x;
			}
			var rx = rect.x + rect.width;
			if (x > rx)
			{
				x = rx;
			}
			if (y < rect.y)
			{
				y = rect.y;
			}
			var ry = rect.y + rect.height;
			if (y > ry)
			{
				y = ry;
			}
			ns[0] = x;
			ns[1] = y;
		}
	}


}

