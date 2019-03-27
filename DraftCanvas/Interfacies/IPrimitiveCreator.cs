using System.Windows;

namespace DraftCanvas.Interfacies
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPrimitiveCreator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentPoint"></param>
        /// <param name="canvas"></param>
        /// <returns></returns>
        IPrimitiveCreator Create(Point currentPoint, DrCanvas canvas);
    }
}
