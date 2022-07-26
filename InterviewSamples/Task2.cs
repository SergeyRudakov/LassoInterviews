using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewSamples
{
    /// <summary>
    /// Tree object, contais value and childen nodes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Tree<T>
    {

        public Tree() { }

        public Tree(T value, T[] children)
        {
            this.Value = value;
            var childrenNodes = new List<Tree<T>>();
            for (int i = 0; i < children.Length; i++)
                childrenNodes.Add(new Tree<T>() { Value = children[i] });
            this.Children = childrenNodes.AsEnumerable();

        }
        public T Value { get; set; }
        public IEnumerable<Tree<T>> Children { get; set; }


        /// <summary>
        /// Create test sample 
        /// </summary>
        /// <returns></returns>
        public static Tree<int> Init()
        {
            var root = new Tree<int>();
            root.Value = 5;
            //???
            root.Children = new List<Tree<int>>() { };
            
            root.Children = new List<Tree<int>>()
            {
                new Tree<int>(5, new int[] {7, 8, 9}),
                new Tree<int>(0, new int[] {3, 5, -9}),
                new Tree<int>(25, new int[] {6, 78, 3})
            };

            return root;
        }
    }

}
