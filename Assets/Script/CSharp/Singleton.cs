/*************************************************************
 * Author    :   Bocchi
 * Mail      :   JenksZhang@gmail.com
 * Date      :   2024-06-03
 * Brief     :   Singleton Template
 ************************************************************/
 
public class Singleton<T> where T : new()
{
    private static T _instance = default(T);
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
            }
            return _instance;
        }
    }
}