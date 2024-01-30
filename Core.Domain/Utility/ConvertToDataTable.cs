using System.ComponentModel;
using System.Data;

namespace Core.Domain.Utilities
{
    public class ConvertToDataTable
    {
        public static DataTable BuildDataTable<T>(IList<T> lst)
        {
            //create DataTable Structure
            DataTable tbl = CreateTable<T>();
            Type entType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entType);
            //get the list item and add into the list
            foreach (T item in lst)
            {
                DataRow row = tbl.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    if (prop.PropertyType.Name != "Nullable`1")
                        row[prop.Name] = prop.GetValue(item);
                    else
                        row[prop.Name] = prop.GetValue(item);
                }
                tbl.Rows.Add(row);
            }
            return tbl;
        }
        private static DataTable CreateTable<T>()
        {
            //T –> ClassName
            Type entType = typeof(T);
            //set the datatable name as class name
            DataTable tbl = new DataTable(entType.Name);
            //get the property list
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entType);
            foreach (PropertyDescriptor prop in properties)
            {
                if (prop.PropertyType.Name != "Nullable`1")
                    tbl.Columns.Add(prop.Name, prop.PropertyType);
                else
                    tbl.Columns.Add(prop.Name, typeof(string));
                //add property as column

            }
            return tbl;
        }
    }
}
