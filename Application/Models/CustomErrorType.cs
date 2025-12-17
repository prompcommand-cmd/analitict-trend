using System.ComponentModel;

namespace Application.Models
{
    public enum CustomErrorType
    {
        [Description("UNKNOWN")] Unknown,
        [Description("VALIDATION")] Validation,
        [Description("BAD_REQUEST")] BadRequest,
        [Description("NOT_FOUND")] NotFound,
        [Description("UNAUTHORIZE_ACCESS")] UnauthorizedAccess,
        [Description("UNAUTHENTICATED")] Unauthenticated,
        [Description("DOMAIN_EXCEPTION")] DomainException,
    }
}
