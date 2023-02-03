using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ReportingReflection
{
    public class CSV2Genertor<T> where T : class
    {
        private readonly IEnumerable<T> _data;
        private readonly string _filename;
        private readonly Type _type;
        public CSV2Genertor(IEnumerable<T> data , string filename)
        {
            this._data = data;
            _filename = filename;
            _type= typeof(T);   

        }

         public void Generator ()
        {
            var row = new List<string>();
            row.Add(CreateHeader());
            foreach (var item in _data)
            {
                row.Add(CreateRow( item));
            }
            File.WriteAllLines(_filename,row.ToArray());
        }

        private string CreateRow(T item)
        {
            var propertyInfos = _type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderProperties = propertyInfos.OrderBy(item => item.GetCustomAttribute<ReportItem2Attribute>());
            var bb = new StringBuilder();
            foreach (var prop in orderProperties)
            {
               bb.Append( CreateItem(prop, item)).Append(",");

            }
            return bb.ToString()[..^1];
        }

        private string CreateHeader()
        {
            var propertyInfos = _type.GetProperties(BindingFlags.Public| BindingFlags.Instance);
            var orderProperties= propertyInfos.OrderBy(item=>item.GetCustomAttribute<ReportItem2Attribute>());
            var bb = new StringBuilder();
            foreach (var item in orderProperties)
            {
                var attr = item.GetCustomAttribute<ReportItem2Attribute>();
                bb.Append(attr?.Heading ?? item.Name).Append(",");
              
            }
            Console.WriteLine(bb);
            return bb.ToString()[..^1];
        }

        private string CreateItem(PropertyInfo prop, T item)
        {
            var attr = prop.GetCustomAttribute<ReportItemAttribute>();

            return string.Format($"{{0:{attr.Format}}}{attr.Units}", prop.GetValue(item));
        }
    }
}
