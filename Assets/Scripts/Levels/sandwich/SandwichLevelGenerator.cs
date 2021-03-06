using System;
using System.Collections.Generic;
using System.Linq;
using Ingredients;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Levels.sandwich
{
    public class SandwichLevelGenerator : MonoBehaviour
    {
        private int rows = Sandwich.GridSizeX;
        private int columns = Sandwich.GridSizeY;
        private List<Vector2> _availablePositions;
        
        
        //List of available Vector2 positions. Instantiate first bread on a random(0, columns*rows),
        //then get the Vector2 position, and get the surrounding positions and add it to a list.
        //Everytime you pick a new ingredient and place it, add more available positions on the list.
        //When number of pieces are over, AssignSurroundingsToContext().
        public SandwichLevel GenerateLevel(SandwichLevel data)
        {
            var totalNodes = rows * columns;
            _availablePositions = new List<Vector2>();
            Random.InitState(data.seed.GetHashCode());
            
            data.nodes?.Clear();
            data.nodes = new List<Ingredient>();
            
            var firstBreadIndex = Random.Range(0, totalNodes);
            var firstPosition = new Vector2(firstBreadIndex / columns, firstBreadIndex % rows);
            var firstBread = new Ingredient()
            {
                content = IngredientType.Bread,
                position = firstPosition
            };
            
            data.nodes.Add(firstBread);
            GetAvailablePositionsOnGrid(data.nodes);
            
            for (int i = 0; i < data.piecesAmount - 1; i++)
            {
                var index = Random.Range(0, _availablePositions.Count);
                var position = _availablePositions[index];
                var content = i == 0 ? IngredientType.Bread : GetRandomNodeContent(data.ingredients, i);
                
                if(data.nodes.Where(n => n.content == content).ToList().Count > 4)
                {
                    i--;
                    continue;
                }

                var node = new Ingredient()
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

        private void GetAvailablePositionsOnGrid(List<Ingredient> placedNodes)
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

        private IngredientType GetRandomNodeContent(List<IngredientType> contents, int index)
        {
            return contents[index % contents.Count];
        }
        
        public SandwichLevel CreateNewLevel(int actualLevel)
        {
            var random = Random.Range(3, 17);
            
            var level = ScriptableObject.CreateInstance<SandwichLevel>();
            level.autoGenerated = true;
            level.piecesAmount = random;
            level.ingredients = GetRandomContents(random);
            level.seed = $"level-sandwich{actualLevel}";

            return level;
        }
        
        private List<IngredientType> GetRandomContents(int numberOfContent)
        {
            var contents = new List<IngredientType>();
            for (int i = 0; i < numberOfContent; i++)
            {
                var index = Random.Range(0, 10);
                IngredientType content = (IngredientType) index;
                if (content == IngredientType.Empty || content == IngredientType.Bread)
                {
                    i--;
                }
                else
                {
                    contents.Add(content);
                }
            }

            return contents;
        }
    }
}