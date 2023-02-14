using System;


[Serializable]

public class ExceptionCategoryExistence : Exception
{
    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    public string CAT { get; }

    /// <summary>
    /// 
    /// </summary>
    public ExceptionCategoryExistence() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    public ExceptionCategoryExistence(string message)
        : base(message) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="inner"></param>
    public ExceptionCategoryExistence(string message, Exception inner)
        : base(message, inner) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="cat"></param>
    public ExceptionCategoryExistence(string message, string cat)
        : this(message)
    {
        CAT = cat;
    }
}