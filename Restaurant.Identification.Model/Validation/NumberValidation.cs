namespace Restaurant.Identification.Model;

public static class NumberValidation
{

    public static Validator GreaterThanZero(this Validator validator, int value)
    {
        return validator.Test(value > 0,
            $"should be greater than zero.");
    }

    public static Validator GreaterThanOrEqualToZero(this Validator validator, int value)
    {
        return validator.Test(value >= 0,
            $"should be greater than or equal to zero.");
    }


}
