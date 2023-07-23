using System.Linq.Expressions;

namespace PersonalFinance.Helpers; 
public static class ObjectHelper {
    public static string GetFullName<T, TValue>(Expression<Func<T, TValue>> expression)
    {
        if (expression.Body is MemberExpression memberExpression) {
            string memberName = GetMemberName(memberExpression);
            return memberName;
        }

        throw new ArgumentException("Invalid expression format.");
    }

    // Recursive helper method to construct the full name
    private static string GetMemberName(MemberExpression memberExpression)
    {
        if (memberExpression.Expression is MemberExpression parentExpression) {
            string parentName = GetMemberName(parentExpression);
            return $"{parentName}.{memberExpression.Member.Name}";
        }

        return memberExpression.Member.Name;
    }
}
