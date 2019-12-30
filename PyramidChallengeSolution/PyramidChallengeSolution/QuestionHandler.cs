using PyramidChallengeSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PyramidChallengeSolution
{
    public class QuestionHandler
    {
        private Pyramid _pyramid;
        private List<List<Node>> _allValidPathes;
        private List<Node> _tempPath;

        public QuestionHandler(Pyramid pyramid)
        {
            _pyramid = pyramid;
            _tempPath = new List<Node>();
            _allValidPathes = new List<List<Node>>();
        }

        public void BuildDirectedGraph()
        {
            if (_pyramid.Levels == null)
                throw new Exception("Pyramid object cannot be null.");

            if (_pyramid.Levels.Count < 2)
                throw new Exception("Pyramid levels cannot be less than 2.");

            for (int i = 0; i < _pyramid.Levels.Count; i++)
            {
                //if not the last level
                if(i != _pyramid.Levels.Count - 1)
                {
                    var currentLevel = _pyramid.Levels[i];
                    var nextLevel = _pyramid.Levels[i + 1];

                    if (nextLevel.Nodes.Count < currentLevel.Nodes.Count)
                        throw new Exception("Error: next level nodes length is smaller than current level nodes length");
                    
                    //build node relations
                    for(int j = 0; j < currentLevel.Nodes.Count; j++)
                    {
                        var node = currentLevel.Nodes[j];
                        node.Children = new List<Node>() { nextLevel.Nodes[j], nextLevel.Nodes[j+1] };
                    }
                }
            }
        }

        public List<Node> GetMaxValuePath()
        {
            if (_pyramid.Levels == null) 
                throw new Exception("Pyramid object cannot be null.");

            if (!_pyramid.Levels.Any()) 
                throw new Exception("Pyramid size cannot be 0.");

            if (_pyramid.Levels[0].Nodes == null)
                throw new Exception("Pyramid level1's nodes cannot be null.");

            if (!_pyramid.Levels[0].Nodes.Any())
                throw new Exception("Pyramid level1's nodes size cannot be 0.");

            var startNode = _pyramid.Levels[0].Nodes[0];

            TraverseAllValidPathesFromNode(startNode, new List<Node>());
            
            return _allValidPathes.OrderByDescending(path => path.Select(x => x.Value).ToList().Sum()).First();
        }

        private void TraverseAllValidPathesFromNode(Node node, List<Node> path)
        {
            path.Add(node);

            //reach the end of path
            if (!node.Children.Any())
            {
                _allValidPathes.Add(path);
                return;
            }

            if(node.Children.Any())
            {
                //if all children node are the same numberic type (Even/Odd) with parent
                if (!node.Children.Where(x => x.ValueNumbericType != node.ValueNumbericType).Any())
                {
                    _allValidPathes.Add(path);
                    return;
                }

                foreach (var childNode in node.Children)
                {
                    if(node.ValueNumbericType != childNode.ValueNumbericType)
                    {
                        //continue if NumbericType is differ and make a new a path
                        TraverseAllValidPathesFromNode(childNode, new List<Node>(path));
                    }
                }
            }
       }
    }
}
