using System;

namespace PtoVta.Infraestructura.Transversales.Log
{
    public interface ILog
    {
        //registro Mensaje de depuración 
        //mensaje: El mensaje de depuración
        //argumentos: los valores de los argumentos de mensajes
        void Debug(string mensaje, params object[] argumentos);


        //registro Mensaje de depuración 
        //mensaje : el mensaje
        //ecepcion: Excepción a escribir en el mensaje de depuración
        void Debug(string mensaje, Exception excepcion, params object[] argumentos);


        //registro Mensaje de depuración 
        //Item: El artículo con información para escribir en depuración
        void Debug(object item);

        //registro de error FATAL
        //mensaje: el mensaje de error   fatal
        //argumentos: Los valores de los argumentos del mensaje
        void Fatal(string mensaje, params object[] argumentos);

        //registro de error FATAL
        //mensaje: el mensaje de error   fatal
        //ecepcion: La excepción a escribir en este mensaje fatal
        void Fatal(string mensaje, Exception excepcion, params object[] argumentos);

        //registra la informacion del mensaje
        //mensaje:      El mensaje de información para escribir
        //argumentos:   Los valores de los argumentos
        void LogInfo(string mensaje, params object[] argumentos);

        //Mensaje de advertencia sesión
        //mensaje:      El mensaje de advertencia para escribir
        //argumentos:   Los valores de los argumentos
        void LogWarning(string mensaje, params object[] argumentos);

        //registro de Mensaje de error 
        //mensaje:      El mensaje de error para escribir
        //argumentos:   Los valores de los argumentos
        void LogError(string mensaje, params object[] argumentos);

        //Mensaje de error de registro
        //mensaje:      El mensaje de error para escribir
        //excepcion:    La excepción asociada con este error
        //argumentos:   Los valores de los argumentos
        void LogError(string mensaje, Exception excepcion, params object[] argumentos);        
    }
}
