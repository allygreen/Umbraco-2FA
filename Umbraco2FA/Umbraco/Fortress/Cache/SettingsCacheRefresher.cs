using System;
using Orc.Fortress.Database;
using umbraco.interfaces;
using Umbraco.Core;
using Umbraco.Core.Cache;

namespace Orc.Fortress.Cache
{
    public class SettingsCacheRefresher : TypedCacheRefresherBase<SettingsCacheRefresher, FortressSettings>, ICacheRefresher
    {
        protected override SettingsCacheRefresher Instance
        {
            get { return this; }
        }
   
        public override Guid UniqueIdentifier
        {
            get { return CacheKeys.SettingsCache.RefresherGuid; }
        }

        public override string Name
        {
            get { return "Fortress Settings Cache"; }
        }

        public override void RefreshAll()
        {
            ApplicationContext.Current.ApplicationCache.RuntimeCache.ClearCacheItem(CacheKeys.SettingsCache.Key);
            base.RefreshAll();
        }
    }


}
