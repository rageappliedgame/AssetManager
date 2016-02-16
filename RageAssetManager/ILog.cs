// <copyright file="ILogger.cs" company="RAGE"> Copyright (c) 2015 RAGE. All rights reserved.
// </copyright>
// <author>Veg</author>
// <date>13-4-2015</date>
// <summary>defines the logger interface</summary>
namespace AssetPackage
{
    using System;

    /// <summary>
    /// Interface for logger.
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Executes the log operation.
        /// 
        /// Implement this in Game Engine Code.
        /// </summary>
        ///
        /// <param name="severity"> The severity. </param>
        /// <param name="msg">      The message. </param>
        void Log(Severity severity, String msg);
    }

    /// <summary>
    /// Values that represent log severity.
    /// <br/>
    ///      See
    /// <a href="https://msdn.microsoft.com/en-us/library/office/ff604025(v=office.14).aspx">Trace
    /// and Event Log Severity Levels</a>
    /// </summary>
    public enum Severity : int
    {
        /// <summary>
        /// An enum constant representing the critical option.
        /// </summary>
        Critical = 1,

        /// <summary>
        /// An enum constant representing the error option.
        /// </summary>
        Error = 2,

        /// <summary>
        /// An enum constant representing the warning option.
        /// </summary>
        Warning = 4,

        /// <summary>
        /// An enum constant representing the information option.
        /// </summary>
        Information = 8,

        /// <summary>
        /// An enum constant representing the verbose option.
        /// </summary>
        Verbose = 16
    }

    /// <summary>
    /// Values that represent log levels.
    /// </summary>
    public enum LogLevel : int
    {
        /// <summary>
        /// An enum constant representing the critical option.
        /// </summary>
        Critical = Severity.Critical,
        /// <summary>
        /// An enum constant representing the error option.
        /// </summary>
        Error = Critical | Severity.Error,
        /// <summary>
        /// An enum constant representing the warning option.
        /// </summary>
        Warn = Error | Severity.Warning,
        /// <summary>
        /// An enum constant representing the information option.
        /// </summary>
        Info = Warn | Severity.Information,
        /// <summary>
        /// An enum constant representing all option.
        /// </summary>
        All = Severity.Critical | Severity.Error | Severity.Warning | Severity.Information | Severity.Verbose,
    }
}
