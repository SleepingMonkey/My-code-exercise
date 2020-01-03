using PyramidChallengeSolution.Models.Enums;
using System.Collections.Generic;

namespace PyramidChallengeSolution.Models
{
    public class Node
    {
        public long Value { get; set; }
        public List<Node> Children { get; set; } = new List<Node>();

        public ValueType ValueNumbericType
        {
            get 
            { 
                return Value % 2 == 0 ? ValueType.Even : ValueType.Odd; 
            }
        }
    }
}
