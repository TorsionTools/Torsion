using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Torsion.Utils
{
    static class BaseModelUtilities
    {
        internal static T GetPropertyValue<T>(this Expression<Func<T>> lambda)
        {
            return lambda.Compile().Invoke();
        }
        internal static void SetPropertyValue<T>(this Expression<Func<T>> lambda, T value)
        {
            MemberExpression expression = (lambda as LambdaExpression).Body as MemberExpression;
            PropertyInfo propertyInfo = (PropertyInfo)expression.Member;
            object target = Expression.Lambda(expression.Expression).Compile().DynamicInvoke();
            propertyInfo.SetValue(target, value);
        }
    }
}
