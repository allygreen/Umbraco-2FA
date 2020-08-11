using System.Linq;
using Orc.Fortress.Database.Models;
using Umbraco.Core;
using Umbraco.Core.Persistence;
using Umbraco.Web;
using Umbraco.Core.Persistence.SqlSyntax;

namespace Orc.Fortress.Database
{
    public class FortressDatabase
    {
        private static readonly ISqlSyntaxProvider SqlSyntaxProvider =
                   ApplicationContext.Current.DatabaseContext.SqlSyntax;
        public FortressUser2FASettings GetUserDetails(int id)
        {
            var db = ApplicationContext.Current.DatabaseContext.Database;
            var query = new Sql().Select("*").From<FortressUser2FASettings>(SqlSyntaxProvider).Where<FortressUser2FASettings>(x => x.UserId == id, SqlSyntaxProvider);
            var results =
                db.FirstOrDefault<FortressUser2FASettings>(query);

            return results;
        }

        public void Update(FortressUser2FASettings details)
        {
            var db = ApplicationContext.Current.DatabaseContext.Database;
            db.Update(details);
        }

        public void Insert(object details)
        {
            var db = ApplicationContext.Current.DatabaseContext.Database;
            db.Insert(details);
        }

        public FortressSettings GetSettingsFromDatabase()
        {
                var db = ApplicationContext.Current.DatabaseContext.Database;
            var query = new Sql().Select("*").From<FortressSettingEntry>(SqlSyntaxProvider);

                var results = db.Fetch<FortressSettingEntry>(query);
                var model = new FortressSettings(results);
            return model;
        }

        public void SaveSettings(FortressSettings settings)
        {
            var db = ApplicationContext.Current.DatabaseContext.Database;
            var data = settings.GetRawData().Select(x=>x.Value);
            var currentDatabaseSettings = GetSettingsFromDatabase();
            var currentData = currentDatabaseSettings.GetRawData();
            foreach (var fortressSettingEntry in data)
            {
                if (currentData.ContainsKey(fortressSettingEntry.Key)){
                    db.Update(fortressSettingEntry);
                }else
                {
                    db.Insert(fortressSettingEntry);
                }
            }
            
        }
        
    }
}