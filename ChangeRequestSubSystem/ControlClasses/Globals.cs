using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class Globals
{
    public const string SUCCESS_STATUS_CODE= "0";
    public const string FAILURE_STATUS_CODE = "100";
    public const string SUCCESS_STATUS_TEXT = "SUCCESS";
    public const string SMS_MASK = "FLEXIPAY";
    public const string MAIL_FROM = "CHANGE REQUEST SYSTEM";
    public const string FROM_EMAIL = "noreply@pegasus.co.ug";
    public const string MAIL_SUBJECT = "CHANGE REQUEST SYSTEM CREDENTIALS";
    public const string SMS_SENDER = "CHANGE REQUEST SYSTEM";
    public const string MAIL_SUBJECT_APPROVE_CR_EMAIL = "APPROVE CHANGE REQUEST";
    public const string MAIL_SUBJECT_APPROVE_TBAR_EMAIL = "APPROVE TBAR REQUEST";
    public const string SMPTP_SERVER = "smtp.gmail.com";
    public const string SMTP_USERNAME = "nkasozi@gmail.com";
    public const string SMTP_PASSWORD = "uluspchlzgzjqqaq";//"Tp4tci2s4i2g!";

    public static List<string> AcceptableMethodsOfSendingOTP = new List<string>() { "EMAIL", "PHONE" };

    public const string FILE_PATH_TO_APPROVE_CR_EMAIL_TEMPLATE = "FILE_PATH_TO_APPROVE_EMAIL_TEMPLATE";
    public const string FILE_PATH_TO_APPROVE_TBAR_EMAIL_TEMPLATE = "FILE_PATH_TO_APPROVE_TBAR_EMAIL_TEMPLATE";
    public const string APPROVE_CR_URL = "APPROVE_CR_URL";
    public const string APPROVE_TBAR_URL = "APPROVE_TBAR_URL";
    public const string DATE_FORMAT = "yyyy-MM-dd HH:mm";

    public static object DOMAIN = "test.pegasus.co.ug:8019/CrWebPortal";
}

