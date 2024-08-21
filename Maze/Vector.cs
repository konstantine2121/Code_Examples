﻿namespace Maze_Example
{
    public struct Vector
    {
        #region Ctors

        public Vector(int x, int y) : this()
        {
            X = x;
            Y = y;
        }

        #endregion Ctors

        #region Properties

        public int X { get; set; }

        public int Y { get; set; }

        #endregion Properties

        #region Directions

        public static Vector Up => new Vector(0, 1);

        public static Vector Down => new Vector(0, -1);
        
        public static Vector Left => new Vector(-1, 0);
        
        public static Vector Right => new Vector(1, 0);

        #endregion Directions

        #region Operators

        public static Vector operator + (Vector point1, Vector point2)
        {
            return new Vector(
                point1.X + point2.X,
                point1.Y + point2.Y);
        }

        public static Vector operator - (Vector point1, Vector point2)
        {
            return new Vector(
                point1.X - point2.X,
                point1.Y - point2.Y);
        }

        public static bool operator == (Vector point1, Vector point2)
        {
            return (point1.X == point2.X) && 
                   (point1.Y == point2.Y);
        }

        public static bool operator !=(Vector point1, Vector point2)
        {
            return !(point1 == point2);
        }

        #endregion Operators

        #region Equals

        public override bool Equals(object obj)
        {
            return obj is Vector point && point == this;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        #endregion Equals
    }
}
