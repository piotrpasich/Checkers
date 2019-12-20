using Checkers.Game.Board;
using Checkers.Game.Board.Filters;
using Checkers.Game.Configuration;
using Checkers.Game.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Game.Moves {
    public class ShowPossibleMoves : ICheckerMove {
        readonly private Field[,] BoardFields;
        readonly private PlayerManager PlayerManager;
        readonly private BoardFilter BoardFilter;
        readonly private int BoardSize;
        readonly private GameConfiguration GameConfiguration;

        public ShowPossibleMoves(
            Field[,] boardFields,
            PlayerManager playerManager,
            GameConfiguration gameConfiguration) {
            GameConfiguration = gameConfiguration;
            BoardFields = boardFields;
            BoardSize = boardFields.GetLength(0);
            PlayerManager = playerManager;
            BoardFilter = new BoardFilter(BoardFields);
        }

        public bool Perform(Field field) {
            if (!field.HasChecker() || !field.IsSelected()) {
                return true;
            }

            List<Point> possibleLocations  = GetPossibleMoves(field);
            foreach (Point possibleLocation in possibleLocations) {
                (BoardFields[possibleLocation.X, possibleLocation.Y]).MarkAsPossibleMove();
            }

            return true;
        }
        
        private List<Point> GetNearestEnemiest(Field field) {
            List<Point> possibleLocations = BoardFilter.GetAllPossibleLocations(field);
            possibleLocations = BoardFilter.LimitToBoard(possibleLocations);
            possibleLocations = BoardFilter.LimitToMaxJumps(possibleLocations, field);
            return BoardFilter.LimitToOnlyEnemies(possibleLocations, field);
        }

        private List<Point> GetPossibleMoves(Field field) {
            List<Point> possibleLocations = BoardFilter.GetAllPossibleLocations(field);
            possibleLocations = BoardFilter.LimitToNearestEnemies(possibleLocations);
            possibleLocations = BoardFilter.LimitToBoard(possibleLocations);
            
            if (!field.PlacedChecker.IsQueen || !GameConfiguration.CanQueenMoveOverMoreFields()) {
                possibleLocations = BoardFilter.LimitToMaxJumps(possibleLocations, field);
            }
            if (!field.PlacedChecker.IsQueen) {
                possibleLocations = BoardFilter.LimitToDirection(possibleLocations, field);
            }
            
            List<Point> enemies = BoardFilter.LimitToOnlyEnemies(GetNearestEnemiest(field), field);
            
            foreach (Point enemyPoint in enemies) {
                Field enemy = BoardFields[enemyPoint.X, enemyPoint.Y];
                possibleLocations.AddRange(GetPossibleMovesForEnemy(enemy, field));
            }

            possibleLocations = BoardFilter.LimitToBoard(possibleLocations);

            return possibleLocations;
        }

        private List<Point> GetPossibleMovesForEnemy(Field enemy, Field currentlySelectedField) {
            List<Point> possibleEnemyLocations = BoardFilter.GetAllPossibleLocations(enemy);
            possibleEnemyLocations = BoardFilter.LimitToMaxJumps(possibleEnemyLocations, enemy);
            if (!currentlySelectedField.PlacedChecker.IsQueen) {
                possibleEnemyLocations = BoardFilter.LimitToDirection(possibleEnemyLocations, currentlySelectedField);
            }
            possibleEnemyLocations = BoardFilter.LimitToNearestEnemies(possibleEnemyLocations);
            possibleEnemyLocations = BoardFilter.LimitToHorizontalDirection(possibleEnemyLocations, currentlySelectedField, enemy);

            return possibleEnemyLocations;
        }

    }
}
