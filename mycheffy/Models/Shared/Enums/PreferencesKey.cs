using System;
using System.Collections.Generic;
using System.Text;

namespace mycheffy.Models.Shared.Enums
{
    public enum PreferencesKey
    {
        FirstName,
        LastName,
        FullName,
        Email,
        Password,
        PhoneNumber,
        IsVendor,
        ProfilePhoto,
        AccessToken
    }

    public enum SecureStorageKey
    {
        OAuthToken,
        Password
    }
}
