
namespace fsync.amath
{
	public interface Transform
	{
		Transform Parent { get; set; }
		Vector3 LocalPosition { get; set; }
		Vector3 LocalScale { get; set; }
		Quaternion LocalRotation { get; set; }

	}
}

