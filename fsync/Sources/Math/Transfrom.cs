
namespace fsync.amath
{
	public interface Transform
	{
		Vector3 LocalPosition { get; set; }
		Vector3 LocalScale { get; set; }
		Quaternion LocalRotation { get; set; }

		Vector3 Position { get; set; }
		Vector3 Scale { get; }
		Quaternion Rotation { get; set; }
	}
}

