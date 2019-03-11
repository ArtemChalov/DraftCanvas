﻿using DraftCanvas.Primitives;
using System.Collections.Generic;

namespace DraftCanvas.ExtendedClasses
{
    /// <summary>
    /// 
    /// </summary>
    public class DcLineSegmentList : List<DcLineSegment>
    {
        Canvas _canvas;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        public DcLineSegmentList(Canvas owner)
        {
            _canvas = owner;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dcLineSegment"></param>
        public new void Add(DcLineSegment dcLineSegment)
        {
            base.Add(dcLineSegment);
            _canvas.AddToVisualCollection(dcLineSegment);
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