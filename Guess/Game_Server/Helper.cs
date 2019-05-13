using System.IO;
using System.Xml.Serialization;

namespace Game_Server
{
	public static class Helper
	{
		/// <summary>
		/// Convert object of type T to xml
		/// </summary>
		/// <typeparam name="T">Type of object</typeparam>
		/// <param name="toSerialize"></param>
		/// <returns></returns>
		public static string Serialize<T>(this T toSerialize)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
			using (StringWriter textWriter = new StringWriter())
			{
				xmlSerializer.Serialize(textWriter, toSerialize);
				return textWriter.ToString();
			}
		}
		/// <summary>
		/// Convert xml to object
		/// </summary>
		/// <typeparam name="T">Type of object</typeparam>
		/// <param name="toDeserialize"></param>
		/// <returns></returns>
		public static T Deserialize<T>(this string toDeserialize)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
			using (StringReader textReader = new StringReader(toDeserialize))
			{
				return (T)xmlSerializer.Deserialize(textReader);
			}
		}
	}
}
