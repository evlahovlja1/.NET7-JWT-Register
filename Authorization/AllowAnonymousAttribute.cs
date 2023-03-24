namespace WebApi.Authorization;


//Custom added to override Identity's attribute with same name
[AttributeUsage(AttributeTargets.Method)]
public class CustomAllowAnonymousAttribute : Attribute
{ }