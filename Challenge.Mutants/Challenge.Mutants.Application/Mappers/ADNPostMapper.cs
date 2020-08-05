using Challenge.Mutants.Application.Models;
using Challenge.Mutants.Application.Models.Request;
using Challenge.Mutants.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Challenge.Mutants.Application.Mappers
{
    public class ADNPostMapper : MapperBase<AdnModel, SaveADNModel>
    {
        public AdnModel MapEntityToModel(SaveADNModel q)
        {
            var dna = q.Dna;

            int w = dna[0].Length;
            int h = dna.Length;

            char[][] m = dna.Select(x => x.ToArray()).ToArray();

            var p = new Position(w, h, 0, 0, Direction.C);

            int c = 0;

            do
            {
                var ps = GetNextPositions(p);
                if (AnalyzePositions(m, ps))
                {
                    c++;
                }

                p.X++;
                if (p.X == w)
                {
                    p.X = 0;
                    p.Y++;
                }

            } while (p.Y < h && c < 2);

            return new AdnModel
            {
                Adn = String.Join(",",q.Dna),
                Mutant = c > 1 ? true : false
            };
        }
        public Position[] GetNextPositions(Position p)
        {
            ICollection<Position> ps = new List<Position> { p };

            bool top = p.Y < 3;
            bool bot = p.Y > p.H - 4;
            bool left = p.X < 3;
            bool right = p.X > p.W - 4;

            if (!right)
            {
                ps.Add(p.Clone(p.X + 1, p.Y, Direction.H));
            }

            if (!bot)
            {
                ps.Add(p.Clone(p.X, p.Y + 1, Direction.V));
                if (!right)
                {
                    ps.Add(p.Clone(p.X + 1, p.Y + 1, Direction.DR));
                }
                if (!left)
                {
                    ps.Add(p.Clone(p.X - 1, p.Y + 1, Direction.DL));
                }
            }

            return ps.ToArray();
        }

        public bool AnalyzePositions(char[][] m, Position[] ps)
        {
            var a = m[ps[0].Y][ps[0].X];
            for (int i = 1; i < ps.Length; i++)
            {
                var b = m[ps[i].Y][ps[i].X];
                if (a.Equals(b) && CheckDirection(m, ps[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckDirection(char[][] m, Position p)
        {
            switch (p.D)
            {
                case Direction.H:
                    var h = m[p.Y][p.X] == m[p.Y][p.X + 1] && m[p.Y][p.X] == m[p.Y][p.X + 2];
                    return h;
                case Direction.DR:
                    var dr = m[p.Y][p.X] == m[p.Y + 1][p.X + 1] && m[p.Y][p.X] == m[p.Y + 2][p.X + 2];
                    return dr;
                case Direction.V:
                    var v = m[p.Y][p.X] == m[p.Y + 1][p.X] && m[p.Y][p.X] == m[p.Y + 2][p.X];
                    return v;
                case Direction.DL:
                    var dl = m[p.Y][p.X] == m[p.Y + 1][p.X - 1] && m[p.Y][p.X] == m[p.Y + 2][p.X - 2];
                    return dl;
                case Direction.C:
                default:
                    return false;
            }
        }
    }

    public class Position
    {
        public int W { get; }
        public int H { get; }
        public int X { get; set; }
        public int Y { get; set; }
        public Direction D { get; }

        public Position(int w, int h, int x, int y, Direction d)
        {
            W = w;
            H = h;
            X = x;
            Y = y;
            D = d;
        }

        public Position Clone(int x, int y, Direction d)
        {
            return new Position(W, H, x, y, d); ;
        }
    }

    public enum Direction
    {
        C,
        H,
        DR,
        V,
        DL,
    }
}
