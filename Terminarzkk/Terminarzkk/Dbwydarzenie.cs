using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;

namespace Terminarzkk
{
    public class Dbwydarzenie
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<Wydarzenie> Wydarzenia { get; set; }

        public  Dbwydarzenie()
        {
            database = DependencyService.Get<IDbase_con>().DbPoloncz();
            database.CreateTable<Wydarzenie>();
            this.Wydarzenia = new ObservableCollection<Wydarzenie>(database.Table<Wydarzenie>());
        }
        public IEnumerable<Wydarzenie> GetKazdeWydarzenia()
        {
            lock (collisionLock)
            {
                return database.Query<Wydarzenie>("select * From [Wydarzenie] ORDER BY Rok ASC, Miesiac ASC, Dzien ASC,Godz ASC").AsEnumerable();
            }
        }
        public IEnumerable<Wydarzenie> GetKazdeWydarzenianiewys(int Miesiac, int rok,int dzien)
        {
            lock (collisionLock)
            {
                return database.Query<Wydarzenie>("select * From [Wydarzenie]  where wyslano='0' and Miesiac='?' and rok ='?' and dzien='?' ORDER BY Rok ASC, Miesiac ASC, Dzien ASC,Godz ASC ", Miesiac, rok,dzien).AsEnumerable();
            }
        }
        public IEnumerable<Wydarzenie> GetFiltrerWydarzenia(int Miesiac, int rok)
        {
            lock (collisionLock)
            {
                return database.Query<Wydarzenie>("select * From [Wydarzenie] where Miesiac='?' and rok ='?'", Miesiac,rok).AsEnumerable();
            }
        }
        public Wydarzenie GetWydarzenie(int id)
        {
            lock (collisionLock)
            {
                return database.Table<Wydarzenie>().FirstOrDefault(Wydarzenie => Wydarzenie.Id == id);
            }
        }
        public int ZapiszWydarzenie(Wydarzenie wydarzenie_wybrane)
        {
            lock (collisionLock)
            {
                if (wydarzenie_wybrane.Id != 0)
                {
                    database.Update(wydarzenie_wybrane);
                    return wydarzenie_wybrane.Id;
                }
                else
                {
                    database.Insert(wydarzenie_wybrane);
                    return wydarzenie_wybrane.Id;
                }
            }
        }
        public int UsunWydarzenie(Wydarzenie wydarzenie_wybrane)
        {
            var id = wydarzenie_wybrane.Id;
            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<Wydarzenie>(id);
                }
            }
            this.Wydarzenia.Remove(wydarzenie_wybrane);
            return id;
        }
    }
}
