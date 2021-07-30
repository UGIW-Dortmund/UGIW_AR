using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class OrigamiFileLogHandler : ILogHandler
{
    private FileStream m_FileStream;
    private StreamWriter m_StreamWriter;
    private ILogHandler m_DefaultLogHandler = Debug.unityLogger.logHandler;
  //  private CurrentMillis cm =  CurrentMillis();

    public OrigamiFileLogHandler()
    {
        string filePath = Application.persistentDataPath + "/MyLogs2.txt";

        m_FileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        m_StreamWriter = new StreamWriter(m_FileStream);

        // Replace the default debug log handler
        Debug.unityLogger.logHandler = this;
    }

    public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
    {
        m_StreamWriter.WriteLine(String.Format(format, args));
        m_StreamWriter.Flush();
        m_DefaultLogHandler.LogFormat(logType, context, format, args);
    }

    public void LogException(Exception exception, UnityEngine.Object context)
    {
        m_DefaultLogHandler.LogException(exception, context);
    }

    public void LogGaze()
    {
        m_StreamWriter.WriteLine("Gaze has changed " + CurrentMillis.Millis);
        m_StreamWriter.Flush();
    }

    public void SelectionSuccessfull()
    {
        m_StreamWriter.WriteLine("Selection successfull " + Time.time);
        m_StreamWriter.Flush();
    }


    public void SelectionNotSuccessfull()
    {
        m_StreamWriter.WriteLine("Selection NOT successfull " + Time.time);
        m_StreamWriter.Flush();
    }
}


/// <summary>Class to get current timestamp with enough precision</summary>
static class CurrentMillis
{
    private static readonly DateTime Jan1St1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    /// <summary>Get extra long current timestamp</summary>
    public static long Millis { get { return (long)((DateTime.UtcNow - Jan1St1970).TotalMilliseconds); } }
}