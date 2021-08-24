using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeListDemo.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace TreeListDemo.BL
{
    public class Generator
    {
         ApplicationDbContext _dbContext;
        public Generator(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public string DataTableToJSONWithStringBuilder(DataTable table)
        {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
            }
            return JSONString.ToString();
        }

        public string LoadData(int ViewID, string Condition="", string langCode = "en")
        {
           var fieldLst = _dbContext.ViewFields.Where(a => a.ViewID == ViewID).Select(a => a.field.ID).ToList();
          var dt=  GetLocalDBDataTable(fieldLst, ViewID, "");
            List<object> lst = new List<object>();

            return DataTableToJSONWithStringBuilder(dt);

            // we need to customize this to be list of data 
            // remmber to add headID
            // remmber


        }
        public  DataTable GetLocalDBDataTable(List<int> fieldLst, int ViewID, string Condition, string langCode = "en")
        {

            DataTable dt = new DataTable();

            string Query = ConcateReportGeneratorQuery(fieldLst, ViewID, langCode);
            if (Condition != string.Empty && Condition != null && Condition != "")
            {
                Condition = " Where 1=1 and " + Condition;
                Query = Query + Condition;
            }

            string errorMsg = string.Empty;
            var cnn = (SqlConnection)_dbContext.Database.GetDbConnection();
            cnn.Open();
            using (var cmd = new SqlCommand(Query, cnn))
            {
                using (var rdr = cmd.ExecuteReader())
                {
                    dt.Load(rdr);
                }
            }
                return dt;

        }
        public string ConcateReportGeneratorQuery(List<int> fieldLst, int viewNo, string langCode = "en")
        {

            List<Fields> FieldList = new List<Fields>();
            FieldList = GetMainQuery(fieldLst, viewNo, langCode);
            StringBuilder SelectStatement = new StringBuilder();
            StringBuilder FromStatement = new StringBuilder();

            foreach (Fields field in FieldList)
            {
                switch (field.FieldType)
                {
                    case FieldTypeLib.NormalEnglish:
                    case FieldTypeLib.NormalArabic:
                        if (!SelectStatement.ToString().Contains(field.DisplayFieldprefix))
                        {
                            SelectStatement.Append(string.Concat(field.TableName, ".", field.FieldsPrefix," as ", field.DisplayFieldprefix ," , "));
                        }

                        if (!FromStatement.ToString().Contains(field.TableName))
                        {
                            FromStatement.Append(field.TableName);
                        }
                        break;                 
                    //3,4
                    // '' select  ''+[FieldPrefix] + '' , '''''' + [FieldPrefix] + '''''' as keys from  ''+[TableName] +'' join ''+ [SecondaryTable]+ '' on ''+ [SecondaryTable]+''.''+[SecondaryLink]+''=''+[TableName]+''.''+convert(nvarchar(max),[PrimaryTableLink]) + '' where ''+ [TableName]+''.'' +'''+@param+''''
                    case FieldTypeLib.JoinNormalEnglish:
                    case FieldTypeLib.JoinNormalArabic:
                        if (!SelectStatement.ToString().Contains(field.DisplayFieldprefix))
                        {
                            SelectStatement.Append(string.Concat(field.SecondaryTable, ".", field.FieldsPrefix, " as ", field.DisplayFieldprefix, " , "));
                        }
                        if (!FromStatement.ToString().Contains(field.TableName))
                        {
                            FromStatement.Append(field.TableName);
                        }
                        if (!FromStatement.ToString().Contains(field.SecondaryTable))
                        {
                            FromStatement.Append(string.Concat(" left join ", field.SecondaryTable, " on ", field.SecondaryTable, '.', field.SecondaryLink, " = ", field.TableName, " . ", field.LinkingFieldPrefix));
                        }


                        break;
   
                } // switch end 

            }

            string x = string.Empty;
            if (SelectStatement.ToString() != "" && SelectStatement != null && SelectStatement.ToString() != string.Empty)
            {
                x = " select " + SelectStatement.ToString().Remove(SelectStatement.Length - 2) + " from  " + FromStatement.ToString();
            }
            return x;
        }
        private  List<Fields> GetMainQuery(List<int> fieldLst, int viewNo, string langCode = "en")
        {
         var fieldslst=   _dbContext.Fields.Where(a => fieldLst.Contains(a.ID)).ToList();  
            return fieldslst;
        }
    }
}
