using System.ComponentModel;

namespace GlobalShared.Constants.Permission;
public static class Permissions
{
    [DisplayName("Person")]
    [Description("Person Permissions")]
    public static class Person
    {
        public const string View = "Permissions.Person.View";
        public const string Search = "Permissions.Person.Search";
    }
}