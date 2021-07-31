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
    private static DateTimeOffset _dto;

    public OrigamiFileLogHandler(string fileName)
    {
        string filePath = Application.persistentDataPath + "/" + fileName;

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
        _dto = DateTimeOffset.Now;
        m_StreamWriter.WriteLine("UGIW-AR | Gaze has changed | " + _dto.LocalDateTime.ToString("dd/MM/yyyy hh:mm:ss.fff tt"));
        m_StreamWriter.Flush();
    }

    public void ClearSpace()
    {
        _dto = DateTimeOffset.Now;
        m_StreamWriter.WriteLine("UGIW-AR | Clear Space | " + _dto.LocalDateTime.ToString("dd/MM/yyyy hh:mm:ss.fff tt"));
        m_StreamWriter.Flush();
    }

    public void SelectionSuccessfull()
    {
        _dto = DateTimeOffset.Now;
        m_StreamWriter.WriteLine("UGIW-AR | Selection successfull " + _dto.LocalDateTime.ToString("dd/MM/yyyy hh:mm:ss.fff tt"));
        m_StreamWriter.Flush();
    }


    public void SelectionNotSuccessfull()
    {
        _dto = DateTimeOffset.Now;
        m_StreamWriter.WriteLine("UGIW-AR | Selection NOT successfull " + _dto.LocalDateTime.ToString("dd/MM/yyyy hh:mm:ss.fff tt"));
        m_StreamWriter.Flush();
    }

    public void SpeechResetWorld()
    {
        _dto = DateTimeOffset.Now;
        m_StreamWriter.WriteLine("UGIW-AR | Speech Reset World " + _dto.LocalDateTime.ToString("dd/MM/yyyy hh:mm:ss.fff tt"));
        m_StreamWriter.Flush();
    }


    public void SpeechDropSphere()
    {
        _dto = DateTimeOffset.Now;
        m_StreamWriter.WriteLine("UGIW-AR | Speech Drop Sphere " + _dto.LocalDateTime.ToString("dd/MM/yyyy hh:mm:ss.fff tt"));
        m_StreamWriter.Flush();
    }

    public void SpeechAttempt()
    {
        _dto = DateTimeOffset.Now;
        m_StreamWriter.WriteLine("UGIW-AR | Speech Attempt " + _dto.LocalDateTime.ToString("dd/MM/yyyy hh:mm:ss.fff tt"));
        m_StreamWriter.Flush();
    }
}


