namespace fsync
{
	using fsync.amath;

	public interface IView<T>
	{
		W getRaw<W>();
		T getRaw();
		IView<T> init();
		void setPos(Vector3 pos);
		void setScale(Vector3 pos);
		void setRotation(Vector4 quat);
		void destroy();
	}

}