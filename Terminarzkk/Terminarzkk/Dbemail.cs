using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;

namespace Terminarzkk
{
    public class Dbemail
    {
        private SQLiteConnection database1;
        private static object collisionLock = new object();
        public ObservableCollection<Email> Mojemail { get; set; }

        public Dbemail()
        {
            database1 = DependencyService.Get<IDbase_con>().DbPoloncz();
            database1.CreateTable<Email>();
            this.Mojemail = new ObservableCollection<Email>(database1.Table<Email>());
               
        }
        public Email GetEmail()
        {
            lock (collisionLock)
            {
                Email k = new Email();
                int jeden = 1;
                if (database1.Table<Email>().FirstOrDefault(Email => Email.ID == jeden) == null) {
                    k.Nazwa = "brak";
                    k.Password = "brak";
                    k.ID = 1;
                }
                else { k = database1.Table<Email>().FirstOrDefault(Email => Email.ID == jeden); }
                return k;
            }
        }
        public void Zapiszemail(Email Email_wybrane)
        {
            lock (collisionLock)
            {    Email_wybrane.ID = 1;
               
                if (database1.Table<Email>().FirstOrDefault(Email => Email.ID == Email_wybrane.ID) == null) {
                    database1.Insert(Email_wybrane);
                }
                else {
                    database1.Update(Email_wybrane);
                }
                   
                 

                
                
            }
        }
      
    }
}