using System;
using System.IO;
using SQLite;
using Terminarzkk.iOS;
[assembly: Xamarin.Forms.Dependency(typeof(IDbase_con_ios))]
namespace Terminarzkk.iOS
{
    class IDbase_con_ios: IDbase_con
    {
        public SQLiteConnection DbPoloncz()
        {
            var dbNazwa = "WydazenDB.db3";
            string osobistyfolder = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string folderbiblioteczny = Path.Combine(osobistyfolder, "..", "Library");
            var path = Path.Combine(folderbiblioteczny, dbNazwa);
            return new SQLiteConnection(path);
        }
    }
}