using Xamarin.Forms;
using SQLite;
using Terminarzkk.UWP;
using Windows.Storage;
using System.IO;
[assembly: Dependency(typeof(IDbase_con_uniwin))]
namespace Terminarzkk.UWP
{
    class IDbase_con_uniwin : IDbase_con
    {
        public SQLiteConnection DbPoloncz()
        {
            var dbNazwa = "WydazenDB.db3";
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, dbNazwa);
            return new SQLiteConnection(path);
        }
    }
}
