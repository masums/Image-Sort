﻿using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageSort.SettingsManagement
{
    public class SettingsViewModel : ReactiveObject
    {
        public IEnumerable<SettingsGroupViewModelBase> SettingsGroups { get; }

        public SettingsViewModel(IEnumerable<SettingsGroupViewModelBase> settingsGroups = null)
        {
            SettingsGroups = settingsGroups ?? Locator.Current.GetService<IEnumerable<SettingsGroupViewModelBase>>();
        }

        public TGroup GetGroup<TGroup>() where TGroup : SettingsGroupViewModelBase
        {
            return SettingsGroups.OfType<TGroup>()
                .FirstOrDefault();
        }

        public Dictionary<string, Dictionary<string, object>> AsDictionary()
        {
            var dict = new Dictionary<string, Dictionary<string, object>>();

            foreach (var group in SettingsGroups)
            {
                dict.Add(group.Name, group.SettingsStore);
            }

            return dict;
        }

        public void RestoreFromDictionary(Dictionary<string, Dictionary<string, object>> dictionary)
        {
            if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));

            foreach (var storedGroup in dictionary)
            {
                var settingsGroup = SettingsGroups.FirstOrDefault(g => g.Name == storedGroup.Key);

                foreach (var setting in storedGroup.Value)
                {
                    settingsGroup.SettingsStore[setting.Key] = setting.Value;
                }

                settingsGroup.UpdatePropertiesFromStore();
            }
        }
    }
}
