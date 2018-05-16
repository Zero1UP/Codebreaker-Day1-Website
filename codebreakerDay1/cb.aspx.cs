using System;
using System.Data;
using System.Text;

namespace codebreakerDay1
{
    public partial class cb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var requestMode = Request.QueryString["m"]; // m = mode. Value of s = save and c = code
            var fileID = Request.QueryString["id"];
           

            if(requestMode  != null)
            {
                if(requestMode =="s")
                {
                   if(fileID != null)
                    {
                        downloadFile(requestMode, fileID);
                    }
                    else
                    {
                        listFiles(requestMode);
                    }
                   
                }
                else if (requestMode =="c")
                {
                    if(fileID != null)
                    {
                        downloadFile(requestMode, fileID);
                    }
                    else
                    {
                        listFiles(requestMode);
                    }
                }
                else
                {
                   
                    Response.Write("Nothing to see here folks!");
                }
            }
        }

        private void listFiles(string mode)
        {
            DataSet DS = new DataSet();
            string sql = "EXEC getFiles " + mode; // getFiles is a stored proceedure that handles possible injection (the parameter passed in is also only 1 character anyways)
            DB DBH = new DB(sql);
            StringBuilder outPut = new StringBuilder();

            DS = DBH.GetDataSet();


            foreach(DataRow row in DS.Tables[0].Rows)
            {
                if (mode == "s") //Save files
                {
                    outPut.Append(row["id"].ToString() + "\t" + row["byteSize"].ToString() + "\t\t" + row["fileName"].ToString() + "\t" + Environment.NewLine);
                }
                else if (mode == "c") //Day 1 code files
                {
                    DateTime fileDate = (DateTime)row["fileDate"];
                    outPut.Append(row["id"].ToString() + "\t" + row["byteSize"].ToString() + "\t" + fileDate .ToString("yyyy-MM-dd")+  "\t" + row["fileName"].ToString() + Environment.NewLine);
                }
               
            }

            Response.ContentType = "text/plain";
            Response.Write(outPut.ToString().TrimEnd('\r', '\n'));
            Response.Flush();
            Response.End();
        }

        private void downloadFile(string mode,string id)
        {
            string sql, parameters, values, fileName;
            sql = "EXEC getDownload @mode,@id" ;
            parameters = "@mode,@id";
            values = mode +"," +id;
            DataSet DS = DS = new DataSet();
            DB dbh = new DB(sql, parameters, values);
                       
            DS = dbh.GetDataSet();

            fileName = DS.Tables[0].Rows[0]["fileName"].ToString();


            Response.AppendHeader("content-disposition", "attachment; filename=" + fileName);
            Response.ContentType = "application/octet-stream";
            Response.WriteFile(Server.MapPath("~/s/" + fileName));
            Response.Flush();
            Response.End();
        }
    }
}