namespace fsync
{

	using WTC.DOM;

	public class Platform
	{
		public static readonly Platform platform = new Platform().init();
		public bool isBrowser = false;

		public Platform init()
		{
			this.isBrowser = (Document.document != null);
			return this;
		}
	}

}