public static class MathExtension
{
#if NETCOREAPP2_0 || NETSTANDARD2_1
#else
    public static int Clamp(int value, int min, int max)
    {
        return (
            value <= min ? min :
            value >= max ? max :
            value
            );
    }
#endif
}
