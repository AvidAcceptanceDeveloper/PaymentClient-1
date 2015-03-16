using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDSSNLSMPUtilsClasses
{
    public class cData
    {
        List<System.Data.OleDb.OleDbParameter> _oledbParametercollection = new List<System.Data.OleDb.OleDbParameter>();

        protected System.Data.OleDb.OleDbConnection GetConnection()
        {
            cSettings oSettings = new cSettings(Properties.Settings.Default.SettingsFile);
            System.Data.OleDb.OleDbConnection oConn = new System.Data.OleDb.OleDbConnection();

            string strServerName = oSettings.NLSRSServer;
            string strDatabaseName = oSettings.NLSRSDatabase;
            string strUser = oSettings.NLSRSUserID;
            string strPassword = oSettings.NLSRSPassword;

            //connectionString="data source=TestPC\SQLEXPRESS;initial catalog=MyTestDB;user id=User1;password=p@ssw0rd;persist security info=True;" providerName="System.Data.SqlClient" 
            //string sConnectionString = "Server='" + strServerName + "';Database='" + strDatabaseName + "';User Id='" + strUser + "';Password='" + strPassword + "'; Provider='SQLOLEDB';";

            string sConnectionString = "Data Source='" + strServerName + "';Initial Catalog='" + strDatabaseName + "';User ID='" + strUser + "';Password='" + strPassword + "'; Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;Provider=SQLOLEDB;";
            oConn.ConnectionString = sConnectionString;
            //oConn.Database = strDatabaseName;
            oConn.Open();
            if (oConn.State == System.Data.ConnectionState.Open)
            {
                return oConn;
            }
            else
            {
                Exception exdb = new Exception("Error connecting to database.  Connection String: " + sConnectionString);
                throw new Exception("Error Opening Connection to NLS Database.", exdb);
            }

        }

        public System.Data.DataSet GetAllRows(string strTableName)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            System.Data.OleDb.OleDbCommand oCommand = new System.Data.OleDb.OleDbCommand();
            oCommand.CommandText = strTableName;
            oCommand.CommandType = System.Data.CommandType.TableDirect;
            oCommand.Connection = GetConnection();

            System.Data.OleDb.OleDbDataAdapter oDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oCommand);
            oDataAdapter.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }
        public List<System.Data.OleDb.OleDbParameter> ParamCollection
        {
            get
            {
                return _oledbParametercollection;
            }

        }
        public void AddToParameterCollection(string sData, string sSourceColumn)
        {
            System.Data.OleDb.OleDbParameter oParameter = new System.Data.OleDb.OleDbParameter("Data", sData);
            oParameter.SourceColumn = sSourceColumn;
            _oledbParametercollection.Add(oParameter);

        }


        public System.Data.DataSet GetRowsByParameterList(String SQLStatement, List<System.Data.OleDb.OleDbParameter> ParameterList)
        {

            System.Data.DataSet ds = new System.Data.DataSet();
            System.Data.OleDb.OleDbCommand oCommand = new System.Data.OleDb.OleDbCommand();
            oCommand.CommandText = SQLStatement;
            oCommand.CommandType = System.Data.CommandType.Text;
            oCommand.Parameters.Clear();
            foreach (var pItem in ParameterList)
            {
                oCommand.Parameters.Add(pItem);
            }
            oCommand.Connection = GetConnection();

            System.Data.OleDb.OleDbDataAdapter oDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oCommand);
            oDataAdapter.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }
        public System.Data.DataSet GetRowsByWhereClause(String SQLStatement, string SQLWhereClause)
        {

            System.Data.DataSet ds = new System.Data.DataSet();
            System.Data.OleDb.OleDbCommand oCommand = new System.Data.OleDb.OleDbCommand();
            oCommand.CommandText = SQLStatement + " WHERE " + SQLWhereClause;
            oCommand.CommandType = System.Data.CommandType.Text;
            oCommand.Connection = GetConnection();
            System.Data.OleDb.OleDbDataAdapter oDataAdapter = new System.Data.OleDb.OleDbDataAdapter(oCommand);
            oDataAdapter.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }


    }
}
