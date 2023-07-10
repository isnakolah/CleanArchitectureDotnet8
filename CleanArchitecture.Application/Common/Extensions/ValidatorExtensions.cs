namespace CleanArchitecture.Application.Extensions;

public static class ValidatorExtensions
{
    public static IRuleBuilderOptions<T, TProperty> IsRequired<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
    {
        return ruleBuilder.NotEmpty().WithMessage("{PropertyName} is required");
    }
    
    public static IRuleBuilderOptions<T, TProperty> MustExistAsync<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, Func<TProperty, Task<bool>> predicate)
    {
        return ruleBuilder.MustAsync(async (propertyValue, _) => await predicate(propertyValue));
    }
}