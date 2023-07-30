using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureSample.Utilities.Constants
{
    public class Errors
    {
        public static ErrorInfo UserNameRequired = new ErrorInfo("USERNAME_REQUIRED", "Username is required.");
        public static ErrorInfo UserNameInvalid = new ErrorInfo("USERNAME_INVALID", "Username is invalid.");
        public static ErrorInfo PasswordRequired = new ErrorInfo("PASSWORD_REQUIRED", "Password is required.");
        public static ErrorInfo PasswordInvalid = new ErrorInfo("PASSWORD_INVALID", "Password is invalid.");
        public static ErrorInfo PasswordIncorrect = new ErrorInfo("PASSWORD_INCORRECRT", "Password is incorrect.");
        public static ErrorInfo UserNameOrPasswordIncorrect = new ErrorInfo("USERNAME_OR_PASSWORD_INCORRECRT", "Username or password is incorrect.");
        public static ErrorInfo RecordNotFound = new ErrorInfo("RECORD_NOT_FOUND", "Record not found.");
        public static ErrorInfo RecordExistsAlready = new ErrorInfo("RECORD_EXISTS_ALREADY", "Record has already exist.");
        public static ErrorInfo UserNameExistAlready = new ErrorInfo("UserNameExistAlready", "Username has already exist.");

    }
}
