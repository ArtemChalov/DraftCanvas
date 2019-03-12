
namespace DraftCanvas
{
    /// <summary>
    /// The constraints enum for LineSegmen.
    /// </summary>
    public enum Constraints {
        /// <summary>
        /// Line hasn't constraint.
        /// </summary>
        Free = 0,
        /// <summary>
        /// Line has height constraint.
        /// </summary>
        Heigth = 1,
        /// <summary>
        /// Line has width constraint.
        /// </summary>
        Width = 2,
        /// <summary>
        /// Line has angle constraint.
        /// </summary>
        Angle = 4,
        /// <summary>
        /// Line has length constraint.
        /// </summary>
        Length = 8
    }
}
