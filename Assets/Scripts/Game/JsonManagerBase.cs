using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonManagerBase
{
    /// <summary>  
    /// 对象转为json  
    /// </summary>  
    /// <typeparam name="ObjType"></typeparam>  
    /// <param name="obj"></param>  
    /// <returns></returns>  
    public static string ObjToJsonString<ObjType>(ObjType obj) where ObjType : class
    {
        string s = JsonConvert.SerializeObject(obj);
        return s;
    }

    /// <summary>  
    /// json转为对象  
    /// </summary>  
    /// <typeparam name="ObjType"></typeparam>  
    /// <param name="JsonString"></param>  
    /// <returns></returns>  
    public static ObjType JsonStringToObj<ObjType>(string JsonString) where ObjType : class
    {
        ObjType s = JsonConvert.DeserializeObject<ObjType>(JsonString);
        return s;
    }
}
