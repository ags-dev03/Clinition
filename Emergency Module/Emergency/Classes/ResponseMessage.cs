using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Emergency.Classes
{
    public static class ResponseMessage
    {
        public static string Insert = "Record has been inserted successfully.";
        public static string Update = "Record has been updated successfully.";
        public static string Delete = "Record has been deleted successfully.";
        public static string PasswordLength = "Password should contain =  <br> 1. At least 8 characters. <br>2. At least one lowercase character. <br>3. At least one uppercase character. <br>4.  At least one numeric value(0-9). <br>5. At least one special character(eg. @$!%*?&). <br>6. No spaces in between.";
        public static string NoMenuAccess = "You have no access to login software from this computer.";
        public static string NoCompAccess = "You have no access to login software from this computer.";
        public static string backDate = "Your System\'s Date & time is not matching with the server\'s date & time settings. Please make proper settings before login.";
        public static string userBackDate = "Your System\"s Date & time is not matching with the server\'s date & time settings. Please make proper settings before login.";
        public static string DemoExpired = "Your Software is current in Temporary Registration. Kindly contact our customer relations Dept On 9782511333 and Licence your software.";
        public static string RgExpired = "Your software is not yet registered.<br/>Kindly contact ASMI GLOBAL SOFTWARES PVT LTD Technical Assistance on  = -<br/> 89528-54321<br/>97825-11333";
        public static string Regnotregister = "Your software is not yet registered.<br/>Kindly contact ASMI GLOBAL SOFTWARES PVT LTD Technical Assistance on  = -<br/> 89528-54321<br/>97825-11333";
        public static string staticAccess = "You have no access for static IP to login software.";
        public static string biometricThumb = "You are not login by biometric thumb impression first login in biometric thumb then login software.";
        public static string passwordExpired = "Your password has been expired. Please change your password then login again.";
        public static string loginName = "The User Code or Company Name is incorrect. Please make correct entry.";
        public static string dllMismatch = "Your software and DB version are not match. Please contect to your IT department and update application";
        public static string UsrCode = "Please enter user code.";
        public static string UsrPass = "Please enter password.";
        public static string UsrPassCreate = "Minimum 8 characters are required in password.";
        public static string PassMatch = "Please enter correct password";
        public static string Already = "Record already exist.";
        public static string Name = "Please enter name.";
        public static string Code = "Please enter code.";
        public static string LinkedRec = "You can not delete linked record.";
        public static string Error = "Some error occured.";
    }
}