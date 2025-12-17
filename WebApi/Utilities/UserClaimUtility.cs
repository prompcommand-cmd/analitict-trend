using Application.Constant;
using Infrastructure.Abstracts;
using System.Security.Claims;

namespace WebApi.Utilities
{
    public static class UserClaimUtility
    {
        public static string GetUserName(ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(x => x.Type == "__USERNAME__")?.Value ?? string.Empty;
        }
        
        public static string GetUserId(ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(x => x.Type == "__USER_ID__")?.Value ?? string.Empty;
        }

        public static string GetEmail(ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(x => x.Type == "__EMAIL__")?.Value ?? string.Empty;
        }
        
        public static int GetTypeValue(ClaimsPrincipal user, string type)
        {
            int.TryParse(user.Claims.FirstOrDefault(x => x.Type == type)?.Value, out int result);
            return result;
        }

        public static bool IsAuthorized(int userAccessValue, string grantedAccess)
        {
            if ((userAccessValue & AccessType.READ) == AccessType.READ && grantedAccess == Auth.READ)
            {
                return true;
            }
            if ((userAccessValue & AccessType.CREATE) == AccessType.CREATE && grantedAccess == Auth.CREATE)
            {
                return true;
            }
            if ((userAccessValue & AccessType.EDIT) == AccessType.EDIT && grantedAccess == Auth.EDIT)
            {
                return true;
            }
            if ((userAccessValue & AccessType.DELETE) == AccessType.DELETE && grantedAccess == Auth.DELETE)
            {
                return true;
            }
            return false;
        }

        public static string GetClientId(ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(x => x.Type == "client_id")?.Value ?? string.Empty;
        }
        public static (string userId, string userName, string email) UserLoginInfo(HttpContext context, ICryptography _cryptography)
        {
            var userId = _cryptography.AesDecrypt(GetUserId(context.User));
            var userName = _cryptography.AesDecrypt(GetUserName(context.User));
            userName = !string.IsNullOrEmpty(userName) ? userName : GetClientId(context.User);
            userId = !string.IsNullOrEmpty(userId) ? userId : userName;
            var email = _cryptography.AesDecrypt(GetEmail(context.User));
            return (userId, userName, email);
        }
    }
}
