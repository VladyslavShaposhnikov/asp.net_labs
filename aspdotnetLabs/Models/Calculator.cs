namespace aspdotnetLabs.Models;

public class Calculator
{
    public Operation Operator { get; set; }
    public double number1 { get; set; }
    public double number2 { get; set; }

    public string Op
    {
        get
        {
            switch (Operator)
            {
                case Operation.Add:
                    return "+";
                case Operation.Sub:
                    return "-";
                case Operation.Mul:
                    return "*";
                case Operation.Div:
                    return "/";
                default:
                    return "?";
            }
        }
    }

    public bool isValid()
    {
        return number1 != null && number2 != null && Operator != null;
    }
    
    public double Calculate() {
        switch (Operator)
        {
            case Operation.Add:
                return (double) (number1 + number2);
             case Operation.Sub:
                 return (double) (number1 - number2);
             case Operation.Mul:
                 return (double) (number1 * number2);
             case Operation.Div:
                if (number1 == 0)
                {
                    return double.NaN;
                }
                return (double) (number1 / number2);
            default: return double.NaN;
        }
    }
}

public enum Operation
{
    Add,    
    Sub,
    Mul,
    Div
}