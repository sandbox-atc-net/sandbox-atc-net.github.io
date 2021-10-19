﻿using System.Linq;
using AtcWeb.Extensions;
using AtcWeb.Models;
using Microsoft.AspNetCore.Components;

namespace AtcWeb.Shared
{
    public partial class NavMenu
    {
        private string section;
        private string componentLink;

        [Inject] NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            Refresh();
            base.OnInitialized();
        }

        public void Refresh()
        {
            section = NavigationManager.GetSection();
            componentLink = NavigationManager.GetComponentLink();
            StateHasChanged();
        }

        public bool IsSubGroupExpanded(AtcComponent item)
        {
            return item.GroupItems.Elements.Any(i => i.Link == componentLink);
        }
    }
}