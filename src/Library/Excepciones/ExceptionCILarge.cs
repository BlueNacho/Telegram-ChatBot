using System;


[Serializable]

/// <summary>
/// Excepción creada para evitar que ingresen un mal formato de cédula.
/// </summary>

public class ExceptionCILarge : Exception
{
    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    public int CI { get; }

    /// <summary>
    /// 
    /// </summary>
    public ExceptionCILarge() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    public ExceptionCILarge(string message)
        : base(message) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="inner"></param>
    public ExceptionCILarge(string message, Exception inner)
        : base(message, inner) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="cI"></param>
    public ExceptionCILarge(string message, int cI)
        : this(message)
    {
        CI = cI;
    }
}