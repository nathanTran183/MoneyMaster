namespace MoneyMaster.Common
{
    public static class Constants
    {
        #region Error Message
        //public static readonly string  = ".{8,}";

        #endregion

        #region Roles
        public static readonly string AdminRole = "Admin";
        public static readonly string UserRole = "User";
        public static readonly string ManagerRole = "Manager";
        #endregion

        #region RolePolicy
        public static readonly string RequireAdminRole = "RequireAdminRole";
        public static readonly string RequireManagerRole = "RequireManagerRole";
        public static readonly string RequireUserRole = "RequireUserRole";
        #endregion
    }
}
