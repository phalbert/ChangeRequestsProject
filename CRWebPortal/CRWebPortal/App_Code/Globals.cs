namespace CRWebPortal
{
    public static class Globals
    {
        public const string SUCCESS_STATUS_CODE = "0";
        public const string FAILURE_STATUS_CODE = "100";
        public const string SUCCESS_STATUS_TEXT = "SUCCESS";

        public const string SMS_MASK = "FLEXIPAY";
        public const string MAIL_FROM = "CHANGE REQUEST SYSTEM";
        public const string MAIL_SUBJECT = "CHANGE REQUEST SYSTEM CREDENTIALS";
        public const string SMS_SENDER = "CHANGE REQUEST SYSTEM";

        public static string PARSE_ERROR_CODE = "500";
        public static string DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm";

        public static string SQL_TO_GET_INTELLISENSE_DATA = "SELECT Name FROM sys.all_objects where type_desc in ('USER_TABLE', 'SQL_STORED_PROCEDURE', 'VIEW') and is_ms_shipped = 0" +
                                                         "union " +
                                                         "SELECT c.name AS 'ColumnName' FROM sys.columns c JOIN sys.tables t   ON c.object_id = t.object_id";

        public const int MAXIMUM_ROWS_FOR_NON_FULL_CONTROL_QUERY = 10;
    }
}