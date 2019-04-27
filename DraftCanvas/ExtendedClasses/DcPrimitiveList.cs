using DraftCanvas.Primitives;
using System.Collections.Generic;

namespace DraftCanvas.ExtendedClasses
{
    /// <summary>
    /// 
    /// </summary>
    public class DcPrimitiveList : List<IPrimitive>
    {
        DrCanvas _canvas;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        public DcPrimitiveList(DrCanvas owner)
        {
            _canvas = owner;
        }
        /// 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dcLineSegment"></param>
        public new void Add(IPrimitive dcLineSegment)
        {
            base.Add(dcLineSegment);
            _canvas.AddToVisualCollection(dcLineSegment);
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="dcLineSegment"></param>
        /// <returns></returns>
        public new bool Remove(IPrimitive dcLineSegment)
        {
            return true;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void Update()
        {
            _canvas.Update();
        }
    }
}
