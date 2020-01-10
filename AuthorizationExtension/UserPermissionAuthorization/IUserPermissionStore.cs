namespace Basic.AuthorizationExtension.UserPermissionAuthorization
{
    public interface IUserPermissionStore
    {
        string [] GetUserPermission(object key);
    }
}
