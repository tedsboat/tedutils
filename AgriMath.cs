using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Agricosmic.Utilities
{
    public enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    /// <summary>
    /// A Vector2D with ints
    /// Update: LITERALLY THE SAME THING AS VECTOR2INT GOD DAMN IT
    /// </summary>
    public struct Point2D
    {
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static Point2D Zero = new(0, 0);
        public static Point2D One = new(1, 1);
        public static Point2D Up = new(0, 1);
        public static Point2D Down = new(0, -1);
        public static Point2D Left = new(-1, 0);
        public static Point2D Right = new(1, 0);
        public static Point2D[] Deltas = { Up, Down, Left, Right };

        public int X;
        public int Y;

        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Point2D operator +(Point2D a, Point2D b) => new(a.X + b.X, a.Y + b.Y);
        public static Point2D operator -(Point2D a, Point2D b) => new(a.X - b.X, a.Y - b.Y);
        public static Vector2 operator *(Point2D a, float b) => new(a.X * b, a.Y * b);
        public static Point2D operator *(Point2D a, int b) => new(a.X * b, a.Y * b);
        public static Point2D operator /(Point2D a, float b) => new((int)(a.X / b), (int)(a.Y / b));
        public static Point2D operator /(Point2D a, int b) => new(a.X / b, a.Y / b);
        public static bool operator ==(Point2D a, Point2D b) => a.Equals(b);
        public static bool operator ==(Point2D a, object b) => a.Equals(b);

        public static bool operator !=(Point2D a, object b) => !(a == b);

        public static bool operator !=(Point2D a, Point2D b) => !(a == b);

        public override bool Equals(object obj)
        {
            if (obj is not Point2D other) return false;
            return Equals(other);
        }

        public bool Equals(Point2D other)
        {
            return X == other.X && Y == other.Y;
        }

        public override string ToString()
        {
            return $"{{{X}, {Y}}}";
        }

        public Vector2 ToVector() => new(X, Y);

        public float DistanceTo(Point2D other)
        {
            return Mathf.Sqrt(
                Mathf.Pow(other.X - X, 2f) + Mathf.Pow(other.Y - Y, 2f)
            );
        }
    }

    public static class AgriMath
    {
        // https://www.alanzucconi.com/2015/09/16/how-to-sample-from-a-gaussian-distribution/
        /// <summary>
        /// Get a standard deviation random value
        /// </summary>
        /// <returns>value from (-1, 1)</returns>
        public static float NextGaussian()
        {
            float v1, v2, s;
            do
            {
                v1 = 2.0f * Random.Range(0f, 1f) - 1.0f;
                v2 = 2.0f * Random.Range(0f, 1f) - 1.0f;
                s = v1 * v1 + v2 * v2;
            } while (s >= 1.0f || s == 0f);

            s = Mathf.Sqrt((-2.0f * Mathf.Log(s)) / s);

            return v1 * s;
        }

        /// <summary>
        /// Returns a random number close to one of the edges between the two edge bounds.
        /// Higher weight makes the center even less likely
        /// </summary>
        /// <param name="leftEdge">min</param>
        /// <param name="rightEdge">max</param>
        /// <param name="weight">weight</param>
        /// <returns>random [min, max]</returns>
        public static float RandomEdgeDistribution(float leftEdge, float rightEdge, float weight = 2f)
        {
            return Mathf.Lerp(leftEdge, rightEdge, Mathf.Pow(Random.value, 1f / weight));
        }

        public static T Choose<T>(List<T> list) => list[Random.Range(0, list.Count)];
        public static T Choose<T>(T[] list) => list[Random.Range(0, list.Length)];
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
        {
            return list.OrderBy(_ => Random.value);
        }

        public static float RandomFromRangeVector(this Vector2 vector) => Random.Range(vector.x, vector.y);
        
        // NOTE: Inclusive on the top end to make it easier for designers!!
        public static int RandomIntFromRangeVector(this Vector2 vector) => Random.Range(Mathf.FloorToInt(vector.x), Mathf.FloorToInt(vector.y) + 1);

        /// <summary>
        /// Rotates a "2D" 3D vector around the Z axis (most useful in 3d)
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public static Vector3 RotateVector3(Vector3 vector, float degrees)
        {
            return Quaternion.AngleAxis(degrees, Vector3.forward) * vector;
        }

        public static float ClampJustBelow1(float value)
        {
            const float EPSILON = 0.0001f;
            return Mathf.Clamp(value, 0, 1 - EPSILON);
        }

        /// <summary>
        /// Get a value from a list using a normalized float index
        /// </summary>
        /// <param name="options">the options to index into</param>
        /// <param name="index">the normalized float to index with. clamped to 0-1</param>
        /// <typeparam name="T">type of the options</typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException">thrown if the list is empty</exception>
        public static T FloatIndexNormalized<T>(IReadOnlyList<T> options, float index)
        {
            if (options.Count == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(options));

            return options[(int)(ClampJustBelow1(index) * options.Count)];
        }

        /// <summary>
        /// Sums multiple layers of perlin noise to create more complex structures
        /// </summary>
        /// <param name="x">the x coord to sample at</param>
        /// <param name="y">the y coord to sample at</param>
        /// <param name="amplitude">the initial multiplier for the noise</param>
        /// <param name="frequency">the initial frequency to sample at</param>
        /// <param name="octaves">the number of layers to sum</param>
        /// <param name="persistence">the amount to multiply the amplitude by each octave</param>
        /// <param name="lacunarity">the amount to multiply the frequency by each octave</param>
        /// <returns>the result</returns>
        public static float FancyPerlinNoise2D(float x, float y, float amplitude, float frequency, int octaves,
            float persistence, float lacunarity)
        {
            float result = 0;

            for (int octave = 1; octave < octaves; octave++)
            {
                result += Mathf.PerlinNoise(x * frequency, y * frequency) * amplitude;

                amplitude *= persistence;
                frequency *= lacunarity;
            }

            return result;
        }
    }
}