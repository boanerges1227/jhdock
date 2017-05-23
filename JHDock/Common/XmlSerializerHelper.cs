using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Data;

namespace JHDock
{
    public class XmlSerializerHelper
    {
        //陈宝栋 2014-01-01 创建
        /// <summary>
        /// 从字符串反序列化
        /// </summary>
        /// <typeparam name="T">要反序列化出的类型</typeparam>
        /// <param name="SerializerStr">要序列化的串</param>
        /// <param name="rootElementName">根节点元素名称</param>
        /// <returns>返回序列化出的对象</returns>
        public static T Deserializer<T>(string serializerStr, string rootElementName)
        {
            XmlRootAttribute xmlRootAttribute = new XmlRootAttribute(rootElementName);
            //xmlRootAttribute.Namespace = "1.0.1";

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), xmlRootAttribute);
            StringReader sr = new StringReader(serializerStr);

            XmlReader xmlReader = XmlReader.Create(sr);
            return (T)xmlSerializer.Deserialize(xmlReader);
        }

        /// <summary>
        /// 从字符串反序列化
        /// </summary>
        /// <typeparam name="T">要反序列化出的类型</typeparam>
        /// <param name="SerializerStr">要序列化的串</param>        
        /// <returns>返回序列化出的对象</returns>
        public static T Deserializer<T>(string serializerStr)
        {
            return Deserializer<T>(serializerStr, "");
        }

        /// <summary>
        /// 从文件反序列化
        /// </summary>
        /// <typeparam name="T">要反序列化出的类型</typeparam>
        /// <param name="file">要序列化的文件</param>
        /// <param name="rootElementName">根节点元素名称</param>
        /// <returns>返回序列化出的对象</returns>
        public static T DeserializerFromFile<T>(string file, string rootElementName)
        {
            //
            //XmlReader xmlTextReader = XmlReader.Create(file);
            XmlTextReader xmlTextReader = new XmlTextReader(file);
            //Console.WriteLine(xmlTextReader.Encoding);
            //XmlReader xmlReader = XmlReader.Create(file);
            T result = (T)Deserializer<T>(xmlTextReader, rootElementName);
            xmlTextReader.Close();
            return result;
        }

        /// <summary>
        /// 从文件反序列化
        /// </summary>
        /// <typeparam name="T">要反序列化出的类型</typeparam>
        /// <param name="file">要序列化的文件</param>
        /// <param name="rootElementName">根节点元素名称</param>
        /// <returns>返回序列化出的对象</returns>
        public static T DeserializerFromFile<T>(string file)
        {
            return (T)DeserializerFromFile<T>(file, "");
        }

        /// <summary>
        /// 从XmlReader反序列化
        /// </summary>
        /// <typeparam name="T">要反序列化出的类型</typeparam>
        /// <param name="SerializerStr">要反序列化的XmlReader对象</param>
        /// <param name="rootElementName">根节点元素名称</param>
        /// <returns>返回序列化出的对象</returns>
        public static T Deserializer<T>(XmlReader xmlReader, string rootElementName)
        {
            XmlRootAttribute xmlRootAttribute = new XmlRootAttribute(rootElementName);
            ////xmlRootAttribute.Namespace = "1.0.1";

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), xmlRootAttribute);
            return (T)xmlSerializer.Deserialize(xmlReader);
        }

        /// <summary>
        /// 从XmlReader反序列化
        /// </summary>
        /// <typeparam name="T">要反序列化出的类型</typeparam>
        /// <param name="SerializerStr">要反序列化的XmlReader对象</param>
        /// <param name="rootElementName">根节点元素名称</param>
        /// <returns>返回序列化出的对象</returns>
        public static T Deserializer<T>(XmlReader xmlReader)
        {
            return Deserializer<T>(xmlReader, "");
        }

        /// <summary>
        /// 序列化到字符串
        /// </summary>
        /// <typeparam name="T">要序列化的类型</typeparam>
        /// <param name="serializerObj">要序列化的对象</param>
        /// <param name="rootElementName">根节点的名称</param>
        /// <returns>序列化的串</returns>
        public static string Serializer<T>(T serializerObj, string rootElementName)
        {
            //根节点属性
            XmlRootAttribute xmlRootAttribute = new XmlRootAttribute(rootElementName);
            //xmlRootAttribute.Namespace = "1.0.1";

            // 命名空间 
            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
            xmlSerializerNamespaces.Add("", "");

            //建立序列化对象并序列化
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), xmlRootAttribute);

            //内存流方式
            MemoryStream memoryStream = new MemoryStream();
            XmlTextWriter xmlSink = new XmlTextWriter(memoryStream, Encoding.UTF8);
            xmlSerializer.Serialize(xmlSink, serializerObj, xmlSerializerNamespaces);
            //内在流reader转码方式
            memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
            System.IO.StreamReader reader = new System.IO.StreamReader(memoryStream, Encoding.UTF8);
            return reader.ReadToEnd();         
        }

        /// <summary>
        /// 序列化到字符串
        /// </summary>
        /// <typeparam name="T">要序列化的类型</typeparam>
        /// <param name="serializerObj">要序列化的对象</param>        
        /// <returns>序列化的串</returns>
        public static string Serializer<T>(T serializerObj)
        {
            return Serializer<T>(serializerObj, "");
        }

        /// <summary>
        /// 序列化到XmlWriter
        /// </summary>
        /// <typeparam name="T">要序列化的类型</typeparam>
        /// <param name="serializerObj">要序列化的对象</param>
        /// <param name="rootElementName">根元素的名称</param>
        /// <param name="xmlWriter">要写入序列化内容的对象 注意Encoding</param>
        public static void Serializer<T>(T serializerObj, string rootElementName, XmlWriter xmlWriter)
        {
            //根节点属性
            XmlRootAttribute xmlRootAttribute = new XmlRootAttribute(rootElementName);
            xmlRootAttribute.Namespace = "";

            // 命名空间 
            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
            xmlSerializerNamespaces.Add("", "");

            //建立序列化对象并序列化            
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), xmlRootAttribute);
            xmlSerializer.Serialize(xmlWriter, serializerObj, xmlSerializerNamespaces);
        }

        /// <summary>
        /// 从XmlWriter序列化
        /// </summary>
        /// <typeparam name="T">要序列化的类型</typeparam>
        /// <param name="serializerObj">要序列化的对象</param>        
        /// <param name="xmlWriter">要写入序列化内容的对象</param>
        public static void Serializer<T>(T serializerObj, XmlWriter xmlWriter)
        {
            Serializer<T>(serializerObj, "", xmlWriter);
        }

        /// <summary>
        /// 序列化到文件
        /// </summary>
        /// <typeparam name="T">要序列化的类型</typeparam>
        /// <param name="serializerObj">要序列化的对象</param>
        /// <param name="rootElementName">根元素的名称</param>
        /// <param name="file">要写入的文件</param>
        public static void SerializerToFile<T>(T serializerObj, string rootElementName, string file)
        {
            //用XML Writer
            //XmlWriterSettings xws = new XmlWriterSettings();
            //xws.Encoding = System.Text.Encoding.UTF8;            
            //XmlWriter xmlWriter = XmlWriter.Create(file, xws);

            //用XMLTextWriter
            XmlTextWriter xmlTextWriter = new XmlTextWriter(file, System.Text.Encoding.UTF8);
            xmlTextWriter.Formatting = Formatting.Indented;
            xmlTextWriter.Indentation = 1;

            Serializer<T>(serializerObj, rootElementName, xmlTextWriter);
            xmlTextWriter.Close();
        }

        /// <summary>
        /// 序列化到文件
        /// </summary>
        /// <typeparam name="T">要序列化的类型</typeparam>
        /// <param name="serializerObj">要序列化的对象</param>
        /// <param name="file">要写入的文件</param>
        public static void SerializerToFile<T>(T serializerObj, string file)
        {
            SerializerToFile<T>(serializerObj, "", file);
        }
        /// <summary>
        /// 将xml对象内容字符串转换为DataSet
        /// </summary>
        /// <param name="xmlData">XML数据</param>
        /// <returns></returns>
        public static DataSet ConvertXMLToDataSet(string xmlData)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmlData);
                //从stream装载到XmlTextReader
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                return xmlDS;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        /// <summary>
        /// 将xml文件转换为DataSet
        /// </summary>
        /// <param name="xmlFile">xml文件地址</param>
        /// <returns></returns>
        public static DataSet ConvertXMLFileToDataSet(string xmlFile)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                XmlDocument xmld = new XmlDocument();
                xmld.Load(xmlFile);

                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmld.InnerXml);
                //从stream装载到XmlTextReader
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                //xmlDS.ReadXml(xmlFile);
                return xmlDS;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        /// <summary>
        /// 将DataSet转换为xml对象字符串
        /// </summary>
        /// <param name="xmlDS"></param>
        /// <returns></returns>
        public static string ConvertDataSetToXML(DataSet xmlDS)
        {
            MemoryStream stream = null;
            XmlTextWriter writer = null;

            try
            {
                stream = new MemoryStream();
                //从stream装载到XmlTextReader
                writer = new XmlTextWriter(stream, Encoding.Unicode);

                //用WriteXml方法写入文件.
                xmlDS.WriteXml(writer);
                int count = (int)stream.Length;
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(arr, 0, count);

                UnicodeEncoding utf = new UnicodeEncoding();
                return utf.GetString(arr).Trim();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        /// <summary>
        /// 将DataSet转换为xml文件
        /// </summary>
        /// <param name="xmlDS"></param>
        /// <param name="xmlFile"></param>
        public static void ConvertDataSetToXMLFile(DataSet xmlDS, string xmlFile)
        {
            MemoryStream stream = null;
            XmlTextWriter writer = null;

            try
            {
                stream = new MemoryStream();
                //从stream装载到XmlTextReader
                writer = new XmlTextWriter(stream, Encoding.Unicode);

                //用WriteXml方法写入文件.
                xmlDS.WriteXml(writer);
                int count = (int)stream.Length;
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(arr, 0, count);

                //返回Unicode编码的文本
                UnicodeEncoding utf = new UnicodeEncoding();
                StreamWriter sw = new StreamWriter(xmlFile);
                //sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                sw.WriteLine(utf.GetString(arr).Trim());
                sw.Close();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
    }
}
