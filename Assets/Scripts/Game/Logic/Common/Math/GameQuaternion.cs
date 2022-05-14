using Game.Assets.Scripts.Game.Logic.Common.Helpers;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Numerics;

namespace Game.Assets.Scripts.Game.Logic.Common.Math
{
	/// <summary>
	/// Quaternions are used to represent rotations.
	/// A custom completely managed implementation of UnityEngine.Quaternion
	/// Base is decompiled UnityEngine.Quaternion
	/// Doesn't implement methods marked Obsolete
	/// Does implicit coversions to and from UnityEngine.Quaternion
	///
	/// Uses code from:
	/// https://raw.githubusercontent.com/mono/opentk/master/Source/OpenTK/Math/Quaternion.cs
	/// http://answers.unity3d.com/questions/467614/what-is-the-source-code-of-quaternionlookrotation.html
	/// http://stackoverflow.com/questions/12088610/conversion-between-euler-quaternion-like-in-unity3d-engine
	/// http://stackoverflow.com/questions/11492299/quaternion-to-euler-angles-algorithm-how-to-convert-to-y-up-and-between-ha
	///
	/// Version: aeroson 2017-07-11 (author yyyy-MM-dd)
	/// License: ODC Public Domain Dedication & License 1.0 (PDDL-1.0) https://tldrlegal.com/license/odc-public-domain-dedication-&-license-1.0-(pddl-1.0)
	/// </summary>
	[Serializable]
	public struct GameQuaternion : IEquatable<GameQuaternion>
	{
		const float radToDeg = (float)(180.0 / System.Math.PI);
		const float degToRad = (float)(System.Math.PI / 180.0);

		public const float kEpsilon = 1E-06f; // should probably be used in the 0 tests in LookRotation or Slerp

		public GameVector3 xyz
		{
			set
			{
				X = value.X;
				Y = value.Y;
				Z = value.Z;
			}
			get
			{
				return new GameVector3(X, Y, Z);
			}
		}
		/// <summary>
		///   <para>X component of the Quaternion. Don't modify this directly unless you know quaternions inside out.</para>
		/// </summary>
		public float X;
		/// <summary>
		///   <para>Y component of the Quaternion. Don't modify this directly unless you know quaternions inside out.</para>
		/// </summary>
		public float Y;
		/// <summary>
		///   <para>Z component of the Quaternion. Don't modify this directly unless you know quaternions inside out.</para>
		/// </summary>
		public float Z;
		/// <summary>
		///   <para>W component of the Quaternion. Don't modify this directly unless you know quaternions inside out.</para>
		/// </summary>
		public float W;

		public float this[int index]
		{
			get
			{
				switch (index)
				{
					case 0:
						return this.X;
					case 1:
						return this.Y;
					case 2:
						return this.Z;
					case 3:
						return this.W;
					default:
						throw new IndexOutOfRangeException("Invalid Quaternion index: " + index + ", can use only 0,1,2,3");
				}
			}
			set
			{
				switch (index)
				{
					case 0:
						this.X = value;
						break;
					case 1:
						this.Y = value;
						break;
					case 2:
						this.Z = value;
						break;
					case 3:
						this.W = value;
						break;
					default:
						throw new IndexOutOfRangeException("Invalid Quaternion index: " + index + ", can use only 0,1,2,3");
				}
			}
		}
		/// <summary>
		///   <para>The identity rotation (RO).</para>
		/// </summary>
		public static GameQuaternion identity
		{
			get
			{
				return new GameQuaternion(0f, 0f, 0f, 1f);
			}
		}
		/// <summary>
		///   <para>Returns the euler angle representation of the rotation.</para>
		/// </summary>
		public GameVector3 eulerAngles
		{
			get
			{
				return GameQuaternion.ToEulerRad(this) * radToDeg;
			}
			set
			{
				this = GameQuaternion.FromEulerRad(value * degToRad);
			}
		}
		/// <summary>
		/// Gets the length (magnitude) of the quaternion.
		/// </summary>
		/// <seealso cref="LengthSquared"/>
		public float Length
		{
			get
			{
				return (float)System.Math.Sqrt(X * X + Y * Y + Z * Z + W * W);
			}
		}

		/// <summary>
		/// Gets the square of the quaternion length (magnitude).
		/// </summary>
		public float LengthSquared
		{
			get
			{
				return X * X + Y * Y + Z * Z + W * W;
			}
		}
		/// <summary>
		///   <para>Constructs new MyQuaternion with given x,y,z,w components.</para>
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <param name="w"></param>
		public GameQuaternion(float x, float y, float z, float w)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}
		/// <summary>
		/// Construct a new MyQuaternion from vector and w components
		/// </summary>
		/// <param name="v">The vector part</param>
		/// <param name="w">The w part</param>
		public GameQuaternion(GameVector3 v, float w)
		{
			this.X = v.X;
			this.Y = v.Y;
			this.Z = v.Z;
			this.W = w;
		}
		/// <summary>
		///   <para>Set x, y, z and w components of an existing MyQuaternion.</para>
		/// </summary>
		/// <param name="new_x"></param>
		/// <param name="new_y"></param>
		/// <param name="new_z"></param>
		/// <param name="new_w"></param>
		public void Set(float new_x, float new_y, float new_z, float new_w)
		{
			this.X = new_x;
			this.Y = new_y;
			this.Z = new_z;
			this.W = new_w;
		}
		/// <summary>
		/// Scales the MyQuaternion to unit length.
		/// </summary>
		public void Normalize()
		{
			float scale = 1.0f / this.Length;
			xyz *= scale;
			W *= scale;
		}
		/// <summary>
		/// Scale the given quaternion to unit length
		/// </summary>
		/// <param name="q">The quaternion to normalize</param>
		/// <returns>The normalized quaternion</returns>
		public static GameQuaternion Normalize(GameQuaternion q)
		{
			GameQuaternion result;
			Normalize(ref q, out result);
			return result;
		}
		/// <summary>
		/// Scale the given quaternion to unit length
		/// </summary>
		/// <param name="q">The quaternion to normalize</param>
		/// <param name="result">The normalized quaternion</param>
		public static void Normalize(ref GameQuaternion q, out GameQuaternion result)
		{
			float scale = 1.0f / q.Length;
			result = new GameQuaternion(q.xyz * scale, q.W * scale);
		}
		/// <summary>
		///   <para>The dot product between two rotations.</para>
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		public static float Dot(GameQuaternion a, GameQuaternion b)
		{
			return a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;
		}
		/// <summary>
		///   <para>Creates a rotation which rotates /angle/ degrees around /axis/.</para>
		/// </summary>
		/// <param name="angle"></param>
		/// <param name="axis"></param>
		public static GameQuaternion AngleAxis(float angle, GameVector3 axis)
		{
			return GameQuaternion.AngleAxis(angle, ref axis);
		}
		private static GameQuaternion AngleAxis(float degress, ref GameVector3 axis)
		{
			if (System.Math.Sqrt(GameVector3.Dot(axis, axis)) == 0)
				return identity;
			//if (axis.sqrMagnitude == 0.0f)
			//	return identity;

			GameQuaternion result = identity;
			var radians = degress * degToRad;
			radians *= 0.5f;
			axis = axis.GetNormalize();
			axis = axis * (float)System.Math.Sin(radians);
			result.X = axis.X;
			result.Y = axis.Y;
			result.Z = axis.Z;
			result.W = (float)System.Math.Cos(radians);

			return Normalize(result);
		}
		public void ToAngleAxis(out float angle, out GameVector3 axis)
		{
			GameQuaternion.ToAxisAngleRad(this, out axis, out angle);
			angle *= radToDeg;
		}
		/// <summary>
		///   <para>Creates a rotation which rotates from /fromDirection/ to /toDirection/.</para>
		/// </summary>
		/// <param name="fromDirection"></param>
		/// <param name="toDirection"></param>
		public static GameQuaternion FromToRotation(GameVector3 fromDirection, GameVector3 toDirection)
		{
			return RotateTowards(LookRotation(fromDirection), LookRotation(toDirection), float.MaxValue);
		}
		/// <summary>
		///   <para>Creates a rotation which rotates from /fromDirection/ to /toDirection/.</para>
		/// </summary>
		/// <param name="fromDirection"></param>
		/// <param name="toDirection"></param>
		public void SetFromToRotation(GameVector3 fromDirection, GameVector3 toDirection)
		{
			this = GameQuaternion.FromToRotation(fromDirection, toDirection);
		}
		/// <summary>
		///   <para>Creates a rotation with the specified /forward/ and /upwards/ directions.</para>
		/// </summary>
		/// <param name="forward">The direction to look in.</param>
		/// <param name="upwards">The vector that defines in which direction up is.</param>
		public static GameQuaternion LookRotation(GameVector3 forward, [DefaultValue("FloatPoint3D.Up")] GameVector3 upwards)
		{
			return GameQuaternion.LookRotation(ref forward, ref upwards);
		}
		public static GameQuaternion LookRotation(GameVector3 forward)
		{
			GameVector3 up = new GameVector3(0,1,0);
			return GameQuaternion.LookRotation(ref forward, ref up);
		}
		// from http://answers.unity3d.com/questions/467614/what-is-the-source-code-of-quaternionlookrotation.html
		private static GameQuaternion LookRotation(ref GameVector3 forward, ref GameVector3 up)
		{

			forward = forward.GetNormalize();
			GameVector3 right = GameVector3.Cross(up, forward).GetNormalize();
			up = GameVector3.Cross(forward, right);
			var m00 = right.X;
			var m01 = right.Y;
			var m02 = right.Z;
			var m10 = up.X;
			var m11 = up.Y;
			var m12 = up.Z;
			var m20 = forward.X;
			var m21 = forward.Y;
			var m22 = forward.Z;


			float num8 = (m00 + m11) + m22;
			var quaternion = new GameQuaternion();
			if (num8 > 0f)
			{
				var num = (float)System.Math.Sqrt(num8 + 1f);
				quaternion.W = num * 0.5f;
				num = 0.5f / num;
				quaternion.X = (m12 - m21) * num;
				quaternion.Y = (m20 - m02) * num;
				quaternion.Z = (m01 - m10) * num;
				return quaternion;
			}
			if ((m00 >= m11) && (m00 >= m22))
			{
				var num7 = (float)System.Math.Sqrt(((1f + m00) - m11) - m22);
				var num4 = 0.5f / num7;
				quaternion.X = 0.5f * num7;
				quaternion.Y = (m01 + m10) * num4;
				quaternion.Z = (m02 + m20) * num4;
				quaternion.W = (m12 - m21) * num4;
				return quaternion;
			}
			if (m11 > m22)
			{
				var num6 = (float)System.Math.Sqrt(((1f + m11) - m00) - m22);
				var num3 = 0.5f / num6;
				quaternion.X = (m10 + m01) * num3;
				quaternion.Y = 0.5f * num6;
				quaternion.Z = (m21 + m12) * num3;
				quaternion.W = (m20 - m02) * num3;
				return quaternion;
			}
			var num5 = (float)System.Math.Sqrt(((1f + m22) - m00) - m11);
			var num2 = 0.5f / num5;
			quaternion.X = (m20 + m02) * num2;
			quaternion.Y = (m21 + m12) * num2;
			quaternion.Z = 0.5f * num5;
			quaternion.W = (m01 - m10) * num2;
			return quaternion;
		}
		public void SetLookRotation(GameVector3 view)
		{
			GameVector3 up = GameVector3.Up;
			this.SetLookRotation(view, up);
		}
		/// <summary>
		///   <para>Creates a rotation with the specified /forward/ and /upwards/ directions.</para>
		/// </summary>
		/// <param name="view">The direction to look in.</param>
		/// <param name="up">The vector that defines in which direction up is.</param>
		public void SetLookRotation(GameVector3 view, [DefaultValue("FloatPoint3D.up")] GameVector3 up)
		{
			this = GameQuaternion.LookRotation(view, up);
		}
		/// <summary>
		///   <para>Spherically interpolates between /a/ and /b/ by t. The parameter /t/ is clamped to the range [0, 1].</para>
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="t"></param>
		public static GameQuaternion Slerp(GameQuaternion a, GameQuaternion b, float t)
		{
			return GameQuaternion.Slerp(ref a, ref b, t);
		}
		private static GameQuaternion Slerp(ref GameQuaternion a, ref GameQuaternion b, float t)
		{
			if (t > 1) t = 1;
			if (t < 0) t = 0;
			return SlerpUnclamped(ref a, ref b, t);
		}
		/// <summary>
		///   <para>Spherically interpolates between /a/ and /b/ by t. The parameter /t/ is not clamped.</para>
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="t"></param>
		public static GameQuaternion SlerpUnclamped(GameQuaternion a, GameQuaternion b, float t)
		{
			return GameQuaternion.SlerpUnclamped(ref a, ref b, t);
		}
		private static GameQuaternion SlerpUnclamped(ref GameQuaternion a, ref GameQuaternion b, float t)
		{
			// if either input is zero, return the other.
			if (a.LengthSquared == 0.0f)
			{
				if (b.LengthSquared == 0.0f)
				{
					return identity;
				}
				return b;
			}
			else if (b.LengthSquared == 0.0f)
			{
				return a;
			}


			float cosHalfAngle = a.W * b.W + GameVector3.Dot(a.xyz, b.xyz);

			if (cosHalfAngle >= 1.0f || cosHalfAngle <= -1.0f)
			{
				// angle = 0.0f, so just return one input.
				return a;
			}
			else if (cosHalfAngle < 0.0f)
			{
				b.xyz = -1 * b.xyz;
				b.W = -b.W;
				cosHalfAngle = -cosHalfAngle;
			}

			float blendA;
			float blendB;
			if (cosHalfAngle < 0.99f)
			{
				// do proper slerp for big angles
				float halfAngle = (float)System.Math.Acos(cosHalfAngle);
				float sinHalfAngle = (float)System.Math.Sin(halfAngle);
				float oneOverSinHalfAngle = 1.0f / sinHalfAngle;
				blendA = (float)System.Math.Sin(halfAngle * (1.0f - t)) * oneOverSinHalfAngle;
				blendB = (float)System.Math.Sin(halfAngle * t) * oneOverSinHalfAngle;
			}
			else
			{
				// do lerp if angle is really small.
				blendA = 1.0f - t;
				blendB = t;
			}

			GameQuaternion result = new GameQuaternion(blendA * a.xyz + blendB * b.xyz, blendA * a.W + blendB * b.W);
			if (result.LengthSquared > 0.0f)
				return Normalize(result);
			else
				return identity;
		}
		/// <summary>
		///   <para>Interpolates between /a/ and /b/ by /t/ and normalizes the result afterwards. The parameter /t/ is clamped to the range [0, 1].</para>
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="t"></param>
		public static GameQuaternion Lerp(GameQuaternion a, GameQuaternion b, float t)
		{
			if (t > 1) t = 1;
			if (t < 0) t = 0;
			return Slerp(ref a, ref b, t); // TODO: use lerp not slerp, "Because quaternion works in 4D. Rotation in 4D are linear" ???
		}
		/// <summary>
		///   <para>Interpolates between /a/ and /b/ by /t/ and normalizes the result afterwards. The parameter /t/ is not clamped.</para>
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="t"></param>
		public static GameQuaternion LerpUnclamped(GameQuaternion a, GameQuaternion b, float t)
		{
			return Slerp(ref a, ref b, t);
		}
		/// <summary>
		///   <para>Rotates a rotation /from/ towards /to/.</para>
		/// </summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <param name="maxDegreesDelta"></param>
		public static GameQuaternion RotateTowards(GameQuaternion from, GameQuaternion to, float maxDegreesDelta)
		{
			float num = GameQuaternion.Angle(from, to);
			if (num == 0f)
			{
				return to;
			}
			float t = System.Math.Min(1f, maxDegreesDelta / num);
			return GameQuaternion.SlerpUnclamped(from, to, t);
		}
		/// <summary>
		///   <para>Returns the Inverse of /rotation/.</para>
		/// </summary>
		/// <param name="rotation"></param>
		public static GameQuaternion Inverse(GameQuaternion rotation)
		{
			float lengthSq = rotation.LengthSquared;
			if (lengthSq != 0.0)
			{
				float i = 1.0f / lengthSq;
				return new GameQuaternion(rotation.xyz * -i, rotation.W * i);
			}
			return rotation;
		}
		/// <summary>
		///   <para>Returns a nicely formatted string of the MyQuaternion.</para>
		/// </summary>
		/// <param name="format"></param>
		public override string ToString()
		{
			return string.Format("({0:F1}, {1:F1}, {2:F1}, {3:F1})", this.X, this.Y, this.Z, this.W);
		}
		/// <summary>
		///   <para>Returns a nicely formatted string of the MyQuaternion.</para>
		/// </summary>
		/// <param name="format"></param>
		public string ToString(string format)
		{
			return string.Format("({0}, {1}, {2}, {3})", this.X.ToString(format), this.Y.ToString(format), this.Z.ToString(format), this.W.ToString(format));
		}
		/// <summary>
		///   <para>Returns the angle in degrees between two rotations /a/ and /b/.</para>
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		public static float Angle(GameQuaternion a, GameQuaternion b)
		{
			float f = GameQuaternion.Dot(a, b);
			return (float)System.Math.Acos(System.Math.Min(System.Math.Abs(f), 1f)) * 2f * radToDeg;
		}
		/// <summary>
		///   <para>Returns a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis (in that order).</para>
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public static GameQuaternion Euler(float x, float y, float z)
		{
			return GameQuaternion.FromEulerRad(new GameVector3((float)x, (float)y, (float)z) * degToRad);
		}
		/// <summary>
		///   <para>Returns a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis (in that order).</para>
		/// </summary>
		/// <param name="euler"></param>
		public static GameQuaternion Euler(GameVector3 euler)
		{
			return GameQuaternion.FromEulerRad(euler * degToRad);
		}
		// from http://stackoverflow.com/questions/12088610/conversion-between-euler-quaternion-like-in-unity3d-engine
		private static GameVector3 ToEulerRad(GameQuaternion rotation)
		{
			float sqw = rotation.W * rotation.W;
			float sqx = rotation.X * rotation.X;
			float sqy = rotation.Y * rotation.Y;
			float sqz = rotation.Z * rotation.Z;
			float unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
			float test = rotation.X * rotation.W - rotation.Y * rotation.Z;
			GameVector3 v;

			if (test > 0.4995f * unit)
			{ // singularity at north pole
				v.Y = 2f * (float)System.Math.Atan2(rotation.Y, rotation.X);
				v.X = (float)System.Math.PI / 2;
				v.Z = 0;
				return NormalizeAngles(v * radToDeg);
			}
			if (test < -0.4995f * unit)
			{ // singularity at south pole
				v.Y = -2f * (float)System.Math.Atan2(rotation.Y, rotation.X);
				v.X = -(float)System.Math.PI / 2;
				v.Z = 0;
				return NormalizeAngles(v * radToDeg);
			}
			GameQuaternion q = new GameQuaternion(rotation.W, rotation.Z, rotation.X, rotation.Y);
			v.Y = (float)System.Math.Atan2(2f * q.X * q.W + 2f * q.Y * q.Z, 1 - 2f * (q.Z * q.Z + q.W * q.W));     // Yaw
			v.X = (float)System.Math.Asin(2f * (q.X * q.Z - q.W * q.Y));                             // Pitch
			v.Z = (float)System.Math.Atan2(2f * q.X * q.Y + 2f * q.Z * q.W, 1 - 2f * (q.Y * q.Y + q.Z * q.Z));      // Roll
			return NormalizeAngles(v * radToDeg);
		}
		private static GameVector3 NormalizeAngles(GameVector3 angles)
		{
			angles.X = NormalizeAngle(angles.X);
			angles.Y = NormalizeAngle(angles.Y);
			angles.Z = NormalizeAngle(angles.Z);
			return angles;
		}
		private static float NormalizeAngle(float angle)
		{
			while (angle > 360)
				angle -= 360;
			while (angle < 0)
				angle += 360;
			return angle;
		}
		// from http://stackoverflow.com/questions/11492299/quaternion-to-euler-angles-algorithm-how-to-convert-to-y-up-and-between-ha
		private static GameQuaternion FromEulerRad(GameVector3 euler)
		{
			var yaw = euler.X;
			var pitch = euler.Y;
			var roll = euler.Z;
			float rollOver2 = roll * 0.5f;
			float sinRollOver2 = (float)System.Math.Sin((float)rollOver2);
			float cosRollOver2 = (float)System.Math.Cos((float)rollOver2);
			float pitchOver2 = pitch * 0.5f;
			float sinPitchOver2 = (float)System.Math.Sin((float)pitchOver2);
			float cosPitchOver2 = (float)System.Math.Cos((float)pitchOver2);
			float yawOver2 = yaw * 0.5f;
			float sinYawOver2 = (float)System.Math.Sin((float)yawOver2);
			float cosYawOver2 = (float)System.Math.Cos((float)yawOver2);
			GameQuaternion result;
			result.X = cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2;
			result.Y = cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2;
			result.Z = cosYawOver2 * sinPitchOver2 * cosRollOver2 + sinYawOver2 * cosPitchOver2 * sinRollOver2;
			result.W = sinYawOver2 * cosPitchOver2 * cosRollOver2 - cosYawOver2 * sinPitchOver2 * sinRollOver2;
			return result;

		}
		private static void ToAxisAngleRad(GameQuaternion q, out GameVector3 axis, out float angle)
		{
			if (System.Math.Abs(q.W) > 1.0f)
				q.Normalize();
			angle = 2.0f * (float)System.Math.Acos(q.W); // angle
			float den = (float)System.Math.Sqrt(1.0 - q.W * q.W);
			if (den > 0.0001f)
			{
				axis = q.xyz / den;
			}
			else
			{
				// This occurs when the angle is zero. 
				// Not a problem: just set an arbitrary normalized axis.
				axis = new GameVector3(1, 0, 0);
			}
		}
		#region Obsolete methods
		/*
		[Obsolete("Use MyQuaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public static MyQuaternion EulerRotation(float x, float y, float z)
		{
			return MyQuaternion.Internal_FromEulerRad(new FloatPoint3D(x, y, z));
		}
		[Obsolete("Use MyQuaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public static MyQuaternion EulerRotation(FloatPoint3D euler)
		{
			return MyQuaternion.Internal_FromEulerRad(euler);
		}
		[Obsolete("Use MyQuaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public void SetEulerRotation(float x, float y, float z)
		{
			this = Quaternion.Internal_FromEulerRad(new FloatPoint3D(x, y, z));
		}
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public void SetEulerRotation(FloatPoint3D euler)
		{
			this = Quaternion.Internal_FromEulerRad(euler);
		}
		[Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees")]
		public FloatPoint3D ToEuler()
		{
			return Quaternion.Internal_ToEulerRad(this);
		}
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public static Quaternion EulerAngles(float x, float y, float z)
		{
			return Quaternion.Internal_FromEulerRad(new FloatPoint3D(x, y, z));
		}
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public static Quaternion EulerAngles(FloatPoint3D euler)
		{
			return Quaternion.Internal_FromEulerRad(euler);
		}
		[Obsolete("Use Quaternion.ToAngleAxis instead. This function was deprecated because it uses radians instead of degrees")]
		public void ToAxisAngle(out FloatPoint3D axis, out float angle)
		{
			Quaternion.Internal_ToAxisAngleRad(this, out axis, out angle);
		}
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public void SetEulerAngles(float x, float y, float z)
		{
			this.SetEulerRotation(new FloatPoint3D(x, y, z));
		}
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public void SetEulerAngles(FloatPoint3D euler)
		{
			this = Quaternion.EulerRotation(euler);
		}
		[Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees")]
		public static FloatPoint3D ToEulerAngles(Quaternion rotation)
		{
			return Quaternion.Internal_ToEulerRad(rotation);
		}
		[Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees")]
		public FloatPoint3D ToEulerAngles()
		{
			return Quaternion.Internal_ToEulerRad(this);
		}
		[Obsolete("Use Quaternion.AngleAxis instead. This function was deprecated because it uses radians instead of degrees")]
		public static Quaternion AxisAngle(FloatPoint3D axis, float angle)
		{
			return Quaternion.INTERNAL_CALL_AxisAngle(ref axis, angle);
		}
		private static Quaternion INTERNAL_CALL_AxisAngle(ref FloatPoint3D axis, float angle)
		{
		}
		[Obsolete("Use Quaternion.AngleAxis instead. This function was deprecated because it uses radians instead of degrees")]
		public void SetAxisAngle(FloatPoint3D axis, float angle)
		{
			this = Quaternion.AxisAngle(axis, angle);
		}
		*/
		#endregion
		public override int GetHashCode()
		{
			return this.X.GetHashCode() ^ this.Y.GetHashCode() << 2 ^ this.Z.GetHashCode() >> 2 ^ this.W.GetHashCode() >> 1;
		}
		public override bool Equals(object other)
		{
			if (!(other is GameQuaternion))
			{
				return false;
			}
			GameQuaternion quaternion = (GameQuaternion)other;
			return this.X.Equals(quaternion.X) && this.Y.Equals(quaternion.Y) && this.Z.Equals(quaternion.Z) && this.W.Equals(quaternion.W);
		}
		public bool Equals(GameQuaternion other)
		{
			return this.X.Equals(other.X) && this.Y.Equals(other.Y) && this.Z.Equals(other.Z) && this.W.Equals(other.W);
		}
		public static GameQuaternion operator *(GameQuaternion lhs, GameQuaternion rhs)
		{
			return new GameQuaternion(lhs.W * rhs.X + lhs.X * rhs.W + lhs.Y * rhs.Z - lhs.Z * rhs.Y, lhs.W * rhs.Y + lhs.Y * rhs.W + lhs.Z * rhs.X - lhs.X * rhs.Z, lhs.W * rhs.Z + lhs.Z * rhs.W + lhs.X * rhs.Y - lhs.Y * rhs.X, lhs.W * rhs.W - lhs.X * rhs.X - lhs.Y * rhs.Y - lhs.Z * rhs.Z);
		}
		public static GameVector3 operator *(GameQuaternion rotation, GameVector3 point)
		{
			float num = rotation.X * 2f;
			float num2 = rotation.Y * 2f;
			float num3 = rotation.Z * 2f;
			float num4 = rotation.X * num;
			float num5 = rotation.Y * num2;
			float num6 = rotation.Z * num3;
			float num7 = rotation.X * num2;
			float num8 = rotation.X * num3;
			float num9 = rotation.Y * num3;
			float num10 = rotation.W * num;
			float num11 = rotation.W * num2;
			float num12 = rotation.W * num3;
			GameVector3 result;
			result.X = (1f - (num5 + num6)) * point.X + (num7 - num12) * point.Y + (num8 + num11) * point.Z;
			result.Y = (num7 + num12) * point.X + (1f - (num4 + num6)) * point.Y + (num9 - num10) * point.Z;
			result.Z = (num8 - num11) * point.X + (num9 + num10) * point.Z + (1f - (num4 + num5)) * point.Z;
			return result;
		}
		public static bool operator ==(GameQuaternion lhs, GameQuaternion rhs)
		{
			return GameQuaternion.Dot(lhs, rhs) > 0.999999f;
		}
		public static bool operator !=(GameQuaternion lhs, GameQuaternion rhs)
		{
			return GameQuaternion.Dot(lhs, rhs) <= 0.999999f;
		}
	}
}
