using Microsoft.AspNetCore.Mvc;
using P_Micro_Banking.Models.Menu;
using P_Micro_Banking.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P_Micro_Banking.Components
{
    public class MenuViewComponent: ViewComponent
    {
        private readonly IDataAccessLayer _DataAccessLayer;

        public MenuViewComponent(IDataAccessLayer DataAccessLayer)
        {
            _DataAccessLayer = DataAccessLayer;
        }

        public async Task<IViewComponentResult> InvokeAsync(string roleId)
        {

            var item = await GetMenuAsync(roleId);

            return View(item);
        }

        public async Task<List<MenuViewModel>> GetMenuAsync(string roleId)
        {
            string returnstr = string.Empty;

            List<MenuViewModel> menuViewModels = new List<MenuViewModel>();
            IEnumerable<Menu> menu = new List<Menu>();
            IEnumerable<MenuChild> menuchild = new List<MenuChild>();


            menu = await _DataAccessLayer.Menu(1);
            menuchild = await _DataAccessLayer.MenuChild(2, roleId);

            List<MenuChildViewModel> menuChildViewModels = new List<MenuChildViewModel>();
            MenuViewModel menuVM = new MenuViewModel();
            MenuChildViewModel menuCVM = new MenuChildViewModel();
            foreach (var mlop in menu)
            {
                menuVM = new MenuViewModel();

                var smc = menuchild.Where(mid => mid.Menu_ID == mlop.ID);
                foreach (var mclop in smc)
                {
                    menuCVM = new MenuChildViewModel();
                    menuCVM.Menu_ID = mclop.Menu_ID;
                    menuCVM.MenuNames = mclop.MenuNames;
                    menuCVM.Paths = mclop.Paths;
                    menuCVM.IconClass = mclop.IconClass;
                    menuCVM.Url = mclop.Url;
                    menuCVM.OnClick = mclop.OnClick;
                    menuCVM.ListClass = mclop.ListClass;
                    menuCVM.aClass = mclop.aClass;

                    menuChildViewModels.Add(menuCVM);
                }

                menuVM.ID = mlop.ID;
                menuVM.MenuNames = mlop.MenuNames;
                menuVM.Paths = mlop.Paths;
                menuVM.IconClass = mlop.IconClass;
                menuVM.Url = mlop.Url;
                menuVM.OnClick = mlop.OnClick;
                menuVM.ListClass = mlop.ListClass;
                menuVM.aClass = mlop.aClass;
                menuVM.Children = menuChildViewModels;

                menuViewModels.Add(menuVM);
            }

            return menuViewModels;

        }

    }
}
