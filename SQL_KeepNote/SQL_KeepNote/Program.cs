using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Note_CRUD_Exercise
{
    class TakeNotes
    {
        SqlConnection conn = new SqlConnection("Data source = IN-B33K9S3; Initial Catalog = Expenses_Tracker;Integrated Security=true");
        DataSet ds = new DataSet();
        public void CreateNote()
        {
            Console.WriteLine("Enter Title");
            string title = Console.ReadLine();
            Console.WriteLine("Enter Description");
            string description = Console.ReadLine();
            Console.Write("Enter Date (dd-MM-yyyy): ");
            DateTime date = Convert.ToDateTime(Console.ReadLine());

            SqlDataAdapter adp = new SqlDataAdapter("Select * from KeepNote", conn);
            adp.Fill(ds);
            
            var row = ds.Tables[0].NewRow();
            row["Title"] = title;
            row["Description"] = description;
            row["Date"] = date;

            ds.Tables[0].Rows.Add(row);

            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds);

            Console.WriteLine("Note added successfully");
        }

        public void ViewNote()
        {
            Console.WriteLine("Enter Note ID");
            int id = Convert.ToInt32(Console.ReadLine());
            SqlDataAdapter adp = new SqlDataAdapter($"Select * from KeepNote where Id ={id}", conn);
            adp.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    Console.Write($"{ds.Tables[0].Rows[i][j]} | ");
                }
                Console.WriteLine();
            }
        }

        public void ViewNotes()
        {
            SqlDataAdapter adp = new SqlDataAdapter("Select * from KeepNote", conn);
            adp.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    Console.Write($"{ds.Tables[0].Rows[i][j]} | ");
                }
                Console.WriteLine();
            }
        

        }

        public void UpdateNote()
        {
            Console.WriteLine("Enter the Id of the row to update:");
            int id = Convert.ToInt32(Console.ReadLine());

            string selectQuery = $"SELECT * FROM KeepNote WHERE Id = {id}";

            SqlDataAdapter adp = new SqlDataAdapter(selectQuery, conn);
            adp.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {             
                 
                Console.WriteLine("Enter the new Title:");
                string newtitle = Console.ReadLine();

                Console.WriteLine("Enter the new Description:");
                string newdescription = Console.ReadLine();

                Console.WriteLine("Enter the new Date (yyyy-MM-dd):");
                DateTime newdate = DateTime.Parse(Console.ReadLine());

                ds.Tables[0].Rows[0]["Title"] = newtitle;
                ds.Tables[0].Rows[0]["Description"] = newdescription;
                ds.Tables[0].Rows[0]["Date"] = newdate;

                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adp);
                adp.Update(ds);

                Console.WriteLine("Row updated successfully.");
            }
            else
            {
                Console.WriteLine($"No row found with Id : {id}.");
            }

        }

        public void DeleteNote()
        {
            Console.WriteLine("Enter Note Id you want to delete");
            int id = Convert.ToInt16(Console.ReadLine());

            string selectQuery = $"select * FROM KeepNote WHERE Id = {id}";

            SqlDataAdapter adp = new SqlDataAdapter(selectQuery, conn);
            adp.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Rows[0].Delete();                                

                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adp);
                adp.Update(ds);

                Console.WriteLine("Row Deleted successfully.");
            }
            else
            {
                Console.WriteLine($"No row found with Id : {id}.");
            }


        }

        
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //SqlConnection conn = new SqlConnection("Data source = IN-B33K9S3; Initial Catalog = Expenses_Tracker;Integrated Security=true");
            TakeNotes takenotes = new TakeNotes();
            int choice = 0;

            while (true)
            {
                Console.WriteLine("Welcome to the Take Note Applcation");
                Console.WriteLine("1. Create the Note");
                Console.WriteLine("2. View Note  by ID");
                Console.WriteLine("3. View all Notes");
                Console.WriteLine("4. Update Note");
                Console.WriteLine("5. Delete Note by ID");
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Enter only numbers");
                    continue;
                }
                switch (choice)
                {
                    case 1:
                        {
                            takenotes.CreateNote();
                            break;
                        }

                    case 2:
                        {
                            takenotes.ViewNote();
                            break;
                        }

                    case 3:
                        {
                            takenotes.ViewNotes();
                            break;
                        }

                    case 4:
                        {
                            takenotes.UpdateNote();
                            break;
                        }

                    case 5:
                        {
                            takenotes.DeleteNote();
                            break;
                        }
                }
            }
        }
    }
}