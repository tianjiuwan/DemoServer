﻿namespace TwlPhy
{
    using System;
    using System.Runtime.InteropServices;

    // Representation of 2D vectors and points.
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2 : IEquatable<Vector2>
    {
        // X component of the vector.
        public float x;
        // Y component of the vector.
        public float y;

        // Access the /x/ or /y/ component using [0] or [1] respectively.
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector2 index!");
                }
            }

            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector2 index!");
                }
            }
        }

        // Constructs a new vector with given x, y components.
        public Vector2(float x, float y) { this.x = x; this.y = y; }

        // Set x and y components of an existing Vector2.
        public void Set(float newX, float newY) { x = newX; y = newY; }



        // Linearly interpolates between two vectors without clamping the interpolant
        public static Vector2 LerpUnclamped(Vector2 a, Vector2 b, float t)
        {
            return new Vector2(
                a.x + (b.x - a.x) * t,
                a.y + (b.y - a.y) * t
            );
        }


        // Multiplies two vectors component-wise.
        public static Vector2 Scale(Vector2 a, Vector2 b) { return new Vector2(a.x * b.x, a.y * b.y); }

        // Multiplies every component of this vector by the same component of /scale/.
        public void Scale(Vector2 scale) { x *= scale.x; y *= scale.y; }


        // used to allow Vector2s to be used as keys in hash tables
        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() << 2);
        }

        // also required for being able to use Vector2s as keys in hash tables
        public override bool Equals(object other)
        {
            if (!(other is Vector2)) return false;

            return Equals((Vector2)other);
        }

        public bool Equals(Vector2 other)
        {
            return x.Equals(other.x) && y.Equals(other.y);
        }

        public static Vector2 Reflect(Vector2 inDirection, Vector2 inNormal)
        {
            return -2F * Dot(inNormal, inDirection) * inNormal + inDirection;
        }

        public static Vector2 Perpendicular(Vector2 inDirection)
        {
            return new Vector2(-inDirection.y, inDirection.x);
        }

        // Dot Product of two vectors.
        public static float Dot(Vector2 lhs, Vector2 rhs) { return lhs.x * rhs.x + lhs.y * rhs.y; }

        // Returns the squared length of this vector (RO).
        public float sqrMagnitude { get { return x * x + y * y; } }


        // [Obsolete("Use Vector2.sqrMagnitude")]
        public static float SqrMagnitude(Vector2 a) { return a.x * a.x + a.y * a.y; }
        // [Obsolete("Use Vector2.sqrMagnitude")]
        public float SqrMagnitude() { return x * x + y * y; }



        // Adds two vectors.
        public static Vector2 operator +(Vector2 a, Vector2 b) { return new Vector2(a.x + b.x, a.y + b.y); }
        // Subtracts one vector from another.
        public static Vector2 operator -(Vector2 a, Vector2 b) { return new Vector2(a.x - b.x, a.y - b.y); }
        // Multiplies one vector by another.
        public static Vector2 operator *(Vector2 a, Vector2 b) { return new Vector2(a.x * b.x, a.y * b.y); }
        // Divides one vector over another.
        public static Vector2 operator /(Vector2 a, Vector2 b) { return new Vector2(a.x / b.x, a.y / b.y); }
        // Negates a vector.
        public static Vector2 operator -(Vector2 a) { return new Vector2(-a.x, -a.y); }
        // Multiplies a vector by a number.
        public static Vector2 operator *(Vector2 a, float d) { return new Vector2(a.x * d, a.y * d); }
        // Multiplies a vector by a number.
        public static Vector2 operator *(float d, Vector2 a) { return new Vector2(a.x * d, a.y * d); }
        // Divides a vector by a number.
        public static Vector2 operator /(Vector2 a, float d) { return new Vector2(a.x / d, a.y / d); }
        // Returns true if the vectors are equal.
        public static bool operator ==(Vector2 lhs, Vector2 rhs)
        {
            // Returns false in the presence of NaN values.
            return (lhs - rhs).sqrMagnitude < kEpsilon * kEpsilon;
        }

        // Returns true if vectors are different.
        public static bool operator !=(Vector2 lhs, Vector2 rhs)
        {
            // Returns true in the presence of NaN values.
            return !(lhs == rhs);
        }

        // Converts a [[Vector3]] to a Vector2.
        public static implicit operator Vector2(Vector3 v)
        {
            return new Vector2(v.x, v.y);
        }

        // Converts a Vector2 to a [[Vector3]].
        public static implicit operator Vector3(Vector2 v)
        {
            return new Vector3(v.x, v.y, 0);
        }

        static readonly Vector2 zeroVector = new Vector2(0F, 0F);
        static readonly Vector2 oneVector = new Vector2(1F, 1F);
        static readonly Vector2 upVector = new Vector2(0F, 1F);
        static readonly Vector2 downVector = new Vector2(0F, -1F);
        static readonly Vector2 leftVector = new Vector2(-1F, 0F);
        static readonly Vector2 rightVector = new Vector2(1F, 0F);
        static readonly Vector2 positiveInfinityVector = new Vector2(float.PositiveInfinity, float.PositiveInfinity);
        static readonly Vector2 negativeInfinityVector = new Vector2(float.NegativeInfinity, float.NegativeInfinity);


        // Shorthand for writing @@Vector2(0, 0)@@
        public static Vector2 zero { get { return zeroVector; } }
        // Shorthand for writing @@Vector2(1, 1)@@
        public static Vector2 one { get { return oneVector; } }
        // Shorthand for writing @@Vector2(0, 1)@@
        public static Vector2 up { get { return upVector; } }
        // Shorthand for writing @@Vector2(0, -1)@@
        public static Vector2 down { get { return downVector; } }
        // Shorthand for writing @@Vector2(-1, 0)@@
        public static Vector2 left { get { return leftVector; } }
        // Shorthand for writing @@Vector2(1, 0)@@
        public static Vector2 right { get { return rightVector; } }
        // Shorthand for writing @@Vector2(float.PositiveInfinity, float.PositiveInfinity)@@
        public static Vector2 positiveInfinity { get { return positiveInfinityVector; } }
        // Shorthand for writing @@Vector2(float.NegativeInfinity, float.NegativeInfinity)@@
        public static Vector2 negativeInfinity { get { return negativeInfinityVector; } }

        // *Undocumented*
        public const float kEpsilon = 0.00001F;
        // *Undocumented*
        public const float kEpsilonNormalSqrt = 1e-15f;
    }
}
