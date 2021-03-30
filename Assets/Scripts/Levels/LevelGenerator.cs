using System.Collections.Generic;
using System.Linq;
using Nodes;
using UnityEngine;

namespace Levels
{
    public class LevelGenerator : MonoBehaviour
    {
        private int rows = GameManager.GRID_SIZE_X;
        private int columns = GameManager.GRID_SIZE_Y;
        private List<Vector2> _availablePositions;
        
        
        //List of available Vector2 positions. Instantiate first bread on a random(0, columns*rows),
        //then get the Vector2 position, and get the surrounding positions and add it to a list.
        //Everytime you pick a new ingredient and place it, add more available positions on the list.
        //When number of pieces are over, AssignSurroundingsToContext().
        public Level GenerateLevel(Level data)
        {
            var totalNodes = rows * columns;
            _availablePositions = new List<Vector2>();
            Random.InitState(data.seed.GetHashCode());
            
            data.nodes?.Clear();
            data.nodes = new List<Node>();
            
            var firstBreadIndex = Random.Range(0, totalNodes);
            var firstPosition = new Vector2(firstBreadIndex / columns, firstBreadIndex % rows);
            var firstBread = new Node()
            {
                content = NodeContent.Bread,
                position = firstPosition
            };
            
            data.nodes.Add(firstBread);
            GetAvailablePositionsOnGrid(data.nodes);
            
            for (int i = 0; i < data.piecesAmount - 1; i++)
            {
                var index = Random.Range(0, _availablePositions.Count);
                var position = _availablePositions[index];
                var content = i == 0 ? NodeContent.Bread : GetRandomNodeContent(data.levelContent, i);

                var node = new Node()
                {
                    content = content,
                    position = position
                };

                _availablePositions.Remove(position);
                data.nodes.Add(node);
                GetAvailablePositionsOnGrid(data.nodes);
            }

            _availablePositions.Clear();
            return data;
        }

        private void GetAvailablePositionsOnGrid(List<Node> placedNodes)
        {
            var unavailablePositions = _availablePositions.Union(placedNodes.Select(n => n.position).ToList());
            foreach (var node in placedNodes)
            {
                var position = node.position;
                var top = new Vector2(position.x, position.y + 1);
                var right = new Vector2(position.x + 1, position.y);
                var bottom = new Vector2(position.x, position.y - 1);
                var left = new Vector2(position.x - 1, position.y);

                if(CheckBoundaries(top) && !unavailablePositions.Contains(top)) _availablePositions.Add(top);
                if(CheckBoundaries(right) && !unavailablePositions.Contains(right)) _availablePositions.Add(right);
                if(CheckBoundaries(bottom) && !unavailablePositions.Contains(bottom)) _availablePositions.Add(bottom);
                if(CheckBoundaries(left) && !unavailablePositions.Contains(left)) _availablePositions.Add(left);
            }
        }

        private bool CheckBoundaries(Vector2 position)
        {
            if (position.x < 0 || position.x >= columns)
                return false;

            if (position.y < 0 || position.y >= rows)
                return false;

            return true;
        }

        private NodeContent GetRandomNodeContent(List<NodeContent> contents, int index)
        {
            return contents[index % contents.Count];
        }
    }
}