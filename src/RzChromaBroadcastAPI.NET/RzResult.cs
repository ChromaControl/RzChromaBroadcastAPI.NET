namespace Razer.Chroma.Broadcast
{
    public enum RzResult
    {
        INVALID = -1,
        SUCCESS = 0,
        ACCESS_DENIED = 5,
        INVALID_HANDLE = 6,
        NOT_SUPPORTED = 50,
        INVALID_PARAMETER = 87,
        SERVICE_NOT_ACTIVE = 1062,
        SINGLE_INSTANCE_APP = 1152,
        DEVICE_NOT_CONNECTED = 1167,
        NOT_FOUND = 1168,
        REQUEST_ABORTED = 1235,
        ALREADY_INITIALIZED = 1247,
        RESOURCE_DISABLED = 4309,
        DEVICE_NOT_AVAILABLE = 4319,
        NOT_VALID_STATE = 5023,
        NO_MORE_ITEMS = 259,
        FAILED = 2147483647
    }
}
