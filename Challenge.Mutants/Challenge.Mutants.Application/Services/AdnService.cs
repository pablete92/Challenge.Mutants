using Challenge.Mutants.Application.Models;
using Challenge.Mutants.Application.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Challenge.Mutants.Application.Services
{
    public interface IAdnService
    {
        AdnModel IsMutant(SaveADNModel model);

    }

    public class AdnService : IAdnService
    {
        public AdnModel IsMutant(SaveADNModel model)
        {
            var dna = model.Dna;

            int widht = dna[0].Length;
            int height = dna.Length;

            char[][] matriz = dna.Select(q => q.ToArray()).ToArray();

            var position = new Position(widht, height, 0, 0, Direction.Center);

            int contCoincidences = 0;

            do
            {
                var positions = GetNextPositions(position);

                if (AnalizePositions(matriz, positions))
                {
                    contCoincidences++;
                }

                position.PositionX++;

                if (position.PositionX == widht)
                {
                    position.PositionX = 0;
                    position.PositionY++;
                }
            } while (position.PositionY < height && contCoincidences < 2);

            return new AdnModel
            {
                Adn = String.Join(",", model.Dna),
                Mutant = contCoincidences > 1 ? true : false
            };
        }

        private Position[] GetNextPositions(Position position)
        {
            ICollection<Position> positions = new List<Position> { position };

            bool top = position.PositionY < 3;
            bool bot = position.PositionY > position.Height - 4;
            bool left = position.PositionX < 3;
            bool right = position.PositionX > position.Width - 4;

            if (!right)
            {
                positions.Add(position.Clone(position.PositionX + 1, position.PositionY, Direction.Horizontal));
            }

            if (!bot)
            {
                positions.Add(position.Clone(position.PositionX, position.PositionY + 1, Direction.Vertical));
                if (!right)
                {
                    positions.Add(position.Clone(position.PositionX + 1, position.PositionY + 1, Direction.DiagonalRight));
                }
                if (!left)
                {
                    positions.Add(position.Clone(position.PositionX - 1, position.PositionY + 1, Direction.DiagonalLeft));
                }
            }

            return positions.ToArray();
        }

        private bool AnalizePositions(char[][] matriz, Position[] positions)
        {
            var aux = matriz[positions[0].PositionY][positions[0].PositionX];

            for (int i = 0; i < positions.Length; i++)
            {
                var auxB = matriz[positions[i].PositionY][positions[i].PositionX];

                if(aux.Equals(auxB) && CheckDirection(matriz, positions[i]))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckDirection(char[][] matriz, Position position)
        {
            switch (position.Direction)
            {
                case Direction.Horizontal:
                    var horizontal = matriz[position.PositionY][position.PositionX] == matriz[position.PositionY][position.PositionX + 1] && matriz[position.PositionY][position.PositionX] == matriz[position.PositionY][position.PositionX + 2];

                    return horizontal;

                case Direction.DiagonalRight:
                    var diagonalRight = matriz[position.PositionY][position.PositionX] == matriz[position.PositionY + 1][position.PositionX + 1] && matriz[position.PositionY][position.PositionX] == matriz[position.PositionY + 2][position.PositionX + 2];

                    return diagonalRight;

                case Direction.Vertical:
                    var diagonalVertical = matriz[position.PositionY][position.PositionX] == matriz[position.PositionY + 1][position.PositionX] && matriz[position.PositionY][position.PositionX] == matriz[position.PositionY + 2][position.PositionX];

                    return diagonalVertical;

                case Direction.DiagonalLeft:
                    var diagonalLeft = matriz[position.PositionY][position.PositionX] == matriz[position.PositionY + 1][position.PositionX - 1] && matriz[position.PositionY][position.PositionX] == matriz[position.PositionY + 2][position.PositionX - 2];

                    return diagonalLeft;

                case Direction.Center:
                default:
                    return false;
            }
        }
    }
}
