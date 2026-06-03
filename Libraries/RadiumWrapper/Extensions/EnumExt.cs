namespace RadTools.Libraries.RadiumWrapper.Extensions;

public static class EnumExt
{
    public static string GetName(this PlatformManager.LDIIFDDCKCN en)
    {
        return new[] { "Steam", "Oculus", "PS4", "Microsoft", "Screen", "Quest", "iOS", "Xbox" }[(int)en];
    }
}