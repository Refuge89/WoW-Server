namespace Framework.Contants
{
    public enum AuthServerOpCode : byte
    {
        AUTH_LOGON_CHALLENGE        = 0x00,
        AUTH_LOGON_PROOF            = 0x01,
        AUTH_RECONNECT_CHALLENGE    = 0x02,
        AUTH_RECONNECT_PROOF        = 0x03,
        AUTH_AUTHENTIFICATOR        = 0x04,
        REALM_LIST                  = 0x10,
        XFER_INITIATE               = 0x30,
        XFER_DATA                   = 0x31,
        XFER_ACCEPT                 = 0x32,
        XFER_RESUME                 = 0x33,
        XFER_CANCEL                 = 0x34,
        UNKNOW                      = byte.MaxValue,
    }

    public enum AuthenticationResult : byte
    {
        Success                     = 0x00, // Successfully logged in.
        Failure                     = 0x01, // Unable to connect
        AccountBanned               = 0x03, // This <game> account has been closed and is no longer available for use.
        UnknownAccount              = 0x04, // The information you have entered is not valid
        UnknownPassword             = 0x05, // The information you have entered is not valid
        AlreadyOnline               = 0x06, // This account is already logged into <game>.
        NoTimeLeft                  = 0x07, // You have used up your prepaid time for this account
        ServerFull                  = 0x08, // Could not log in to <game> at this time.
        WrongBuild                  = 0x09, // Unable to validate game version.
        UpdateClientVersion         = 0x0A, // Downloading
        Suspended                   = 0x0C, // This <game> account has been temporarily suspended.
        ParentalControl             = 0x0F, // Access to this account has been blocked by parental controls.
    }
}
