using System.ComponentModel;

namespace Data
{
    public enum EnumUserParams
    {
        [Description("@id")]
        Id,

        [Description("@email")]
        Email,

        [Description("@first_name")]
        FirstName,

        [Description("@last_name")]
        LastName,

        [Description("@avatar")]
        Avatar,

        [Description("@Username")]
        Username,

        [Description("@Password")]
        Password,
    }
}