using SQLite;
using Terminarzkk.Droid;
using System.IO;
[assembly: Xamarin.Forms.Dependency(typeof(IDbase_con_android))]
namespace Terminarzkk.Droid
{
    class IDbase_con_android : IDbase_con
    {
        public SQLiteConnection DbPoloncz()
        {
            var dbNazwa = "WydazenDB.db3";
            var scieszka = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbNazwa);
            return new SQLiteConnection(scieszka);
        }
    }
}