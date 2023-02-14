namespace Proyecto
{
    /// <summary>
    /// Clase Singleton. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> where T : new()
    {
        private static T instance;

        /// <summary>
        /// "Constructor" de clase singleton
        /// </summary>
        /// <value></value>
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                }

                return instance;
            }

            
        }

        /// <summary>
        /// Resetea la intancia singleton (se utiliza en los test)
        /// </summary>
        public static void resetForTesting()
        {
            instance = default(T);
        }
    }
}