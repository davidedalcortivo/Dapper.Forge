namespace Forget.Tests.Core
{
    /// <summary>
    /// A small enum shared by every provider's <c>TypeMatrixRow</c>, so the type-matrix tests can prove an enum
    /// property round-trips (and filters, and binds inside an <c>IN</c> list) against a real engine.
    /// </summary>
    public enum TypeMatrixKind
    {
        None = 0,
        Alpha = 1,
        Beta = 2
    }
}
