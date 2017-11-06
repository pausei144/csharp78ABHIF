using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using System.Xml;

namespace TaskSheet_03_Artikel_Lieferant
{
    public class DAL
    {
        private string conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + Directory.GetCurrentDirectory() + "\\EinkaufDB.mdf;Integrated Security=True;Connect Timeout=30";
        private SqlConnection m_Con;
        private XmlDocument m_Doc;
        private XmlElement m_Root;
        public DAL()
        {
            m_Con = new SqlConnection(conString);
            m_Doc = new XmlDocument();
            m_Root = m_Doc.DocumentElement;
            m_Doc.LoadXml("<? xml version = '1.0' ?>< Bestellungen />");
            m_Doc.Save(Directory.GetCurrentDirectory() + "\\Bestellung.xml");
        }
        public void ErstelleArtTabelle(string name)
        {
            string query = "CREATE TABLE [dbo].[Artikel]" +
                "(" +
                "[ArtID] INT NOT NULL PRIMARY KEY IDENTITY(1000,100), " +
                "[ArtNr] INT NOT NULL," +
                "[Bezeichnung] NCHAR(50) NULL" +
                ")";
            SqlCommand cmd = new SqlCommand(query, m_Con);
            try
            {
                m_Con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Table "+name + " was already created");
            }
            finally
            {
                m_Con.Close();
            }
        }
        public void ErstelleLieferantenTabelle(string name)
        {
            string query = "CREATE TABLE [dbo].[Lieferant]" +
                "(" +
                "[LieferantID] INT NOT NULL PRIMARY KEY IDENTITY(100,10), " +
                "[Name] NCHAR(50) NULL, " +
                "[Ort] NCHAR(50) NULL," +
                "[PLZ] INT NULL, " +
                ")";
            SqlCommand cmd = new SqlCommand(query, m_Con);
            try
            {
                m_Con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Table " + name + " was already created");
            }
            finally
            {
                m_Con.Close();
            }
        }
        public void ErstelleRelTabelle(string name)
        {
            string query = "CREATE TABLE [dbo].[Relation]" +
                "(" +
                "[RelationID] INT NOT NULL PRIMARY KEY IDENTITY(1,1), " +
                "[ArtID] INT NOT NULL, " +
                "[LieferantID] INT NOT NULL, " +
                "[Menge] INT NULL, " +
                "[Preis] FLOAT NULL, " +
                "CONSTRAINT [CONSTRAINT_ARTIKEL] FOREIGN KEY ([ArtID]) REFERENCES [dbo].[Artikel]([ArtID])," +
                "CONSTRAINT [CONSTRAINT_LIEFERANT] FOREIGN KEY ([LieferantID]) REFERENCES [dbo].[Lieferant]([LieferantID])" +
                ")";
            SqlCommand cmd = new SqlCommand(query, m_Con);
            try
            {
                m_Con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Table " + name + " was already created");
            }
            finally
            {
                m_Con.Close();
            }
        }
        public void TextdateiNachDOM()
        {
            string path = Directory.GetCurrentDirectory() + "\\Bestellung.txt";
            StreamReader sr = new StreamReader(path);
            sr.ReadLine();

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (line.Split()[0] == "Firma")
                {
                    string name = line.Split()[1];
                    line = sr.ReadLine();
                    string plz = line.Split()[0];
                    string ort = line.Split()[1];
                }
            }
        }
    }
}
