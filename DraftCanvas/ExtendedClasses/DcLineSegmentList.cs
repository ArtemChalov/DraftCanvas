using DraftCanvas.Primitives;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftCanvas.ExtendedClasses
{
    public class DcLineSegmentList : List<DcLineSegment>
    {
        Canvas _canvas;

        public DcLineSegmentList(Canvas owner)
        {
            _canvas = owner;
        }

        public new void Add(DcLineSegment dcLineSegment)
        {
            base.Add(dcLineSegment);
            _canvas.AddToVisualCollection(dcLineSegment);
        }

        public void Update()
        {
            _canvas.Update();
        }
    }
}
