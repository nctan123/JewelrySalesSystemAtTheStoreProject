namespace JSSATSProject.Repository.ConstantsContainer;

public static class DiamondFactors
{
    public static readonly Dictionary<string, double> ShapeFactors = new Dictionary<string, double>
    {
        { "Round", 1.1 },
        { "Princess", 1.0 },
        { "Emerald", 0.93 },
        { "Asscher", 1.03 },
        { "Marquise", 0.95 },
        { "Oval", 0.9 },
        { "Radiant", 1.05 },
        { "Pear", 0.98 },
        { "Heart", 1.1 },
        { "Cushion", 1.02 }
    };

    public static readonly Dictionary<string, double> FluorescenceFactors = new Dictionary<string, double>
    {
        { "None", 1.05 },
        { "Faint", 1.0 },
        { "Medium", 0.95 },
        { "Strong", 0.9 }
    };

    public static readonly Dictionary<string, double> SymmetryFactors = new Dictionary<string, double>
    {
        { "Excellent", 1.1 },
        { "Very Good", 1.05 },
        { "Good", 1.0 },
        { "Fair", 0.95 },
        { "Poor", 0.9 }
    };

    public static readonly Dictionary<string, double> PolishFactors = new Dictionary<string, double>
    {
        { "Excellent", 1.1 },
        { "Very Good", 1.05 },
        { "Good", 1.0 },
        { "Fair", 0.95 },
        { "Poor", 0.9 }
    };
}