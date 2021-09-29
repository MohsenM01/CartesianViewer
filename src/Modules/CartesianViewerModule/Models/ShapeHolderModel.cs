using System;
using System.Collections.Generic;
using Common.Models.Shapes.Bases;

namespace CartesianViewerModule.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ShapeHolderModel
    {
        /// <summary>
        /// 
        /// </summary>
        public ShapeHolderModel()
        {
            BiningEvents = new List<Tuple<string, string>>();
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CartesianShapeModel CartesianShape { get; set; }

        /// <summary>
        ///
        /// <returns>returns a Tuple of propertyPath and eventName </returns>
        /// </summary>
        public List<Tuple<string, string>> BiningEvents { get; set; }
    }
}
