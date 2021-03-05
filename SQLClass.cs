using System;
using System.Collections.Generic;
using System.IO;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Windows.Forms;

namespace Booking3
{
    public static class SQLClass
    {
        public const string CONNECTION_STRING =
            "SslMode=none;Server=vh287.spaceweb.ru;Database=beavisabra;port=3306;User Id=beavisabra;Pwd=Beavis1989";

        public static MySqlConnection CONN;

        /// <summary>
        /// Таблица гостиниц
        /// </summary>
        public static string HOTELS = "hotels";

        /// <summary>
        /// Select-запрос. Возвращает список строк
        /// </summary>
        public static List<string> Select(string cmdText)
        {
            List<string> list = new List<string>();

            MySqlCommand cmd = null;
            DbDataReader reader = null;

            try
            {
                cmd = new MySqlCommand(cmdText, CONN);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                        list.Add(reader.GetValue(i).ToString());
                }
            }
            catch (Exception ex)
            {
                if (!File.Exists(Path.GetTempPath() + "/booking.txt"))
                    File.Create(Path.GetTempPath() + "/booking.txt").Close();

                File.AppendAllText(Path.GetTempPath() + "/booking.txt",
                    "Ошибка" + Environment.NewLine +
                    DateTime.Now.ToString() + Environment.NewLine +
                    cmdText + Environment.NewLine + 
                    ex.Message + " " + cmdText + Environment.NewLine + Environment.NewLine);
                MessageBox.Show("Ошибка");
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (cmd != null)
                    cmd.Dispose();
            }

            return list;
        }

        /// <summary>
        /// Insert/Update/Delete-запрос
        /// </summary>
        public static void Update(string cmdText)
        {
            try
            { 
                MySqlCommand cmd = new MySqlCommand(cmdText, CONN);
                cmd.ExecuteReader();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                if (!File.Exists(Path.GetTempPath() + "/booking.txt"))
                    File.Create(Path.GetTempPath() + "/booking.txt").Close();

                File.AppendAllText(Path.GetTempPath() + "/booking.txt",
                    "Ошибка" + Environment.NewLine +
                    DateTime.Now.ToString() + Environment.NewLine +
                    cmdText + Environment.NewLine +
                    ex.Message + Environment.NewLine + Environment.NewLine);
                MessageBox.Show("Ошибка");
            }
        }
    }
}
